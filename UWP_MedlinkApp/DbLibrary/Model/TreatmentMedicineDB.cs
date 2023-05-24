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
        private int trme_units_of_measure_id;

        public TreatmentMedicineDB()
        {
        }

        public TreatmentMedicineDB(int trme_treatment_id, int trme_medicine_id, 
            DateTime trme_date_start, DateTime trme_date_end, float trme_quantity_per_day, 
            float trme_total_quantity, int trme_units_of_measure_id)
        {
            this.Trme_treatment_id = trme_treatment_id;
            this.Trme_medicine_id = trme_medicine_id;
            this.Trme_date_start = trme_date_start;
            this.Trme_date_end = trme_date_end;
            this.Trme_quantity_per_day = trme_quantity_per_day;
            this.Trme_total_quantity = trme_total_quantity;
            this.Trme_units_of_measure_id = trme_units_of_measure_id;
        }

        public int Trme_treatment_id { get => trme_treatment_id; set => trme_treatment_id = value; }
        public int Trme_medicine_id { get => trme_medicine_id; set => trme_medicine_id = value; }
        public DateTime Trme_date_start { get => trme_date_start; set => trme_date_start = value; }
        public DateTime Trme_date_end { get => trme_date_end; set => trme_date_end = value; }
        public float Trme_quantity_per_day { get => trme_quantity_per_day; set => trme_quantity_per_day = value; }
        public float Trme_total_quantity { get => trme_total_quantity; set => trme_total_quantity = value; }
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
                                                    trme_quantity_per_day, trme_total_quantity, trme_unit_of_measure_id
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
                            int trme_unit_of_measure_id = reader.GetInt32(reader.GetOrdinal("trme_unit_of_measure_id"));

                            TreatmentMedicineDB treatmentMedicineAux = new TreatmentMedicineDB(trme_treatment_id, trme_medicine_id, trme_date_start,
                                                                                                trme_date_end, trme_quantity_per_day, trme_total_quantity,
                                                                                                trme_unit_of_measure_id);
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
                                                    trme_quantity_per_day, trme_total_quantity, trme_unit_of_measure_id
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
                            int trme_unit_of_measure_id = reader.GetInt32(reader.GetOrdinal("trme_unit_of_measure_id"));

                            treatmentMedicineAux = new TreatmentMedicineDB(trme_treatment_id, trme_medicine_id, trme_date_start,
                                                                                                trme_date_end, trme_quantity_per_day, trme_total_quantity,
                                                                                                trme_unit_of_measure_id);
                        }
                    }
                }
            }
            return treatmentMedicineAux;
        }
        #endregion
    }
}
