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
    public class SpecialtyDB
    {
        private int spec_id;
        private String spec_name;

        public SpecialtyDB()
        {
        }

        public SpecialtyDB(int spec_id, string spec_name)
        {
            this.Spec_id = spec_id;
            this.Spec_name = spec_name;
        }

        public int Spec_id { get => spec_id; set => spec_id = value; }
        public string Spec_name { get => spec_name; set => spec_name = value; }

        

        #region EQUALS & HASHCODE
        public override bool Equals(object obj) 
        {
            return obj is SpecialtyDB dB &&
                   spec_id == dB.spec_id;
        }

        public override int GetHashCode()
        {
            return 1695754670 + spec_id.GetHashCode();
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

        public static ObservableCollection<SpecialtyDB> GetAllSpecialties()
        {
            ObservableCollection<SpecialtyDB> specialties = new ObservableCollection<SpecialtyDB>();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        query.CommandText = @"SELECT spec_id, spec_name
                                                FROM specialty";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int spec_id = reader.GetInt32(reader.GetOrdinal("spec_id"));
                            String spec_name = DBUtils.readDBC<String>(reader, "spec_name");

                            SpecialtyDB specialtyAux = new SpecialtyDB(spec_id, spec_name);
                            specialties.Add(specialtyAux);
                        }
                    }
                }
            }
            return specialties;
        }

        public static SpecialtyDB GetSpecialtyById(int specialtyId)
        {
            SpecialtyDB specialtyAux = new SpecialtyDB();

            using (MySQLConnDbContext context = new MySQLConnDbContext())
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var query = connection.CreateCommand())
                    {
                        DBUtils.crearParametre(query, "@specialty_id", specialtyId, DbType.Int32);

                        query.CommandText = @"SELECT spec_id, spec_name
                                                FROM specialty
                                                WHERE spec_id = @specialty_id";

                        DbDataReader reader = query.ExecuteReader();

                        while (reader.Read())
                        {
                            int spec_id = reader.GetInt32(reader.GetOrdinal("spec_id"));
                            String spec_name = DBUtils.readDBC<String>(reader, "spec_name");

                            specialtyAux = new SpecialtyDB(spec_id, spec_name);
                        }
                    }
                }
            }
            return specialtyAux;
        }
        #endregion
    }
}
