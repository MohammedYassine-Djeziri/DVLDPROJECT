using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            this.Mode = EnMode.Update;
        }

        public void Save()
        {
            switch (Mode)
            {
                case EnMode.New:
                    this.PerID = clsSqlPeoples.AddPerson(NationalNub, FirstName, SecondName, ThirdName, LastName
                        , Phone, Email, Nationality, DateOfBirth, Gender, Address, ImagePath);
                    this.Mode = EnMode.Update;
                    break;
                case EnMode.Update:
                    //call function Update New from Access layer
                    clsSqlPeoples.UpdatePerson(PerID, NationalNub, FirstName, SecondName, ThirdName, LastName
                       , Phone, Email, Nationality, DateOfBirth, Gender, Address, ref ImagePath, ref LastImg);
                    break;
                default:
                    break;

            }
        }


        public static void FindByNationalNumber(string nationalNumber)
        {

        }

        public static clsPeoples GetEmptyPerson()
        {
            return new clsPeoples(-1 , "", "", "", "", "", "", "", -1, DateTime.Now.AddYears(-19), -1, "", "");
        }

        public static clsPeoples GetEmptyPerson2()
        {
            return new clsPeoples( "", "", "", "", "", "", "", -1, DateTime.Now.AddYears(-19), -1, "", "");
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