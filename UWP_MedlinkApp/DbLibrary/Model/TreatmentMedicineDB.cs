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
    public class TreatmentMedicineDB
    {
        private int trme_treatment_id;
        private int trme_medicine_id;
        private DateTime trme_date_start;
        private DateTime trme_date_end;
        private float trme_quantity_per_day;
        private float trme_total_quantity;
        private String trme_frequency;
        private int trme_units_of_measure_id;

        public TreatmentMedicineDB()
        {
        }

        public TreatmentMedicineDB(int trme_treatment_id, int trme_medicine_id, 
            DateTime trme_date_start, DateTime trme_date_end, float trme_quantity_per_day, 
            float trme_total_quantity, String trme_frequency, int trme_units_of_measure_id)
        {
            this.Trme_treatment_id = trme_treatment_id;
            this.Trme_medicine_id = trme_medicine_id;
            this.Trme_date_start = trme_date_start;
            this.Trme_date_end = trme_date_end;
            this.Trme_quantity_per_day = trme_quantity_per_day;
            this.Trme_total_quantity = trme_total_quantity;
            this.Trme_frequency = trme_frequency;
            this.Trme_units_of_measure_id = trme_units_of_measure_id;
        }

        public int Trme_treatment_id { get => trme_treatment_id; set => trme_treatment_id = value; }
        public int Trme_medicine_id { get => trme_medicine_id; set => trme_medicine_id = value; }
        public DateTime Trme_date_start { get => trme_date_start; set => trme_date_start = value; }
        public DateTime Trme_date_end { get => trme_date_end; set => trme_date_end = value; }
        public float Trme_quantity_per_day { get => trme_quantity_per_day; set => trme_quantity_per_day = value; }
        public float Trme_total_quantity { get => trme_total_quantity; set => trme_total_quantity = value; }
        public string Trme_frequency { get => trme_frequency; set => trme_frequency = value; }
        public int Trme_units_of_measure_id { get => trme_units_of_measure_id; set => trme_units_of_measure_id = value; }

        #region EXTRA_PROPERTIES
        public String Trme_MedicineName
        {
            get
            {
                return MedicineDB.GetMedicineById(Trme_medicine_id).Medi_name;
            }
        }

        public String Trme_MedicineCategoryName
        {
            get
            {
                return MedicineCategoryDB.GetCategoryByMedicineId(trme_medicine_id).Meca_name;
            }
        }

        public String Trme_DateStart_Date
        {
            get
            {
                return Trme_date_start.ToString("dd/MM/yyyy");

            }
        }

        public String Trme_DateEnd_Date
        {
            get
            {
                return Trme_date_end.ToString("dd/MM/yyyy");

            }
        }

        public String Trme_UOM_Name
        {
            get
            {
                string name = UnitsOfMeasureDB.GetUOMById(trme_units_of_measure_id).Unme_name;
                return name.Substring(0, 1).ToUpper() + name.Substring(1);
            }
        }

        public String Trme_UOM_Abbreviation
        {
            get
            {
                return UnitsOfMeasureDB.GetUOMById(trme_units_of_measure_id).Unme_abbreviation;
            }
        }
        #endregion

        #region EQUALS & HASHCODE
        public override bool Equals(object obj)
        {
            return obj is TreatmentMedicineDB dB &&
                   trme_treatment_id == dB.trme_treatment_id &&
                   trme_medicine_id == dB.trme_medicine_id;
        }

        public override int GetHashCode()
        {
            int hashCode = 690243276;
            hashCode = hashCode * -1521134295 + trme_treatment_id.GetHashCode();
            hashCode = hashCode * -1521134295 + trme_medicine_id.GetHashCode();
            return hashCode;
        }
        #endregion

        #region VERIFICATION METHODS
        public static bool IsValidFrequency(string frequency)
        {
            if ((string.IsNullOrWhiteSpace(frequency) || string.IsNullOrEmpty(frequency)) || (frequency.Length <= 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsValidTotalQuantity(float total_quantity)
        {
            if (total_quantity > 0 && total_quantity <= 1000000)
            {
                return true;
            }
            else
            {
                return false;
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

        public static ObservableCollection<TreatmentMedicineDB> GetAllTretmentMedicinesByTreatmentId(int treatmentId)
        {
            ObservableCollection<TreatmentMedicineDB> treatmentMedicines = new ObservableCollection<TreatmentMedicineDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@treatment_id", treatmentId, DbType.Int32);

                        query.CommandText = @"SELECT trme_treatment_id, trme_medicine_id, trme_date_start, trme_date_end, 
                                                    trme_quantity_per_day, trme_total_quantity, trme_frequency, trme_unit_of_measure_id
                                                FROM treatment_medicine
                                                WHERE trme_treatment_id = @treatment_id;";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int trme_treatment_id = reader.GetInt32(reader.GetOrdinal("trme_treatment_id"));
                            int trme_medicine_id = reader.GetInt32(reader.GetOrdinal("trme_medicine_id"));
                            DateTime trme_date_start = reader.GetDateTime(reader.GetOrdinal("trme_date_start"));
                            DateTime trme_date_end = reader.GetDateTime(reader.GetOrdinal("trme_date_end"));
                            float trme_quantity_per_day = reader.GetFloat(reader.GetOrdinal("trme_quantity_per_day"));
                            float trme_total_quantity = reader.GetFloat(reader.GetOrdinal("trme_total_quantity"));
                            String trme_frequency = reader.GetString(reader.GetOrdinal("trme_frequency"));
                            int trme_unit_of_measure_id = reader.GetInt32(reader.GetOrdinal("trme_unit_of_measure_id"));

                            TreatmentMedicineDB treatmentMedicineAux = new TreatmentMedicineDB(trme_treatment_id, trme_medicine_id, trme_date_start,
                                                                                                trme_date_end, trme_quantity_per_day, trme_total_quantity,
                                                                                                trme_frequency, trme_unit_of_measure_id);
                            treatmentMedicines.Add(treatmentMedicineAux);
                        }
                    }
                }
            }
            return treatmentMedicines;
        }

        public static TreatmentMedicineDB GetTreatmentMedicineByTreatmentAndMedicineId(int treatmentId, int medicineId)
        {
            TreatmentMedicineDB treatmentMedicineAux = new TreatmentMedicineDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@treatment_id", treatmentId, DbType.Int32);
                        DBUtils.crearParametre(query, "@medicine_id", medicineId, DbType.Int32);

                        query.CommandText = @"SELECT trme_treatment_id, trme_medicine_id, trme_date_start, trme_date_end, 
                                                    trme_quantity_per_day, trme_total_quantity, trme_frequency, trme_unit_of_measure_id
                                                FROM treatment_medicine
                                                WHERE trme_treatment_id = @treatment_id AND trme_medicine_id = @medicine_id;";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int trme_treatment_id = reader.GetInt32(reader.GetOrdinal("trme_treatment_id"));
                            int trme_medicine_id = reader.GetInt32(reader.GetOrdinal("trme_medicine_id"));
                            DateTime trme_date_start = reader.GetDateTime(reader.GetOrdinal("trme_date_start"));
                            DateTime trme_date_end = reader.GetDateTime(reader.GetOrdinal("trme_date_end"));
                            float trme_quantity_per_day = reader.GetFloat(reader.GetOrdinal("trme_quantity_per_day"));
                            float trme_total_quantity = reader.GetFloat(reader.GetOrdinal("trme_total_quantity"));
                            String trme_frequency = reader.GetString(reader.GetOrdinal("trme_frequency"));
                            int trme_unit_of_measure_id = reader.GetInt32(reader.GetOrdinal("trme_unit_of_measure_id"));

                            treatmentMedicineAux = new TreatmentMedicineDB(trme_treatment_id, trme_medicine_id, trme_date_start,
                                                                                                trme_date_end, trme_quantity_per_day, trme_total_quantity,
                                                                                                trme_frequency, trme_unit_of_measure_id);
                        }
                    }
                }
            }
            return treatmentMedicineAux;
        }

        public static int InsertTreatmentMedicine(TreatmentMedicineDB treatmentMedicineAux)
        {
            int insertedId = -1;

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (DbConnection connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        using (DbCommand query = connection.CreateCommand())
                        {
                            query.Transaction = transaction;

                            DBUtils.crearParametre(query, "@m_trea_id", treatmentMedicineAux.Trme_treatment_id, DbType.Int32);
                            DBUtils.crearParametre(query, "@m_medi_id", treatmentMedicineAux.Trme_medicine_id, DbType.Int32);
                            DBUtils.crearParametre(query, "@m_date_start", treatmentMedicineAux.Trme_date_start, DbType.DateTime);
                            DBUtils.crearParametre(query, "@m_date_end", treatmentMedicineAux.Trme_date_end, DbType.DateTime);
                            DBUtils.crearParametre(query, "@m_qty_per_day", treatmentMedicineAux.Trme_quantity_per_day, DbType.Double);
                            DBUtils.crearParametre(query, "@m_total_qty", treatmentMedicineAux.Trme_total_quantity, DbType.Double);
                            DBUtils.crearParametre(query, "@m_frequency", treatmentMedicineAux.Trme_frequency, DbType.String);
                            DBUtils.crearParametre(query, "@m_uom_id", treatmentMedicineAux.Trme_units_of_measure_id, DbType.Int32);

                            query.CommandText = @"INSERT INTO treatment_medicine (trme_treatment_id, 
                                                                        trme_medicine_id, 
                                                                        trme_date_start,
                                                                        trme_date_end,
                                                                        trme_quantity_per_day, 
                                                                        trme_total_quantity,
                                                                        trme_frequency,
                                                                        trme_unit_of_measure_id) 
                                                    values ( @m_trea_id, 
                                                                @m_medi_id, 
                                                                @m_date_start, 
                                                                @m_date_end, 
                                                                @m_qty_per_day, 
                                                                @m_total_qty,
                                                                @m_frequency,
                                                                @m_uom_id)";

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

        public static bool UpdateTreatmentMedicine(TreatmentMedicineDB treatmentMedicineAux)
        {
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
                            DBUtils.crearParametre(consulta, "@m_trea_id", treatmentMedicineAux.Trme_treatment_id, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@m_medi_id", treatmentMedicineAux.Trme_medicine_id, DbType.Int32);
                            DBUtils.crearParametre(consulta, "@m_date_start", treatmentMedicineAux.Trme_date_start, DbType.DateTime);
                            DBUtils.crearParametre(consulta, "@m_date_end", treatmentMedicineAux.Trme_date_end, DbType.DateTime);
                            DBUtils.crearParametre(consulta, "@m_qty_per_day", treatmentMedicineAux.Trme_quantity_per_day, DbType.Double);
                            DBUtils.crearParametre(consulta, "@m_total_qty", treatmentMedicineAux.Trme_total_quantity, DbType.Double);
                            DBUtils.crearParametre(consulta, "@m_frequency", treatmentMedicineAux.Trme_frequency, DbType.String);
                            DBUtils.crearParametre(consulta, "@m_uom_id", treatmentMedicineAux.Trme_units_of_measure_id, DbType.Int32);

                            consulta.CommandText = @"UPDATE treatment_medicine SET 
                                                                    trme_date_start = @m_date_start,
                                                                    trme_date_end = @m_date_end,
                                                                    trme_quantity_per_day = @m_qty_per_day,
                                                                    trme_total_quantity = @m_total_qty,
                                                                    trme_frequency = @m_frequency,
                                                                    trme_unit_of_measure_id = @m_uom_id
                                                    WHERE trme_treatment_id = @m_trea_id 
                                                            AND trme_medicine_id = @m_medi_id";

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

        public static bool DeleteTreatmentMedicineByTreatmentAndMedicineId(int treatmentId, int medicineId)
        {
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
                        DBUtils.crearParametre(query, "@medicine_id", medicineId, DbType.Int32);

                        // DELETE FROM TREATMENT_MEDICINE TABLE =========================
                        query.CommandText = @"SELECT COUNT(1) 
                                                FROM treatment_medicine
                                                WHERE trme_treatment_id = @treatment_id AND trme_medicine_id = @medicine_id";

                        long numTreatmentsMedicine = (long)query.ExecuteScalar();
                        if (numTreatmentsMedicine < 0) return false;

                        query.CommandText = @"DELETE FROM treatment_medicine 
                                                WHERE trme_treatment_id = @treatment_id AND trme_medicine_id = @medicine_id";

                        int numTreatmentMedicineDeleted = query.ExecuteNonQuery();
                        if (numTreatmentMedicineDeleted != 1)
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
