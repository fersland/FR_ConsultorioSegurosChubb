using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Consultorio_Seguros.Data;
using Consultorio_Seguros.Models;
using Consultorio_Seguros.Process;

namespace Consultorio_Seguros.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Cliente_DAL _dal;

        public ClientesController(Cliente_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                clientes = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                
            }

            return View(clientes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Los datos son invalidos";
                }

                bool result = _dal.Insert(model);

                if (!result)
                {
                    TempData["errorMessage"] = "La cedúla ya existe en la base de datos.";
                    return View();
                }

                TempData["successMessage"] = "Cliente agregado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Cliente cliente = _dal.GetById(id);
                if (cliente.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Cliente con la Id : {id}";
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Cliente model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Los datos son invalidos";
                    return View();
                }

                bool result = _dal.Update(model);  

                if (!result)
                {
                    TempData["errorMessage"] = "Error al actualizar los datos del cliente";
                    return View();
                }

                TempData["successMessage"] = "Los datos del cliente han sido actualizados";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                Cliente cliente = _dal.GetById(id);
                if (cliente.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Cliente con la Id : {id}";
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Cliente model)
        {
            try
            {                
                bool result = _dal.Delete(model.Id);

                if (!result)
                {
                    TempData["errorMessage"] = "Error al eliminar los datos del cliente";
                    return View();
                }

                TempData["successMessage"] = "Los datos del cliente han sido eliminados";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            //var appDbContext = .Asegurados.Include(a => a.Clientes).Include(a => a.Seguros);
            try
            {
                Cliente cliente = _dal.GetById(id);
                if (cliente.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Cliente con la Id : {id}";
                    return RedirectToAction("Index");
                }

                return View(cliente);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        } 

        
    }
}