using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using DataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsUsers
    {
        public enum EnMode { New = 0, Update = 1 };

        public EnMode Mode = EnMode.New;
        public clsPeoples Person = clsPeoples.GetEmptyPerson();
        public int UserID = -1;
        public string UserName;
        public string Password;
        public bool IsActive;

        // ---------- Password hashing (PBKDF2-HMAC-SHA256, salted) ----------
        // Stored format: "<iterations>:<base64-salt>:<base64-hash>"
        private const int PBKDF2_ITERATIONS = 100_000;
        private const int PBKDF2_SALT_BYTES = 16; //number of bytes in the salt (128-bit)
        private const int PBKDF2_HASH_BYTES = 32; // 256-bit

        /// <summary>
        /// Hashes a plain-text password with a random per-user salt using
        /// PBKDF2-HMAC-SHA256. Returns "iterations:salt:hash".
        /// This is the single static helper used everywhere a password is set.
        /// </summary>
        public static string HashPassword(string plainPassword)
        {
            if (plainPassword == null)
                plainPassword = "";

            byte[] salt = new byte[PBKDF2_SALT_BYTES];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                       plainPassword, salt, PBKDF2_ITERATIONS, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(PBKDF2_HASH_BYTES);
                return PBKDF2_ITERATIONS + ":" + Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Verifies a plain-text password against a stored
        /// "iterations:salt:hash" string. Uses a constant-time compare.
        /// </summary>
        public static bool VerifyPassword(string plainPassword, string stored)
        {   
            if (string.IsNullOrEmpty(stored) || stored.IndexOf(':') < 0) 
            {   
                return false;
            }

            string[] parts = stored.Split(':');
            if (parts.Length != 3) return false;
            if (!int.TryParse(parts[0], out int iterations) || iterations <= 0) return false;
            byte[] salt;
            byte[] expected;
            try
            {
                salt = Convert.FromBase64String(parts[1]);
                expected = Convert.FromBase64String(parts[2]);
                
            }
            catch
            {
                return false;
            }


            using (var pbkdf2 = new Rfc2898DeriveBytes(
                       plainPassword ?? "", salt, iterations, HashAlgorithmName.SHA256))
            {
                byte[] actual = pbkdf2.GetBytes(expected.Length);

                return SlowEquals(actual, expected);
            }
        }

        /// <summary>
        /// Detects whether a string is already in our stored hash format,
        /// so we never double-hash a password (e.g. when editing a user
        /// without changing the password, or when "Remember me" feeds the
        /// stored hash back into the login flow).
        /// </summary>
        public static bool IsPasswordHashed(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            string[] parts = value.Split(':');
            if (parts.Length != 3) return false;
            if (!int.TryParse(parts[0], out int iters) || iters <= 0) return false;
            try
            {
                Convert.FromBase64String(parts[1]);
                Convert.FromBase64String(parts[2]);
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// Constant-time comparison of two stored hash strings.
        /// Used for the "Remember me" path where the registry holds the
        /// stored hash itself (read back into the login form).
        /// </summary>
        public static bool HashEquals(string storedA, string storedB)
        {
            byte[] a = Encoding.UTF8.GetBytes(storedA ?? "");
            byte[] b = Encoding.UTF8.GetBytes(storedB ?? "");
            return SlowEquals(a, b);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }

        
        public clsUsers(string username , string pass , int perID , bool Act) 
        {
            this.Mode = EnMode.New;
            this.Person = clsPeoples.FindByPersonalID(perID);
            this.UserName = username;
            this.Password = pass;
            this.IsActive = Act;
            this.UserID = -1;
            
        }

        public clsUsers(int User_Id , string username, string pass, int perID, bool Act)
        {
            Mode = EnMode.Update;
            UserID= User_Id;
            if (perID == -1)
            {
                Person = clsPeoples.GetEmptyPerson();
            }
            else
            {
                Person = clsPeoples.FindByPersonalID(perID);
            }
            UserName = username;
            Password = pass;
            IsActive = Act;

        }

        public static clsUsers GetEmptyUser() 
        {
            return new clsUsers( "", "", -1, false); 
        }

        public static clsUsers FindByUserNamePass(string username , string Pass) 
        {
            int perID = -1;
            int userID = -1;
            bool isActive = false;
            string storedHash = "";

            // NOTE: The plain-text password is NEVER sent to the DAL.
            // We look the user up by user name only, then verify the
            // password here in the business layer against the stored
            // salted hash. (Salted hashes cannot be compared in SQL.)
            if (!clsSqlUsers.FindUserByUserName(username, ref storedHash, ref userID, ref perID, ref isActive))
            {
                return null;
            }   

           

            bool ok;
            if (IsPasswordHashed(Pass))
            {
                // "Remember me" path: the caller (LogInScreen) supplied the
                // stored hash itself, read back from the registry. Compare
                // directly, constant-time.
                ok = HashEquals(Pass, storedHash);
            }
            else
            {
                // Normal path: caller supplied plain-text -> verify with PBKDF2.
                ok = VerifyPassword(Pass, storedHash);
            }

            if (!ok)
                return null;

            // Keep the stored hash in the object so other forms (e.g. update
            // password) can verify against it later.
            return new clsUsers(userID, username, storedHash, perID, isActive);
        }

        public static DataTable LisUsers()
        {
            return clsSqlUsers.ListUsers();
        }
        
        public static bool IsUserExistByPersonID(int personID)
        {
            return clsSqlUsers.IsUserExistByPersonID(personID);
        }

        public void Save()
        {
            // Never send a plain-text password to the DAL. Hash it once;
            // the IsPasswordHashed guard prevents double-hashing when the
            // value is already a hash (e.g. editing a user without
            // changing the password).
            if (!IsPasswordHashed(Password))
                Password = HashPassword(Password);

            switch(Mode)
            {
                case EnMode.New:
                    clsSqlUsers.AddNewUser(ref UserID , Person.PerID , UserName , Password , IsActive );
                    break;
                case EnMode.Update:
                    clsSqlUsers.UpdateUser(UserID, Person.PerID, UserName, Password, IsActive);
                    break;
                default: break;
            }
        }

        public static bool DeleteUser(int User_id)
        {
            return clsSqlUsers.DeleteUser(User_id);
        }

        public static clsUsers FindUserByUserID(int User_id) 
        {
            string pass=""; string username=""; int per_id=-1; bool is_active=false;
            if(clsSqlUsers.FindUserByUserID(ref username , ref pass , User_id ,ref per_id , ref is_active ))
            {
                return new clsUsers(User_id , username , pass , per_id , is_active);
            }
            else
            {
                return null;
            }
        }

    }
}
