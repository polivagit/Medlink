using DbLibrary.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbLibrary.Model
{
    public class PersonDB
    {
        private int pers_id;
        private String pers_nif;
        private String pers_first_name;
        private String pers_last_name_1;
        private String pers_last_name_2;
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

        private static string[] genderTypes = null;

        public PersonDB()
        {

        }

        public PersonDB(int pers_id, string pers_nif, string pers_first_name, string pers_last_name_1, 
            string pers_last_name_2, DateTime pers_birthdate, string pers_phone_number, string pers_email,
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
        public string Pers_nif
        {
            get { return pers_nif; }
            set
            {
                if (IsValidNIF(value))
                {
                    pers_nif = value;
                }
                else
                {
                    throw new ArgumentException("ERROR: Invalid NIF format.");
                }
            }
        }
        public string Pers_first_name { get => pers_first_name; set => pers_first_name = value; }
        public string Pers_last_name_1 { get => pers_last_name_1; set => pers_last_name_1 = value; }
        public string Pers_last_name_2 { get => pers_last_name_2; set => pers_last_name_2 = value; }
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

        public String Pers_Birthdate_Date
        {
            get
            {
                return Pers_birthdate.ToString("dd/MM/yyyy");
            }
        }

        public static string[] GetGenders()
        {
            if (genderTypes == null)
            {
                genderTypes = new string[] { "MALE", "FEMALE", "NON_BINARY", "UNDEFINED" };
            }

            return genderTypes;
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
            return 124075931 + pers_id.GetHashCode();
        }
        #endregion

        #region DB METHODS
        public static ObservableCollection<PersonDB> GetAllCaregivers(String filterNIF, String filterFullName, String filterPhoneNumber, String filterEmail)
        {
            ObservableCollection<PersonDB> caregivers = new ObservableCollection<PersonDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@person_nif", "%" + filterNIF + "%", DbType.String);
                        DBUtils.crearParametre(query, "@person_full_name", "%" + filterFullName + "%", DbType.String);
                        DBUtils.crearParametre(query, "@person_phone_number", "%" + filterPhoneNumber + "%", DbType.String);
                        DBUtils.crearParametre(query, "@person_email", "%" + filterEmail + "%", DbType.String);

                        query.CommandText = @"SELECT pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                    pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                    pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, 
                                                    pers_addrs_country, pers_login_username, pers_login_password
                                                FROM person pe
                                                WHERE  
                                                    NOT EXISTS (SELECT 1 FROM patient WHERE pati_person_id = pers_id)
                                                    AND NOT EXISTS (SELECT 1 FROM doctor WHERE doct_person_id = pers_id)
                                                    AND (@person_nif='' or pers_nif like @person_nif)
                                                    AND (@person_full_name='' or CONCAT(pers_first_name,pers_last_name_1,pers_last_name_2) like @person_full_name)
                                                    AND (@person_phone_number='' or pers_phone_number like @person_phone_number)
                                                    AND (@person_email='' or pers_email like @person_email)";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int pers_id = reader.GetInt32(reader.GetOrdinal("pers_id"));
                            String pers_nif = DBUtils.readDBC<String>(reader, "pers_nif");
                            String pers_first_name = DBUtils.readDBC<String>(reader, "pers_first_name");
                            String pers_last_name_1 = DBUtils.readDBC<String>(reader, "pers_last_name_1");

                            String pers_last_name_2 = null;
                            if (reader["pers_last_name_2"] != DBNull.Value)
                            {
                                pers_last_name_2 = DBUtils.readDBC<String>(reader, "pers_last_name_2");
                            }

                            DateTime pers_birthdate = reader.GetDateTime(reader.GetOrdinal("pers_birthdate"));
                            String pers_phone_number = DBUtils.readDBC<String>(reader, "pers_phone_number");
                            String pers_email = DBUtils.readDBC<String>(reader, "pers_email");
                            //int pers_gender = reader.GetInt32(reader.GetOrdinal("pers_gender"));
                            GenderTypeDB pers_gender = (GenderTypeDB)reader.GetValue(reader.GetOrdinal("pers_gender"));
                            String pers_addrs_street = DBUtils.readDBC<String>(reader, "pers_addrs_street");
                            String pers_addrs_zip_code = DBUtils.readDBC<String>(reader, "pers_addrs_zip_code");
                            String pers_addrs_city = DBUtils.readDBC<String>(reader, "pers_addrs_city");
                            String pers_addrs_province = DBUtils.readDBC<String>(reader, "pers_addrs_province");
                            String pers_addrs_country = DBUtils.readDBC<String>(reader, "pers_addrs_country");
                            String pers_login_username = DBUtils.readDBC<String>(reader, "pers_login_username");
                            String pers_login_password = DBUtils.readDBC<String>(reader, "pers_login_password");

                            PersonDB caregiverAux = new PersonDB(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province,
                                                pers_addrs_country, pers_login_username, pers_login_password);
                            caregivers.Add(caregiverAux);
                        }
                    }
                }
            }
            return caregivers;
        }

        public static PersonDB GetCaregiverById(int caregiverId)
        {
            PersonDB caregiverAux = new PersonDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@c_id", caregiverId, DbType.String);

                        query.CommandText = @"SELECT pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                    pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                    pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, 
                                                    pers_addrs_country, pers_login_username, pers_login_password
                                                FROM person pe
                                                WHERE  
                                                    NOT EXISTS (SELECT 1 FROM patient pa WHERE pa.pati_person_id = pe.pers_id)
                                                    AND NOT EXISTS (SELECT 1 FROM doctor doc WHERE doc.doct_person_id = pe.pers_id)
                                                    AND pers_id = @c_id";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int pers_id = reader.GetInt32(reader.GetOrdinal("pers_id"));
                            String pers_nif = DBUtils.readDBC<String>(reader, "pers_nif");
                            String pers_first_name = DBUtils.readDBC<String>(reader, "pers_first_name");
                            String pers_last_name_1 = DBUtils.readDBC<String>(reader, "pers_last_name_1");

                            String pers_last_name_2 = null;
                            if (reader["pers_last_name_2"] != DBNull.Value)
                            {
                                pers_last_name_2 = DBUtils.readDBC<String>(reader, "pers_last_name_2");
                            }

                            DateTime pers_birthdate = reader.GetDateTime(reader.GetOrdinal("pers_birthdate"));
                            String pers_phone_number = DBUtils.readDBC<String>(reader, "pers_phone_number");
                            String pers_email = DBUtils.readDBC<String>(reader, "pers_email");
                            GenderTypeDB pers_gender = (GenderTypeDB)reader.GetValue(reader.GetOrdinal("pers_gender"));
                            String pers_addrs_street = DBUtils.readDBC<String>(reader, "pers_addrs_street");
                            String pers_addrs_zip_code = DBUtils.readDBC<String>(reader, "pers_addrs_zip_code");
                            String pers_addrs_city = DBUtils.readDBC<String>(reader, "pers_addrs_city");
                            String pers_addrs_province = DBUtils.readDBC<String>(reader, "pers_addrs_province");
                            String pers_addrs_country = DBUtils.readDBC<String>(reader, "pers_addrs_country");
                            String pers_login_username = DBUtils.readDBC<String>(reader, "pers_login_username");
                            String pers_login_password = DBUtils.readDBC<String>(reader, "pers_login_password");

                            caregiverAux = new PersonDB(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                        pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                        pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province,
                                        pers_addrs_country, pers_login_username, pers_login_password);;
                        }
                    }
                }
            }
            return caregiverAux;
        }
        #endregion

        #region VERIFICATION METHODS
        public static bool IsValidNIF(string value)
        {
            string pattern = "^[0-9]{8}[A-Z]{1}$";
            return Regex.IsMatch(value, pattern);
        }

        public static bool IsValidFirstName(string first_name)
        {
            if ((string.IsNullOrWhiteSpace(first_name) || string.IsNullOrEmpty(first_name)) || (first_name.Length <= 0) || (first_name.Length >= 50))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidLastName1(string last_name_1)
        {
            if ((string.IsNullOrWhiteSpace(last_name_1) || string.IsNullOrEmpty(last_name_1)) || (last_name_1.Length <= 0) || (last_name_1.Length >= 50))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidLastName2(string last_name_2)
        {
            if (!(string.IsNullOrWhiteSpace(last_name_2) || string.IsNullOrEmpty(last_name_2)) && (last_name_2.Length <= 0) || (last_name_2.Length >= 50))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidBirthdate(DateTime birthdate)
        {
            DateTime startDate = new DateTime(1900, 1, 1);
            DateTime currentDate = DateTime.Now;

            return birthdate >= startDate && birthdate <= currentDate;
        }

        public static bool isValidPhoneNumber(string phone_number)
        {
            string pattern = @"^\d{9}$";
            return Regex.IsMatch(phone_number, pattern);
        }

        public static bool isValidEmail(string email)
        {
            if ((string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email)) || (email.Length <= 4) || (email.Length >= 50))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isValidStreet(string addrs_street)
        {
            if ((string.IsNullOrWhiteSpace(addrs_street) || string.IsNullOrEmpty(addrs_street)) || (addrs_street.Length <= 4) || (addrs_street.Length >= 100))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isValidAddrsZipCode(string addrs_zip_code)
        {
            if ((string.IsNullOrWhiteSpace(addrs_zip_code) || string.IsNullOrEmpty(addrs_zip_code)) || (addrs_zip_code.Length <= 4) || (addrs_zip_code.Length >= 10))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isValidAddrsCity(string addrs_city)
        {
            if ((string.IsNullOrWhiteSpace(addrs_city) || string.IsNullOrEmpty(addrs_city)) || (addrs_city.Length <= 4) || (addrs_city.Length >= 50))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isValidAddrsProvince(string addrs_province)
        {
            if ((string.IsNullOrWhiteSpace(addrs_province) || string.IsNullOrEmpty(addrs_province)) || (addrs_province.Length <= 4) || (addrs_province.Length >= 50))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isValidAddrsCountry(string addrs_country)
        {
            if ((string.IsNullOrWhiteSpace(addrs_country) || string.IsNullOrEmpty(addrs_country)) || (addrs_country.Length <= 4) || (addrs_country.Length >= 50))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isValidLoginUsername(string login_username)
        {
            if ((string.IsNullOrWhiteSpace(login_username) || string.IsNullOrEmpty(login_username)) || (login_username.Length <= 2) || (login_username.Length >= 40))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
