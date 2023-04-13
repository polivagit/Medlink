using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public abstract class PersonDB
    {
        private int pers_id;
        private String pers_nif;
        private String pers_first_name;
        private String pers_last_name_1;
        private String pers_last_name_2;    //nullable
        private DateTime pers_birthdate;
        private String pers_phone_number;
        private String pers_email;
        private GenderDB pers_gender;
        private String pers_address;
        private String pers_login_username;
        private String pers_login_password;
        private bool pers_is_caregiver;
        private PersonTypeDB pers_type;

        public PersonDB()
        {
        }

        protected PersonDB(int pers_id, String pers_nif, String pers_first_name, String pers_last_name_1, String pers_last_name_2, 
            DateTime pers_birthdate, String pers_phone_number, String pers_email, GenderDB pers_gender, String pers_address, 
            String pers_login_username, String pers_login_password, bool pers_is_caregiver, PersonTypeDB pers_type)
        {
            this.Pers_id = pers_id;
            this.Pers_nif = pers_nif;
            this.Pers_first_name = pers_first_name;
            this.Pers_last_name_1 = pers_last_name_1;
            this.Pers_last_name_2 = pers_last_name_2;
            this.Pers_birthdate = pers_birthdate;
            this.Pers_phone_number = pers_phone_number;
            this.Pers_email = pers_email;
            this.Pers_gender = pers_gender;
            this.Pers_address = pers_address;
            this.Pers_login_username = pers_login_username;
            this.Pers_login_password = pers_login_password;
            this.Pers_is_caregiver = pers_is_caregiver;
            this.Pers_type = pers_type;
        }

        public int Pers_id { get => pers_id; set => pers_id = value; }
        public String Pers_nif { get => pers_nif; set => pers_nif = value; }
        public String Pers_first_name { get => pers_first_name; set => pers_first_name = value; }
        public String Pers_last_name_1 { get => pers_last_name_1; set => pers_last_name_1 = value; }
        public String Pers_last_name_2 { get => pers_last_name_2; set => pers_last_name_2 = value; }
        public DateTime Pers_birthdate { get => pers_birthdate; set => pers_birthdate = value; }
        public String Pers_phone_number { get => pers_phone_number; set => pers_phone_number = value; }
        public String Pers_email { get => pers_email; set => pers_email = value; }
        public GenderDB Pers_gender { get => pers_gender; set => pers_gender = value; }
        public String Pers_address { get => pers_address; set => pers_address = value; }
        public String Pers_login_username { get => pers_login_username; set => pers_login_username = value; }
        public String Pers_login_password { get => pers_login_password; set => pers_login_password = value; }
        public bool Pers_is_caregiver { get => pers_is_caregiver; set => pers_is_caregiver = value; }
        public PersonTypeDB Pers_type { get => pers_type; set => pers_type = value; }

        #region EXTRA PROPERTIES
        public String Pers_FullName
        {
            get
            {
                return Pers_first_name +" "+ Pers_last_name_1 +" "+ Pers_last_name_2;
            }
        }

        public String Pers_SpecialtyName
        {
            get
            {
                return "";
            }
        }
        #endregion

        public override bool Equals(object obj)
        {
            return obj is PersonDB dB &&
                   pers_id == dB.pers_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(pers_id);
        }
    }
}
