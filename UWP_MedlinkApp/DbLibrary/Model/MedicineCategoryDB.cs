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
    public class MedicineCategoryDB
    {
        private int meca_id;
        private String meca_name;

        public MedicineCategoryDB()
        {
        }

        public MedicineCategoryDB(int meca_id, string meca_name)
        {
            this.Meca_id = meca_id;
            this.Meca_name = meca_name;
        }

        public int Meca_id { get => meca_id; set => meca_id = value; }
        public string Meca_name { get => meca_name; set => meca_name = value; }

        #region EQUALS & HASHCODE
        public override bool Equals(object obj)
        {
            return obj is MedicineCategoryDB dB &&
                   meca_id == dB.meca_id;
        }

        public override int GetHashCode()
        {
            return 1249771661 + meca_id.GetHashCode();
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

        public static ObservableCollection<MedicineCategoryDB> GetAllMedicineCategories()
        {
            ObservableCollection<MedicineCategoryDB> medicineCategories = new ObservableCollection<MedicineCategoryDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = @"SELECT meca_id, meca_name
                                                FROM medicine_category";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int meca_id = reader.GetInt32(reader.GetOrdinal("meca_id"));
                            String meca_name = DBUtils.readDBC<String>(reader, "meca_name");

                            MedicineCategoryDB medicineCategoryAux = new MedicineCategoryDB(meca_id, meca_name);
                            medicineCategories.Add(medicineCategoryAux);
                        }
                    }
                }
            }
            return medicineCategories;
        }

        public static MedicineCategoryDB GetMedicineCategoryById(int categoryId)
        {
            MedicineCategoryDB medicineCategoryAux = new MedicineCategoryDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@category_id", categoryId, DbType.Int32);

                        query.CommandText = @"SELECT meca_id, meca_name
                                                FROM medicine_category
                                                WHERE meca_id = @category_id";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int meca_id = reader.GetInt32(reader.GetOrdinal("meca_id"));
                            String meca_name = DBUtils.readDBC<String>(reader, "meca_name");

                            medicineCategoryAux = new MedicineCategoryDB(meca_id, meca_name);
                        }
                    }
                }
            }
            return medicineCategoryAux;
        }

        public static MedicineCategoryDB GetCategoryByMedicineId(int medicineId)
        {
            MedicineCategoryDB medicineCategoryAux = new MedicineCategoryDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@medicine_id", medicineId, DbType.Int32);

                        query.CommandText = @"SELECT meca_id, meca_name
                                                FROM medicine LEFT JOIN medicine_category ON medi_category_id = meca_id
                                                WHERE medi_id = @medicine_id
                                                GROUP BY meca_name;";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int meca_id = reader.GetInt32(reader.GetOrdinal("meca_id"));
                            String meca_name = DBUtils.readDBC<String>(reader, "meca_name");

                            medicineCategoryAux = new MedicineCategoryDB(meca_id, meca_name);
                        }
                    }
                }
            }
            return medicineCategoryAux;
        }
        #endregion
    }
}
