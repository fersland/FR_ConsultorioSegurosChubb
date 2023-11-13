using Consultorio_Seguros.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Consultorio_Seguros.DAL
{
    public class Asegurado_DAL
    {
        SqlConnection db = null;
        SqlCommand command = null;

        public static IConfiguration Config {  get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");

            Config = builder.Build();
            return Config.GetConnectionString("db");
        }

        public List<Asegurado> GetAll()
        {
            List<Asegurado> aseguradoListar = new List<Asegurado>();
            using(db = new SqlConnection (GetConnectionString()))
            {
                command = db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[Dbo].[SP_ASEGURADOS_LISTAR]";
                db.Open();

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Asegurado asegurado = new Asegurado();
                    Cliente cliente = new Cliente();
                    Seguro seguro = new Seguro();

                    asegurado.Id = Convert.ToInt32(dr["Id"]);
                    cliente.Cedula = dr["CedulaCliente"].ToString();
                    cliente.Nombre = dr["NombreCliente"].ToString();
                    seguro.Codigo = dr["CodigoSeguro"].ToString();
                    seguro.Nombre = dr["NombreSeguro"].ToString();
                    seguro.SemiAsegurada = dr["Asegurada"].ToString();
                    seguro.Prima = dr["Prima"].ToString();

                    //seguro.SemiAsegurada = Convert.ToDecimal(dr["Asegurada"]);
                    //seguro.Prima = Convert.ToDecimal(dr["Prima"]);
                    //aseguradoListar.Add(asegurado, cliente, seguro);

                }
                db.Close();
            }
            return aseguradoListar;
        
        }
    }
}
