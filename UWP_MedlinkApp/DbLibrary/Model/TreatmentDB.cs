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
    public class TreatmentDB
    {
        private int trea_id;
        private String trea_name;
        private String trea_description;
        private DateTime trea_date_start;
        private DateTime trea_date_end;
        private String trea_observations;
        private bool trea_is_active;
        private int trea_doctor_id;
        private int trea_patient_id;

        public TreatmentDB()
        {
        }

        public TreatmentDB(int trea_id, string trea_name, string trea_description,
            DateTime trea_date_start, DateTime trea_date_end, string trea_observations,
            bool trea_is_active, int trea_doctor_id, int trea_patient_id)
        {
            this.Trea_id = trea_id;
            this.Trea_name = trea_name;
            this.Trea_description = trea_description;
            this.Trea_date_start = trea_date_start;
            this.Trea_date_end = trea_date_end;
            this.Trea_observations = trea_observations;
            this.Trea_is_active = trea_is_active;
            this.Trea_doctor_id = trea_doctor_id;
            this.Trea_patient_id = trea_patient_id;
        }

        public int Trea_id { get => trea_id; set => trea_id = value; }
        public string Trea_name { get => trea_name; set => trea_name = value; }
        public string Trea_description { get => trea_description; set => trea_description = value; }
        public DateTime Trea_date_start { get => trea_date_start; set => trea_date_start = value; }
        public DateTime Trea_date_end { get => trea_date_end; set => trea_date_end = value; }
        public string Trea_observations { get => trea_observations; set => trea_observations = value; }
        public bool Trea_is_active { get => trea_is_active; set => trea_is_active = value; }
        public int Trea_doctor_id { get => trea_doctor_id; set => trea_doctor_id = value; }
        public int Trea_patient_id { get => trea_patient_id; set => trea_patient_id = value; }

        #region EXTRA PROPERTIES
        public String Trea_DateStart_Date
        {
            get
            {
                return Trea_date_start.ToString("dd/MM/yyyy");

            }
        }

        public String Trea_DateEnd_Date
        {
            get
            {
                return Trea_date_end.ToString("dd/MM/yyyy");

            }
        }

        public String Trea_IsActive
        {
            get
            {
                if (Trea_is_active)
                {
                    return "Active";
                }
                else
                {
                    return "Inactive";
                }

            }
        }
        #endregion

        #region EQUALS & HASHCODE
        public override bool Equals(object obj)
        {
            return obj is TreatmentDB dB &&
                   trea_id == dB.trea_id;
        }

        public override int GetHashCode()
        {
            return -22530745 + trea_id.GetHashCode();
        }
        #endregion

        #region DB METHODS
        public static ObservableCollection<TreatmentDB> GetAllTreatmentsByPatientAndDoctorId(String filterName, int patientId, int doctorId)
        {
            ObservableCollection<TreatmentDB> treatments = new ObservableCollection<TreatmentDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@treatment_name", "%" + filterName + "%", DbType.String);
                        DBUtils.crearParametre(query, "@patient_id", patientId, DbType.Int32);
                        DBUtils.crearParametre(query, "@doctor_id", doctorId, DbType.Int32);

                        query.CommandText = @"SELECT trea_id, trea_name, trea_description, trea_date_start, trea_date_end, 
                                                    trea_observations, trea_is_active, trea_doctor_id, trea_patient_id
                                                FROM treatment
                                                WHERE (@treatment_name='' or trea_name like @treatment_name) 
                                                    AND (trea_doctor_id = @doctor_id AND trea_patient_id = @patient_id)";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int trea_id = reader.GetInt32(reader.GetOrdinal("trea_id"));
                            String trea_name = DBUtils.readDBC<String>(reader, "trea_name");
                            String trea_description = DBUtils.readDBC<String>(reader, "trea_description");
                            DateTime trea_date_start = reader.GetDateTime(reader.GetOrdinal("trea_date_start"));
                            DateTime trea_date_end = reader.GetDateTime(reader.GetOrdinal("trea_date_end"));

                            String trea_observations = null;
                            if (reader["trea_observations"] != DBNull.Value)
                            {
                                trea_observations = DBUtils.readDBC<String>(reader, "trea_observations");
                            }

                            bool trea_is_active = reader.GetBoolean(reader.GetOrdinal("trea_is_active"));
                            int trea_doctor_id = reader.GetInt32(reader.GetOrdinal("trea_doctor_id"));
                            int trea_patient_id = reader.GetInt32(reader.GetOrdinal("trea_patient_id"));

                            TreatmentDB treatmentAux = new TreatmentDB(trea_id, trea_name, trea_description,
                                                            trea_date_start, trea_date_end, trea_observations,
                                                            trea_is_active, trea_doctor_id, trea_patient_id);
                            treatments.Add(treatmentAux);
                        }
                    }
                }
            }
            return treatments;
        }

        public static TreatmentDB GetTreatmentById(int treatmentId)
        {
            TreatmentDB treatmentAux = new TreatmentDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@treatment_id", treatmentId, DbType.Int32);

                        query.CommandText = @"SELECT trea_id, trea_name, trea_description, trea_date_start, trea_date_end, 
                                                    trea_observations, trea_is_active, trea_doctor_id, trea_patient_id
                                                FROM treatment
                                                WHERE trea_id = @treatment_id;";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int trea_id = reader.GetInt32(reader.GetOrdinal("trea_id"));
                            String trea_name = DBUtils.readDBC<String>(reader, "trea_name");
                            String trea_description = DBUtils.readDBC<String>(reader, "trea_description");
                            DateTime trea_date_start = reader.GetDateTime(reader.GetOrdinal("trea_date_start"));
                            DateTime trea_date_end = reader.GetDateTime(reader.GetOrdinal("trea_date_end"));

                            String trea_observations = null;
                            if (reader["trea_observations"] != DBNull.Value)
                            {
                                trea_observations = DBUtils.readDBC<String>(reader, "trea_observations");
                            }

                            bool trea_is_active = reader.GetBoolean(reader.GetOrdinal("trea_is_active"));
                            int trea_doctor_id = reader.GetInt32(reader.GetOrdinal("trea_doctor_id"));
                            int trea_patient_id = reader.GetInt32(reader.GetOrdinal("trea_patient_id"));

                            treatmentAux = new TreatmentDB(trea_id, trea_name, trea_description,
                                                            trea_date_start, trea_date_end, trea_observations,
                                                            trea_is_active, trea_doctor_id, trea_patient_id);
                        }
                    }
                }
            }
            return treatmentAux;
        }

        public static int InsertTreatment(TreatmentDB treatmentAux)
        {

            int insertedId = -1;

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (DbConnection connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();

                    try {
                        using (DbCommand query = connection.CreateCommand())
                        {
                            query.Transaction = transaction;

                            /*
                             `trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, 
                            `trea_is_active`, `trea_doctor_id`, `trea_patient_id`
                            */

                            DBUtils.crearParametre(query, "@t_name", treatmentAux.Trea_name, DbType.String);
                            DBUtils.crearParametre(query, "@t_description", treatmentAux.Trea_description, DbType.String);
                            DBUtils.crearParametre(query, "@t_date_start", treatmentAux.Trea_date_start, DbType.DateTime);
                            DBUtils.crearParametre(query, "@t_date_end", treatmentAux.Trea_date_end, DbType.DateTime);
                            DBUtils.crearParametre(query, "@t_observations", treatmentAux.Trea_observations, DbType.String);
                            DBUtils.crearParametre(query, "@t_is_active", treatmentAux.Trea_is_active, DbType.Boolean);
                            DBUtils.crearParametre(query, "@t_doctor_id", treatmentAux.Trea_doctor_id, DbType.Int32);
                            DBUtils.crearParametre(query, "@t_patient_id", treatmentAux.Trea_patient_id, DbType.Int32);

                            query.CommandText = @"INSERT INTO treatment (trea_name, 
                                                                        trea_description, 
                                                                        trea_date_start,
                                                                        trea_date_end,
                                                                        trea_observations, 
                                                                        trea_is_active,
                                                                        trea_doctor_id,
                                                                        trea_patient_id) 
                                                    values ( @t_name, 
                                                                @t_description, 
                                                                @t_date_start, 
                                                                @t_date_end, 
                                                                @t_observations, 
                                                                @t_is_active, 
                                                                @t_doctor_id, 
                                                                @t_patient_id )";

                            int numRowsInserted = query.ExecuteNonQuery();
                            if (numRowsInserted != 1)
                            {
                                transaction.Rollback();
                                return -1;
                            }

                            query.CommandText = @"SELECT LAST_INSERT_ID()";

                            insertedId = Convert.ToInt32(query.ExecuteScalar());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR" + ex);
                    }

                    transaction.Commit();
                    return insertedId;
                }
            }
        }

        public static bool UpdateTreatment(TreatmentDB treatmentAux)
        {
            /*
                1. UPDATE treatment SET ... WHERE trea_id=@t_id;
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
                            `trea_name`, `trea_description`, `trea_date_start`, `trea_date_end`, `trea_observations`, 
                            `trea_is_active`, `trea_doctor_id`, `trea_patient_id`
                            */

                            // TREATMENT TABLE FIELDS
                            DBUtils.crearParametre(consulta, "@t_id", treatmentAux.Trea_id, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@t_name", treatmentAux.Trea_name, DbType.String);
                            DBUtils.crearParametre(consulta, "@t_description", treatmentAux.Trea_description, DbType.String);
                            DBUtils.crearParametre(consulta, "@t_date_start", treatmentAux.Trea_date_start, DbType.DateTime);
                            DBUtils.crearParametre(consulta, "@t_date_end", treatmentAux.Trea_date_end, DbType.DateTime);
                            DBUtils.crearParametre(consulta, "@t_observations", treatmentAux.Trea_observations, DbType.String);
                            DBUtils.crearParametre(consulta, "@t_is_active", treatmentAux.Trea_is_active, DbType.Boolean);

                            consulta.CommandText = @"UPDATE treatment SET trea_name = @t_name,
                                                                    trea_description = @t_description,
                                                                    trea_date_start = @t_date_start,
                                                                    trea_date_end = @t_date_end,
                                                                    trea_observations = @t_observations,
                                                                    trea_is_active = @t_is_active
                                                    WHERE trea_id = @t_id";

                            int numUpdatedTreaRows = consulta.ExecuteNonQuery();
                            if (numUpdatedTreaRows != 1)
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

        public static bool DeleteTreatmentById(int treatmentId)
        {
            /*
                1. DELETE FROM treatment_medicine WHERE trme_treatment_id = 6;

                2. DELETE FROM treatment WHERE trea_id = 6;
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

                        DBUtils.crearParametre(query, "@treatment_id", treatmentId, DbType.Int32);

                        // DELETE FROM TREATMENT_MEDICINE TABLE =========================
                        query.CommandText = "SELECT COUNT(1) FROM treatment_medicine WHERE trme_treatment_id = @treatment_id";

                        long numTreatmentsMedicine = (long)query.ExecuteScalar();
                        if (numTreatmentsMedicine < 0) return false;

                        query.CommandText = "DELETE FROM treatment_medicine WHERE trme_treatment_id = @treatment_id";

                        int numTreatmentMedicineDeleted = query.ExecuteNonQuery();
                        if (numTreatmentMedicineDeleted != 1)
                        {
                            transaction.Rollback();
                            success = false;
                        }
                        // ===================================================

                        // DELETE FROM TREATMENT TABLE =========================
                        query.CommandText = "SELECT COUNT(1) FROM treatment WHERE trea_id = @treatment_id";

                        long numTreatments = (long)query.ExecuteScalar();
                        if (numTreatments < 0) return false;

                        query.CommandText = "DELETE FROM treatment WHERE trea_id = @treatment_id";

                        int numTreatmentDeleted = query.ExecuteNonQuery();
                        if (numTreatmentDeleted != 1)
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

        #region VERIFICATION METHODS
        public static bool IsValidName(string name)
        {
            if ((string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name)) || (name.Length <= 0) || (name.Length >= 60))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool isValidDescription(string desc)
        {
            if ((string.IsNullOrWhiteSpace(desc) || string.IsNullOrEmpty(desc)) || (desc.Length <= 0) || (desc.Length >= 60))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidDateStart(DateTime start_date)
        {
            DateTime startDate = new DateTime(2000,01,01);
            DateTime endDate = DateTime.Now.AddDays(1);

            return start_date >= startDate && start_date <= endDate;
        }

        public static bool IsValidDateEnd(DateTime end_date)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = new DateTime(3000, 01, 01);

            return end_date >= startDate && end_date <= endDate;
        }
        #endregion
    }
}
