using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DataAccessLayer;


namespace DVLDBusinessLayer
{

    public class clsPeoples
    {
        public enum EnMode { New = 0, Update = 1 };

        public EnMode Mode = EnMode.New;
        public int PerID;
        public string NationalNub;
        public string FirstName;
        public string SecondName;
        public string ThirdName;
        public string LastName;
        public string Phone;
        public string Email;
        public int Gender;
        public int Nationality;
        public DateTime DateOfBirth;
        public string Address;
        public string ImagePath;
        public string LastImg = "";

        public clsPeoples(string NatNub, string FN, string SN, string TN, string LN, string Phn, string Em
              , int Nat, DateTime date, int gender, string Addr, string Img)
        {
            this.NationalNub = NatNub;
            this.FirstName = FN;
            this.SecondName = SN;
            this.ThirdName = TN;
            this.LastName = LN;
            this.Phone = Phn;
            this.Email = Em;
            this.Gender = gender;
            this.Nationality = Nat;
            this.DateOfBirth = date;
            this.Address = Addr;
            this.ImagePath = Img;
            this.Mode = EnMode.New;
            // object is full but in data base he still did not added
        }

        private clsPeoples(int PersonID, string NatNub, string FN, string SN, string TN, string LN, string Phn, string Em
              , int Nat, DateTime date, int gender, string Addr, string Img)
        {
            this.PerID = PersonID;
            this.NationalNub = NatNub;
            this.FirstName = FN;
            this.SecondName = SN;
            this.ThirdName = TN;
            this.LastName = LN;
            this.Phone = Phn;
            this.Email = Em;
            this.Gender = gender;
            this.Nationality = Nat;
            this.DateOfBirth = date;
            this.Address = Addr;
            this.ImagePath = Img;
            this.LastImg = Img;
            this.Mode = EnMode.Update;
        }

        public void Save()
        {
            switch (Mode)
            {
                case EnMode.New:
                    // copy the chosen image (if any) into the ImageCopy folder
                    ImagePath = CopyImageToFolder(ImagePath);
                    this.PerID = clsSqlPeoples.AddPerson(NationalNub, FirstName, SecondName, ThirdName, LastName
                        , Phone, Email, Nationality, DateOfBirth, Gender, Address, ImagePath);
                    this.Mode = EnMode.Update;
                    this.LastImg = ImagePath;
                    break;

                case EnMode.Update:
                    // Decide what to do with the image based on ImagePath vs LastImg
                    if (string.IsNullOrEmpty(ImagePath))
                    {
                        // image removed -> delete the previously stored file (if any)
                        DeleteImageFile(LastImg);
                        ImagePath = "";
                    }
                    else if (ImagePath != LastImg)
                    {
                        // a new source image was selected -> copy it and delete the old one
                        string newPath = CopyImageToFolder(ImagePath);
                        DeleteImageFile(LastImg);
                        ImagePath = newPath;
                    }
                    // else: unchanged -> keep ImagePath (== LastImg), no file operations

                    clsSqlPeoples.UpdatePerson(PerID, NationalNub, FirstName, SecondName, ThirdName, LastName
                       , Phone, Email, Nationality, DateOfBirth, Gender, Address, ImagePath);
                    this.LastImg = ImagePath;
                    break;

                default:
                    break;
            }
        }

        // -----------------------------------------------------------------
        //  Image file helpers (ImageCopy lives under the working directory)
        // -----------------------------------------------------------------

        /// <summary>
        /// Copies the selected source image into the ImageCopy folder under a new
        /// GUID-based .png name and returns the new full path. Returns "" when no
        /// source is provided or the source file does not exist.
        /// </summary>
        private static string CopyImageToFolder(string sourceImagePath)
        {
            if (string.IsNullOrEmpty(sourceImagePath) || !File.Exists(sourceImagePath))
                return "";

            string DirectoryPath = Directory.GetCurrentDirectory();
            string ImagePathDirectory = Path.Combine(DirectoryPath, "ImageCopy");
            if (!Directory.Exists(ImagePathDirectory))
                Directory.CreateDirectory(ImagePathDirectory);

            Guid guid = Guid.NewGuid();
            string newFileName = guid.ToString() + ".png";
            string newFilePath = Path.Combine(ImagePathDirectory, newFileName);

            File.Copy(sourceImagePath, newFilePath);
            return newFilePath;
        }

