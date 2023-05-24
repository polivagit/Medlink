using DbLibrary.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbLibrary.Model
{
    public class DoctorDB : PersonDB
    {
        private String pers_doct_collegiate_uid;
        private int pers_doct_specialty_id;

        public DoctorDB()
        {
        }

        public DoctorDB(int pers_id, string pers_nif, string pers_first_name, string pers_last_name_1, string pers_last_name_2, DateTime pers_birthdate,
            string pers_phone_number, string pers_email, GenderTypeDB pers_gender, string pers_addrs_street, string pers_addrs_zip_code, string pers_addrs_city, 
            string pers_addrs_province, string pers_addrs_country, string pers_login_username, string pers_login_password, string pers_doct_collegiate_uid, int pers_doct_specialty_id) 
            : base(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2, pers_birthdate, pers_phone_number, pers_email, pers_gender, 
                  pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, pers_addrs_country, pers_login_username, pers_login_password)
        {
            this.Pers_doct_collegiate_uid = pers_doct_collegiate_uid;
            this.Pers_doct_specialty_id = pers_doct_specialty_id;
        }

        public string Pers_doct_collegiate_uid { get => pers_doct_collegiate_uid; set => pers_doct_collegiate_uid = value; }
        public int Pers_doct_specialty_id { get => pers_doct_specialty_id; set => pers_doct_specialty_id = value; }

        #region EXTRA PROPERTIES
        public String Pers_DoctorSpecialtyName
        {
            get
            {
                string name = SpecialtyDB.GetSpecialtyById(Pers_doct_specialty_id).Spec_name;
                return name.Substring(0, 1).ToUpper() + name.Substring(1);
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

        public static DoctorDB GetDoctorById(int doctorId)
        {
            DoctorDB doctorAux = new DoctorDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@doctor_id", doctorId, DbType.Int32);

                        query.CommandText = @"SELECT pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
		                                            pers_birthdate, pers_phone_number, pers_email, pers_gender,
		                                            pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province, 
		                                            pers_addrs_country, pers_login_username, pers_login_password, doct_collegiate_uid, doct_specialty_id
                                            FROM person RIGHT JOIN doctor ON pers_id = doct_person_id
                                            WHERE pers_id = @doctor_id;";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            /*
                                TODO:
                                    - CHECK IF PERS_LAST_NAME_2 IS NULL
                             */

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
                            String pers_addrs_country = DBUtils.readDBC<String>(reader, "pers_addrs_country");
                            String pers_login_username = DBUtils.readDBC<String>(reader, "pers_login_username");
                            String pers_login_password = DBUtils.readDBC<String>(reader, "pers_login_password");
                            String doct_collegiate_uid = DBUtils.readDBC<String>(reader, "doct_collegiate_uid");
                            int doct_specialty_id = reader.GetInt32(reader.GetOrdinal("doct_specialty_id"));

                            doctorAux = new DoctorDB(pers_id, pers_nif, pers_first_name, pers_last_name_1, pers_last_name_2,
                                                pers_birthdate, pers_phone_number, pers_email, pers_gender,
                                                pers_addrs_street, pers_addrs_zip_code, pers_addrs_city, pers_addrs_province,
                                                pers_addrs_country, pers_login_username, pers_login_password, doct_collegiate_uid, doct_specialty_id);
                        }
                    }
                }
            }
            return doctorAux;
        }


        public static bool UpdateDoctor(DoctorDB doctorAux)
        {
            /*
                1. UPDATE person SET ... WHERE pers_id=@d_id;
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
                            `doct_person_id`, `doct_collegiate_uid`, `doct_specialty_id`
                            */

                            // PERSON TABLE FIELDS
                            DBUtils.crearParametre(consulta, "@d_id", doctorAux.Pers_id, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@d_first_name", doctorAux.Pers_first_name, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_last_name_1", doctorAux.Pers_last_name_1, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_last_name_2", doctorAux.Pers_last_name_2, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_phone_number", doctorAux.Pers_phone_number, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_email", doctorAux.Pers_email, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_gender", doctorAux.Pers_gender, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@d_pers_addrs_street", doctorAux.Pers_addrs_street, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_pers_addrs_zip_code", doctorAux.Pers_addrs_zip_code, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_pers_addrs_city", doctorAux.Pers_addrs_city, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_pers_addrs_province", doctorAux.Pers_addrs_province, DbType.String);
                            DBUtils.crearParametre(consulta, "@d_pers_addrs_country", doctorAux.Pers_addrs_country, DbType.String);

                            consulta.CommandText = @"UPDATE person SET pers_first_name = @d_first_name,
                                                                    pers_last_name_1 = @d_last_name_1,
                                                                    pers_last_name_2 = @d_last_name_2,
                                                                    pers_phone_number = @d_phone_number,
                                                                    pers_email = @d_email,
                                                                    pers_gender = @d_gender,
                                                                    pers_addrs_street = @d_pers_addrs_street,
                                                                    pers_addrs_zip_code = @d_pers_addrs_zip_code,
                                                                    pers_addrs_city = @d_pers_addrs_city,
                                                                    pers_addrs_province = @d_pers_addrs_province,
                                                                    pers_addrs_country = @d_pers_addrs_country
                                                    WHERE pers_id = @d_id";

                            int numUpdatedPersRows = consulta.ExecuteNonQuery();
                            if (numUpdatedPersRows != 1)
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
        #endregion
    }
}
