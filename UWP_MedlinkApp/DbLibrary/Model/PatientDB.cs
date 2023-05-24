using DbLibrary.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLibrary.Model
{
    public class PatientDB : PersonDB
    {
        private int pati_height;
        private float pati_weight;
        private String pati_remarks;
        private int pati_caregiver_id;

        public PatientDB()
        {

        }

        public PatientDB(int pers_id, string pers_nif, string pers_first_name, string pers_last_name_1, string pers_last_name_2,
            DateTime pers_birthdate, string pers_phone_number, string pers_email, GenderTypeDB pers_gender, 
            string pers_addrs_street, string pers_addrs_zip_code, string pers_addrs_city, string pers_addrs_province,
            string pers_addrs_country, string pers_login_username, string pers_login_password, int pati_height, 
            float pati_weight, string pati_remarks, int pati_caregiver_id) 
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
        public string Pati_remarks { get => pati_remarks; set => pati_remarks = value; }
        public int Pati_caregiver_id { get => pati_caregiver_id; set => pati_caregiver_id = value; }

        #region EXTRA_PROPERTIES
        public String Pers_Caregiver
        {
            get
            {
                if (Pati_caregiver_id != -1)
                {
                    return PatientDB.GetPatientById(Pati_caregiver_id).Pers_FullName;
                }
                else
                {
                    return "NO CAREGIVER";
                }
                
            }
        }
        #endregion

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

        public static ObservableCollection<PatientDB> GetAllPatients(String filterFullName)
        {
            ObservableCollection<PatientDB> patients = new ObservableCollection<PatientDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@person_full_name", "%" + filterFullName + "%", DbType.String);

                        query.CommandText = @"SELECT pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                    pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                    pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, 
                                                    pers_addrs_country, pers_login_username, pers_login_password, pati_height, pati_weight,
                                                    pati_remarks, pati_caregiver_id
                                                FROM person RIGHT JOIN patient ON pers_id = pati_person_id
                                                WHERE (@person_full_name='' or CONCAT(pers_first_name,pers_last_name_1,pers_last_name_2) like @person_full_name)";

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
                            int pati_height = reader.GetInt32(reader.GetOrdinal("pati_height"));
                            float pati_weight = reader.GetFloat(reader.GetOrdinal("pati_weight"));

                            String pati_remarks = null;
                            if (reader["pati_remarks"] != DBNull.Value)
                            {
                                pati_remarks = DBUtils.readDBC<String>(reader, "pati_remarks");
                            }

                            int pati_caregiver_id = -1;
                            if (reader["pati_caregiver_id"] != DBNull.Value)
                            {
                                pati_caregiver_id = reader.GetInt32(reader.GetOrdinal("pati_caregiver_id"));
                            }

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

        public static PatientDB GetPatientById(int patientId)
        {
            PatientDB patientAux = new PatientDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@patient_id", patientId, DbType.Int32);

                        query.CommandText = @"SELECT pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                    pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                    pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, 
                                                    pers_addrs_country, pers_login_username, pers_login_password, pati_height, pati_weight,
                                                    pati_remarks, pati_caregiver_id
                                                FROM person RIGHT JOIN patient ON pers_id = pati_person_id
                                                WHERE pers_id = @patient_id;";

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
                            int pati_height = reader.GetInt32(reader.GetOrdinal("pati_height"));
                            float pati_weight = reader.GetFloat(reader.GetOrdinal("pati_weight"));

                            String pati_remarks = null;
                            if (reader["pati_remarks"] != DBNull.Value)
                            {
                                pati_remarks = DBUtils.readDBC<String>(reader, "pati_remarks");
                            }

                            int pati_caregiver_id = -1;
                            if (reader["pati_caregiver_id"] != DBNull.Value)
                            {
                                pati_caregiver_id = reader.GetInt32(reader.GetOrdinal("pati_caregiver_id"));
                            }

                            patientAux = new PatientDB(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province,
                                                pers_addrs_country, pers_login_username, pers_login_password, pati_height, pati_weight,
                                                pati_remarks, pati_caregiver_id);
                        }
                    }
                }
            }
            return patientAux;
        }

        public static bool UpdatePatient(PatientDB patientAux)
        {
            /*
                1. UPDATE person SET ... WHERE pers_id=@p_id;

                2. UPDATE patient SET ... WHERE pati_person_id=@p_id;
            */

            bool success = true;

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (DbConnection connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        using (DbCommand consulta = connection.CreateCommand())
                        {
                            consulta.Transaction = transaction;

                            /*
                             `pers_nif`,`pers_first_name`,`pers_last_name_1`,`pers_last_name_2`,`pers_birthdate`,
                            `pers_phone_number`,`pers_email`,`pers_gender`,`pers_addrs_street`,`pers_addrs_zip_code`,
                            `pers_addrs_city`,`pers_addrs_province`,`pers_addrs_country`,`pers_login_username`,`pers_login_password`,
                            `pati_person_id`, `pati_height`, `pati_weight`, `pati_remarks`, `pati_caregiver_id`
                            */

                            // PERSON TABLE FIELDS
                            DBUtils.crearParametre(consulta, "@p_id", patientAux.Pers_id, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@p_nif", patientAux.Pers_nif, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_first_name", patientAux.Pers_first_name, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_last_name_1", patientAux.Pers_last_name_1, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_last_name_2", patientAux.Pers_last_name_2, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_birthdate", patientAux.Pers_birthdate, DbType.DateTime);
                            DBUtils.crearParametre(consulta, "@p_phone_number", patientAux.Pers_phone_number, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_email", patientAux.Pers_email, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_gender", patientAux.Pers_gender, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@p_pers_addrs_street", patientAux.Pers_addrs_street, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_pers_addrs_zip_code", patientAux.Pers_addrs_zip_code, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_pers_addrs_city", patientAux.Pers_addrs_city, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_pers_addrs_province", patientAux.Pers_addrs_province, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_pers_addrs_country", patientAux.Pers_addrs_country, DbType.String);
                            DBUtils.crearParametre(consulta, "@p_pers_login_username", patientAux.Pers_login_username, DbType.String);
                            //DBUtils.crearParametre(consulta, "@p_pers_addrs_country", patientAux.Pers_login_password, DbType.String);

                            // PATIENT TABLE FIELDS
                            DBUtils.crearParametre(consulta, "@p_pers_pati_height", patientAux.Pati_height, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@p_pers_pati_weight", patientAux.Pati_weight, DbType.Double);
                            DBUtils.crearParametre(consulta, "@p_pers_pati_remarks", patientAux.Pati_remarks, DbType.String);
                            //DBUtils.crearParametre(consulta, "@p_pers_pati_caregiver_id", patientAux.Pati_caregiver_id, DbType.Int32);

                            consulta.CommandText = @"UPDATE person SET pers_nif = @p_nif,
                                                                    pers_first_name = @p_first_name,
                                                                    pers_last_name_1 = @p_last_name_1,
                                                                    pers_last_name_2 = @p_last_name_2,
                                                                    pers_birthdate = @p_birthdate,
                                                                    pers_phone_number = @p_phone_number,
                                                                    pers_email = @p_email,
                                                                    pers_gender = @p_gender,
                                                                    pers_addrs_street = @p_pers_addrs_street,
                                                                    pers_addrs_zip_code = @p_pers_addrs_zip_code,
                                                                    pers_addrs_city = @p_pers_addrs_city,
                                                                    pers_addrs_province = @p_pers_addrs_province,
                                                                    pers_addrs_country = @p_pers_addrs_country,
                                                                    pers_login_username = @p_pers_login_username
                                                    WHERE pers_id = @p_id";

                            int numUpdatedPersRows = consulta.ExecuteNonQuery();
                            if (numUpdatedPersRows != 1)
                            {
                                transaction.Rollback();
                                success = false;
                            }

                            consulta.CommandText = @"UPDATE patient SET pati_height = @p_pers_pati_height,
                                                                    pati_weight = @p_pers_pati_weight,
                                                                    pati_remarks = @p_pers_pati_remarks
                                                    WHERE pati_person_id = @p_id";

                            int numUpdatedPatiRows = consulta.ExecuteNonQuery();
                            if (numUpdatedPatiRows != 1)
                            {
                                transaction.Rollback();
                                success = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR" + ex);
                    }

                    transaction.Commit();
                    return success;
                }
            }
        }

        public static bool DeletePatientById(int patientId)
        {
            /*
                1. DELETE FROM patient WHERE pati_person_id = 4;

                2. DELETE FROM person WHERE pers_id = 4;
            */

            bool success = true;
            using (MySQLConnDbContext context = new MySQLConnDbContext()) // CREATE DB CONTEXT
            {
                using (DbConnection connection = context.Database.GetDbConnection()) // GET DB CONNECTION
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction(); // CREATE TRANSACTION

                    using (DbCommand query = connection.CreateCommand())
                    {
                        query.Transaction = transaction; // SET QUERY IN TRANSACTION

                        DBUtils.crearParametre(query, "@patient_id", patientId, DbType.Int32);

                        // DELETE FROM PATIENT TABLE =========================
                        query.CommandText = "SELECT COUNT(1) FROM patient WHERE pati_person_id = @patient_id";

                        long numPatients = (long)query.ExecuteScalar();
                        if (numPatients < 0) return false;

                        query.CommandText = "DELETE FROM patient WHERE pati_person_id = @patient_id";

                        int numPatiDeleted = query.ExecuteNonQuery();
                        if (numPatiDeleted != 1)
                        {
                            transaction.Rollback();
                            success = false;
                        }
                        // ===================================================

                        // DELETE FROM PERSON TABLE ==========================
                        query.CommandText = "SELECT COUNT(1) FROM person WHERE pers_id = @patient_id";

                        long numPerson = (long)query.ExecuteScalar();
                        if (numPerson < 0) return false;

                        query.CommandText = "DELETE FROM person WHERE pers_id = @patient_id";

                        int numPersDeleted = query.ExecuteNonQuery();
                        if (numPersDeleted != 1)
                        {
                            transaction.Rollback();
                            success = false;
                        }
                        // ===================================================

                        transaction.Commit();
                        return success;
                    }
                }
            }
        }
        #endregion
    }
}
