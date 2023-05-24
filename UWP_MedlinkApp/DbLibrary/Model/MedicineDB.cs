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
    public class MedicineDB
    {
        private int medi_id;
        private String medi_name;
        private int medi_category_id;

        public MedicineDB()
        {
        }

        public MedicineDB(int medi_id, string medi_name, int medi_category_id)
        {
            this.Medi_id = medi_id;
            this.Medi_name = medi_name;
            this.Medi_category_id = medi_category_id;
        }

        public int Medi_id { get => medi_id; set => medi_id = value; }
        public string Medi_name { get => medi_name; set => medi_name = value; }
        public int Medi_category_id { get => medi_category_id; set => medi_category_id = value; }

        #region EXTRA PROPERTIES
        public String Medi_CategoryName
        {
            get
            {
                return MedicineCategoryDB.GetMedicineCategoryById(Medi_category_id).Meca_name;
            }
        }
        #endregion

        #region EQUALS & HASHCODE
        public override bool Equals(object obj)
        {
            return obj is MedicineDB dB &&
                   medi_id == dB.medi_id;
        }

        public override int GetHashCode()
        {
            return -749112888 + medi_id.GetHashCode();
        }
        #endregion

        #region DB METHODS
        public static ObservableCollection<MedicineDB> GetAllMedicines(String filterByName, int filterByCategoryId)
        {
            ObservableCollection<MedicineDB> medicines = new ObservableCollection<MedicineDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@medicine_name", "%" + filterByName + "%", DbType.String);
                        DBUtils.crearParametre(query, "@medicine_category_id", filterByCategoryId, DbType.Int32);

                        query.CommandText = @"SELECT medi_id, medi_name, medi_category_id
                                                FROM medicine
                                                WHERE (@medicine_name='' OR medi_name LIKE @medicine_name)
                                                    AND (@medicine_category_id=0 OR medi_category_id = @medicine_category_id)";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int medi_id = reader.GetInt32(reader.GetOrdinal("medi_id"));
                            String medi_name = DBUtils.readDBC<String>(reader, "medi_name");
                            int medi_category_id = reader.GetInt32(reader.GetOrdinal("medi_category_id"));

                            MedicineDB medicineAux = new MedicineDB(medi_id, medi_name, medi_category_id);
                            medicines.Add(medicineAux);
                        }
                    }
                }
            }
            return medicines;
        }

        public static MedicineDB GetMedicineById(int medicineId)
        {
            MedicineDB medicineAux = new MedicineDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@medicine_id", medicineId, DbType.Int32);

                        query.CommandText = @"SELECT medi_id, medi_name, medi_category_id
                                                FROM medicine
                                                WHERE medi_id = @medicine_id";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int medi_id = reader.GetInt32(reader.GetOrdinal("medi_id"));
                            String medi_name = DBUtils.readDBC<String>(reader, "medi_name");
                            int medi_category_id = reader.GetInt32(reader.GetOrdinal("medi_category_id"));

                            medicineAux = new MedicineDB(medi_id, medi_name, medi_category_id);
                        }
                    }
                }
            }
            return medicineAux;
        }

        public static MedicineDB GetMedicineByMedicineCategoryId(int categoryId)
        {
            MedicineDB medicineAux = new MedicineDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@category_id", categoryId, DbType.Int32);

                        query.CommandText = @"SELECT medi_id, medi_name, medi_category_id
                                                FROM medicine
                                                WHERE medi_category_id = @category_id";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int medi_id = reader.GetInt32(reader.GetOrdinal("medi_id"));
                            String medi_name = DBUtils.readDBC<String>(reader, "medi_name");
                            int medi_category_id = reader.GetInt32(reader.GetOrdinal("medi_category_id"));

                            medicineAux = new MedicineDB(medi_id, medi_name, medi_category_id);
                        }
                    }
                }
            }
            return medicineAux;
        }
        #endregion
    }
}
