using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        
        public clsUsers(string username , string pass , int perID , bool Act) 
        {
            Mode = EnMode.New;
            Person = clsPeoples.FindByPersonalID(perID);
            UserName = username;
            Password = pass;
            IsActive = Act;
            
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
            return new clsUsers(-1 , "", "", -1, false); 
        }

        public static clsUsers FindByUserNamePass(string username , string Pass) 
        {
            int perID = -1;
            int userID = -1;
            bool isActive = false;
            if (clsSqlUsers.FindUserByUserNameAndPassword(username, Pass, ref userID, ref perID, ref isActive))
            {
                return new clsUsers (userID , username,Pass , perID , isActive );
            }

            else
            {
                return null;
            }
            
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
