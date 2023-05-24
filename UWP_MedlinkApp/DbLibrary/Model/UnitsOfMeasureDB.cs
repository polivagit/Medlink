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
    public class UnitsOfMeasureDB
    {
        private int unme_id;
        private String unme_name;
        private String unme_abbreviation;

        public UnitsOfMeasureDB()
        {

        }

        public UnitsOfMeasureDB(int unme_id, string unme_name, string unme_abbreviation)
        {
            this.Unme_id = unme_id;
            this.Unme_name = unme_name;
            this.Unme_abbreviation = unme_abbreviation;
        }

        public int Unme_id { get => unme_id; set => unme_id = value; }
        public string Unme_name { get => unme_name; set => unme_name = value; }
        public string Unme_abbreviation { get => unme_abbreviation; set => unme_abbreviation = value; }

        #region EQUALS & HASHCODE
        public override bool Equals(object obj)
        {
            return obj is UnitsOfMeasureDB dB &&
                   unme_id == dB.unme_id;
        }

        public override int GetHashCode()
        {
            return 181895336 + unme_id.GetHashCode();
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

        public static ObservableCollection<UnitsOfMeasureDB> GetAllUOMs()
        {
            ObservableCollection<UnitsOfMeasureDB> UOMs = new ObservableCollection<UnitsOfMeasureDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = @"SELECT unme_id, unme_name, unme_abbreviation
                                                FROM units_of_measure";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int unme_id = reader.GetInt32(reader.GetOrdinal("unme_id"));
                            String unme_name = DBUtils.readDBC<String>(reader, "unme_name");
                            String unme_abbreviation = DBUtils.readDBC<String>(reader, "unme_abbreviation");

                            UnitsOfMeasureDB UOMAux = new UnitsOfMeasureDB(unme_id, unme_name, unme_abbreviation);
                            UOMs.Add(UOMAux);
                        }
                    }
                }
            }
            return UOMs;
        }

        public static UnitsOfMeasureDB GetUOMById(int uomId)
        {
            UnitsOfMeasureDB UOMAux = new UnitsOfMeasureDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@uom_id", uomId, DbType.Int32);

                        query.CommandText = @"SELECT unme_id, unme_name, unme_abbreviation
                                                FROM units_of_measure
                                                WHERE unme_id = @uom_id";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int unme_id = reader.GetInt32(reader.GetOrdinal("unme_id"));
                            String unme_name = DBUtils.readDBC<String>(reader, "unme_name");
                            String unme_abbreviation = DBUtils.readDBC<String>(reader, "unme_abbreviation");

                            UOMAux = new UnitsOfMeasureDB(unme_id, unme_name, unme_abbreviation);
                        }
                    }
                }
            }
            return UOMAux;
        }
        #endregion
    }
}
