using Consultorio_Seguros.Data;
using Consultorio_Seguros.Models;
using Microsoft.EntityFrameworkCore;

namespace Consultorio_Seguros.Servicios
{
        
    public class InicioService
    {
        private readonly AppDbContext _context;

        public InicioService(AppDbContext appDbContext)
        {
                this._context = appDbContext;
        }

        public IEnumerable<Asegurado> GetAllSearch(string searchBy, string search)
        {
            switch (searchBy)
            {
                case "Cedula":
                    var byCedula = _context.Asegurados.Include(x => x.Clientes)
                                                        .Include(x => x.Seguros)
                                                        .Where(x => x.Clientes.Cedula == search || search == null);
                    return byCedula;

                case "Codigo":
                    var byCodigo = _context.Asegurados.Include(x => x.Clientes)
                                                            .Include(x => x.Seguros)
                                                            .Where(x => x.Seguros.Codigo == search || search == null);
                    return byCodigo;

                default:
                    var def = _context.Asegurados.Include(x => x.Clientes)
                                                            .Include(x => x.Seguros);
                    return def;
            }

        }

    }
}
