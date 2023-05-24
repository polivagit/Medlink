using DBLib.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class PatientDB : PersonDB
    {
        private int pati_height;
        private float pati_weight;
        private String? pati_remarks;
        private int? pati_caregiver_id;

        public PatientDB(int pers_id, string pers_nif, string pers_first_name, string pers_last_name_1, string? pers_last_name_2,
            DateTime pers_birthdate, string pers_phone_number, string pers_email, GenderTypeDB pers_gender, 
            string pers_addrs_street, string pers_addrs_zip_code, string pers_addrs_city, string pers_addrs_province,
            string pers_addrs_country, string pers_login_username, string pers_login_password, int pati_height, 
            float pati_weight, string? pati_remarks, int? pati_caregiver_id) 
            : base(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2, pers_birthdate, pers_phone_number, 
                  pers_email, pers_gender, pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, 
                  pers_addrs_province, pers_addrs_country, pers_login_username, pers_login_password)
        {
            this.Pati_height = pati_height;
            this.Pati_weight = pati_weight;
            this.Pati_remarks = pati_remarks;
            this.Pati_caregiver_id = pati_caregiver_id;
        }

        public int Pati_height { get => pati_height; set => pati_height = value; }
        public float Pati_weight { get => pati_weight; set => pati_weight = value; }
        public string? Pati_remarks { get => pati_remarks; set => pati_remarks = value; }
        public int? Pati_caregiver_id { get => pati_caregiver_id; set => pati_caregiver_id = value; }

        #region DB METHODS
        private delegate T callable_query<T>(DbCommand consulta);

        private static T launchQuery<T>(callable_query<T> cq)
        {
            using (MySQLConnDbContext context = new MySQLConnDbContext()) //crea el contexte de la base de dades
            {
                using (DbConnection connection = context.Database.GetDbConnection()) //pren la conexxio de la BD
                {
                    connection.Open();
                    using (DbCommand consulta = connection.CreateCommand())
                    {
                        return cq(consulta);
                    }
                }
            }
        }

        public static ObservableCollection<PatientDB> GetPatients()
        {
            ObservableCollection<PatientDB> patients = new ObservableCollection<PatientDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = @"SELECT pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, 
                                                pers_addrs_country, pers_login_username, pers_login_password, pati_height, pati_weight,
                                                pati_remarks, pati_caregiver_id
                                                FROM person RIGHT JOIN patient ON pers_id = pati_person_id";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int pers_id = reader.GetInt32(reader.GetOrdinal("pers_id"));
                            String pers_nif = DBUtils.readDBC<String>(reader, "pers_nif");
                            String pers_first_name = DBUtils.readDBC<String>(reader, "pers_first_name");
                            String pers_last_name_1 = DBUtils.readDBC<String>(reader, "pers_last_name_1");
                            String pers_last_name_2 = DBUtils.readDBC<String>(reader, "pers_last_name_2");
                            DateTime pers_birthdate = reader.GetDateTime(reader.GetOrdinal("pers_birthdate"));
                            String pers_phone_number = DBUtils.readDBC<String>(reader, "pers_phone_number");
                            String pers_email = DBUtils.readDBC<String>(reader, "pers_email");
                            //int pers_gender = reader.GetInt32(reader.GetOrdinal("pers_gender"));
                            GenderTypeDB pers_gender = (GenderTypeDB)reader.GetValue(reader.GetOrdinal("pers_gender"));
                            String pers_addrs_street = DBUtils.readDBC<String>(reader, "pers_addrs_street");
                            String pers_addrs_zip_code = DBUtils.readDBC<String>(reader, "pers_addrs_zip_code");
                            String pers_addrs_city = DBUtils.readDBC<String>(reader, "pers_addrs_city");
                            String pers_addrs_province = DBUtils.readDBC<String>(reader, "pers_addrs_province");
                            String pers_addrs_country = DBUtils.readDBC<String>(reader, "pers_addrs_zip_code");
                            String pers_login_username = DBUtils.readDBC<String>(reader, "pers_addrs_city");
                            String pers_login_password = DBUtils.readDBC<String>(reader, "pers_addrs_province");
                            int pati_height = reader.GetInt32(reader.GetOrdinal("pati_height"));
                            float pati_weight = reader.GetFloat(reader.GetOrdinal("pati_weight"));
                            String pati_remarks = DBUtils.readDBC<String>(reader, "pati_remarks");
                            int pati_caregiver_id = reader.GetInt32(reader.GetOrdinal("pati_caregiver_id"));

                            PatientDB patientAux = new PatientDB(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2, 
                                                pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province,
                                                pers_addrs_country, pers_login_username, pers_login_password, pati_height, pati_weight,
                                                pati_remarks, pati_caregiver_id);
                            patients.Add(patientAux);
                        }
                    }
                }
            }
            return patients;
        }
        #endregion
    }
}
