using Consultorio_Seguros.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Consultorio_Seguros.DAL
{
    public class Seguro_DAL
    {
        SqlConnection db = null;
        SqlCommand command = null;

        public static IConfiguration Config { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");

            Config = builder.Build();
            return Config.GetConnectionString("db");
        }

        public List<Seguro> GetAll()
        {
            List<Seguro> seguroListar = new List<Seguro>();
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_SEGUROS_LISTAR]";
                db.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Seguro seguro = new Seguro();
                    seguro.Id = Convert.ToInt32(dr["Id"]);
                    seguro.Codigo = dr["Codigo"].ToString();
                    seguro.Nombre = dr["Nombre"].ToString();
                    seguro.SemiAsegurada = dr["SemiAsegurada"].ToString();
                    seguro.Prima = dr["Prima"].ToString();

                    //seguro.SemiAsegurada = Convert.ToDecimal(dr["SemiAsegurada"]);
                    //seguro.Prima = Convert.ToDecimal(dr["Prima"]);
                    seguroListar.Add(seguro);
                }
                db.Close();
            }
            return seguroListar;
        }

        public bool Insert(Seguro model)
        {
            int id = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_SEGUROS_CREAR]";
                command.Parameters.AddWithValue("@Codigo", model.Codigo);
                command.Parameters.AddWithValue("@Nombre", model.Nombre);
                command.Parameters.AddWithValue("@SemiA", model.SemiAsegurada);
                command.Parameters.AddWithValue("@Prima", model.Prima);
                db.Open();

                id = command.ExecuteNonQuery();
                db.Close();
            }
            return id > 0 ? true : false;
        }

        public Seguro GetById(int id)
        {
            Seguro seguro = new Seguro();
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_SEGUROS_ID]";
                command.Parameters.AddWithValue("@Id", id);
                db.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    seguro.Id = Convert.ToInt32(dr["Id"]);
                    seguro.Codigo = dr["Codigo"].ToString();
                    seguro.Nombre = dr["Nombre"].ToString();
                    seguro.SemiAsegurada = dr["SemiAsegurada"].ToString();
                    seguro.Prima = dr["Prima"].ToString();

                    //seguro.SemiAsegurada = Convert.ToDecimal(dr["SemiAsegurada"]);
                    //seguro.Prima = Convert.ToDecimal(dr["Prima"]);
                }
                db.Close();
            }
            return seguro;
        }

        public bool Update(Seguro model)
        {
            int id = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_SEGUROS_EDITAR]";
                command.Parameters.AddWithValue("@Id", model.Id);
                command.Parameters.AddWithValue("@Codigo", model.Codigo);
                command.Parameters.AddWithValue("@Nombre", model.Nombre);
                command.Parameters.AddWithValue("@SemiA", model.SemiAsegurada);
                command.Parameters.AddWithValue("@Prima", model.Prima);
                db.Open();
                id = command.ExecuteNonQuery();
                db.Close();
            }
            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int deleteRowCount = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_SEGUROS_ELIMINAR]";
                command.Parameters.AddWithValue("@Id", id);
                db.Open();
                deleteRowCount = command.ExecuteNonQuery();
                db.Close();
            }
            return deleteRowCount > 0 ? true : false;
        }
    }
}