        /// <summary>
        /// Safely deletes an image file (no-op for empty/missing paths).
        /// </summary>
        private static void DeleteImageFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;
            try
            {
                File.Delete(path);
            }
            catch
            {
            }
        }

        public static clsPeoples GetEmptyPerson()
        {
            clsPeoples P=  new clsPeoples(-1 , "", "", "", "", "", "", "", -1, DateTime.Now.AddYears(-19), -1, "", "");
            P.Mode = EnMode.New;
            return P;
        }

        public static clsPeoples GetEmptyPerson2()
        {
            clsPeoples P = new clsPeoples("", "", "", "", "", "", "", -1, DateTime.Now.AddYears(-19), -1, "", "");
            P.Mode = EnMode.New;
            return P;
        }

        public static clsPeoples FindByPersonalID(int PerID)
        {
            string NatNub = ""; string FN = ""; string SN = ""; string TN = "";
            string LN = ""; string Phn = ""; string Em = ""; int Nat = -1; DateTime date = DateTime.Now; int gender = -1;
            string Addr = ""; string Img = "";

            if (clsSqlPeoples.FindByPersonalID(PerID, ref NatNub, ref FN, ref SN, ref TN, ref LN, ref Phn, ref Em,
                ref Nat, ref date, ref gender, ref Addr, ref Img))
            {
                return new clsPeoples(PerID, NatNub, FN, SN, TN, LN, Phn, Em, Nat, date, gender, Addr, Img);
            }
            else
            {
                return GetEmptyPerson();
            }

        }

        public static bool DeletePerson(int PerID)
        {
            // Delete the person's image file (if any) before removing the DB record.
            clsPeoples person = FindByPersonalID(PerID);
            if (person != null)
            {
                DeleteImageFile(person.ImagePath);
            }
            return clsSqlPeoples.DeletePerson(PerID);
        }

        public static DataTable ListPeoples()
        {
            return clsSqlPeoples.ListPeoples();
        }

        public static DataTable ListCountries()
        {
            return clsSqlPeoples.ListCountries();
        }

        public static bool IsNationalNumberExists(string NatNo)
        {
            return clsSqlPeoples.IsNationalNumberExists(NatNo);
        }

        public static string GetGenderFromCode(int code)
        {
            string result = "";
            if (code == 0)
            {
                result = "Male";
            }
            else if (code == 1)
            {
                result = "Female";
            }
            else
            {
                result = "Croissant";
            }
            return result;
        }

        public static string GetCountryFromCode(int code)
        {
            DataTable dt = clsSqlPeoples.ListCountries();
            return dt.Rows[code][1].ToString();
        }

        public static clsPeoples FindPersonByNationalNumber(string No)
        {
            int PerID = -1; string FN = ""; string SN = ""; string TN = "";
            string LN = ""; string Phn = ""; string Em = ""; int Nat = -1; DateTime date = DateTime.Now; int gender = -1;
            string Addr = ""; string Img = "";

            if (clsSqlPeoples.FindByNationalNumber(No , ref PerID, ref FN, ref SN, ref TN, ref LN, ref Phn, ref Em,
                ref Nat, ref date, ref gender, ref Addr, ref Img))
            {
                return new clsPeoples(PerID,No, FN, SN, TN, LN, Phn, Em, Nat, date, gender, Addr, Img);
            }
            else
            {
                return null;
            }
        }

        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }

        public static string GetPersonFullNameByPersonID(int per_ID)
        {
            return clsSqlPeoples.GetPersonFullNameByPersonID(per_ID);
        }

    }

}