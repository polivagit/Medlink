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
        private String? pers_last_name_2;
        private DateTime pers_birthdate;
        private String pers_phone_number;
        private String pers_email;
        private GenderTypeDB pers_gender;
        private String pers_addrs_street;
        private String pers_addrs_zip_code;
        private String pers_addrs_city;
        private String pers_addrs_province;
        private String pers_addrs_country;
        private String pers_login_username;
        private String pers_login_password;

        public PersonDB()
        {

        }

        public PersonDB(int pers_id, string pers_nif, string pers_first_name, string pers_last_name_1, 
            string? pers_last_name_2, DateTime pers_birthdate, string pers_phone_number, string pers_email,
            GenderTypeDB pers_gender, string pers_addrs_street, string pers_addrs_zip_code, string pers_addrs_city, 
            string pers_addrs_province, string pers_addrs_country, 
            string pers_login_username, string pers_login_password)
        {
            this.Pers_id = pers_id;
            this.Pers_nif = pers_nif;
            this.pers_first_name = pers_first_name;
            this.Pers_last_name_1 = pers_last_name_1;
            this.pers_last_name_2 = pers_last_name_2;
            this.Pers_birthdate = pers_birthdate;
            this.Pers_phone_number = pers_phone_number;
            this.Pers_email = pers_email;
            this.Pers_gender = pers_gender;
            this.Pers_addrs_street = pers_addrs_street;
            this.Pers_addrs_zip_code = pers_addrs_zip_code;
            this.Pers_addrs_city = pers_addrs_city;
            this.Pers_addrs_province = pers_addrs_province;
            this.Pers_addrs_country = pers_addrs_country;
            this.Pers_login_username = pers_login_username;
            this.Pers_login_password = pers_login_password;
        }

        public int Pers_id { get => pers_id; set => pers_id = value; }
        public string Pers_nif { get => pers_nif; set => pers_nif = value; }
        public string Pers_first_name { get => pers_first_name; set => pers_first_name = value; }
        public string Pers_last_name_1 { get => Pers_last_name_1; set => Pers_last_name_1 = value; }
        public string? Pers_last_name_2 { get => Pers_last_name_2; set => Pers_last_name_2 = value; }
        public DateTime Pers_birthdate { get => pers_birthdate; set => pers_birthdate = value; }
        public string Pers_phone_number { get => pers_phone_number; set => pers_phone_number = value; }
        public string Pers_email { get => pers_email; set => pers_email = value; }
        public GenderTypeDB Pers_gender { get => pers_gender; set => pers_gender = value; }
        public string Pers_addrs_street { get => pers_addrs_street; set => pers_addrs_street = value; }
        public string Pers_addrs_zip_code { get => pers_addrs_zip_code; set => pers_addrs_zip_code = value; }
        public string Pers_addrs_city { get => pers_addrs_city; set => pers_addrs_city = value; }
        public string Pers_addrs_province { get => pers_addrs_province; set => pers_addrs_province = value; }
        public string Pers_addrs_country { get => pers_addrs_country; set => pers_addrs_country = value; }
        public string Pers_login_username { get => pers_login_username; set => pers_login_username = value; }
        public string Pers_login_password { get => pers_login_password; set => pers_login_password = value; }

        #region EXTRA_PROPERTIES
        public String Pers_FullName
        {
            get
            {
                return Pers_last_name_1+" "+Pers_last_name_2+", "+Pers_first_name;
            }
        }
        #endregion

        #region EQUALS & HASHCODE
        public override bool Equals(object obj)
        {
            return obj is PersonDB dB &&
                   pers_id == dB.pers_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(pers_id);
        }
        #endregion
    }
}
