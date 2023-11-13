using Consultorio_Seguros.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Consultorio_Seguros.Process
{
    public class Cliente_DAL
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

        public List<Cliente> GetAll()
        {
            List<Cliente> clienteListar = new List<Cliente>();
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_CLIENTES_LISTAR]";
                db.Open();

                SqlDataReader dr = command.ExecuteReader();

                while(dr.Read())
                {
                    Cliente cliente = new Cliente();
                    cliente.Id = Convert.ToInt32(dr["Id"]);
                    cliente.Cedula = dr["Cedula"].ToString();
                    cliente.Nombre = dr["Nombre"].ToString();
                    cliente.Telefono = dr["Telefono"].ToString();
                    cliente.Edad = Convert.ToInt32(dr["Edad"]);
                    clienteListar.Add(cliente);
                }
                db.Close();
            }
            return clienteListar;
        }

        public bool Insert(Cliente model)
        {
            int id = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_CLIENTES_CREAR]";
                command.Parameters.AddWithValue("@Cedula", model.Cedula);
                command.Parameters.AddWithValue("@Nombre", model.Nombre);
                command.Parameters.AddWithValue("@Telefono", model.Telefono);
                command.Parameters.AddWithValue("@Edad", model.Edad);
                db.Open();
                id = command.ExecuteNonQuery();
                db.Close();
            }

            return id > 0 ? true:false;
        }

        public Cliente GetById(int id)
        {
            Cliente cliente = new Cliente();
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_CLIENTES_ID]";
                command.Parameters.AddWithValue("@Id", id);
                db.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    cliente.Id = Convert.ToInt32(dr["Id"]);
                    cliente.Cedula = dr["Cedula"].ToString();
                    cliente.Nombre = dr["Nombre"].ToString();
                    cliente.Telefono = dr["Telefono"].ToString();
                    cliente.Edad = Convert.ToInt32(dr["Edad"]);                    
                }
                db.Close();
            }
            return cliente;
        }



        public bool Update(Cliente model)
        {
            int id = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_CLIENTES_EDITAR]";
                command.Parameters.AddWithValue("@Id", model.Id);
                command.Parameters.AddWithValue("@Cedula", model.Cedula);
                command.Parameters.AddWithValue("@Nombre", model.Nombre);
                command.Parameters.AddWithValue("@Telefono", model.Telefono);
                command.Parameters.AddWithValue("@Edad", model.Edad);
                db.Open();
                id = command.ExecuteNonQuery();
                db.Close();
            }

            return id > 0 ? true : false;
        }

        public bool Delete(int id)
        {
            int deleteRouwCount = 0;
            using (db = new SqlConnection(GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_CLIENTES_ELIMINAR]";
                command.Parameters.AddWithValue("@Id", id);
                db.Open();
                deleteRouwCount = command.ExecuteNonQuery();
                db.Close();
            }

            return deleteRouwCount > 0 ? true : false;
        }
    }
}
