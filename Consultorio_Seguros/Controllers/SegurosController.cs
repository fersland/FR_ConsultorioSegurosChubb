using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Consultorio_Seguros.Data;
using Consultorio_Seguros.Models;
using Consultorio_Seguros.DAL;

namespace Consultorio_Seguros.Controllers
{
    public class SegurosController : Controller
    {
        private readonly Seguro_DAL _dal;

        public SegurosController(Seguro_DAL dal)
        {
            _dal = dal;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Seguro> seguros = new List<Seguro>();
            try
            {
                seguros = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(seguros);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Seguro seguro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Los datos son invalidos";
                }

                bool result = _dal.Insert(seguro);

                if (!result)
                {
                    TempData["errorMessage"] = "Este codigo ya existe en un Seguro de la base de datos.";
                    return View();
                }

                TempData["successMessage"] = "Seguro agregado correctamente.";
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
                Seguro seguro = _dal.GetById(id);
                if (seguro.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Seguro con la Id : {id}";
                    return RedirectToAction("Index");
                }

                return View(seguro);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(Seguro seguro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Los datos son invalidos";
                    return View();
                }

                bool result = _dal.Update(seguro);

                if (!result)
                {
                    TempData["errorMessage"] = "Error al actualizar los datos.";
                    return View();
                }

                TempData["successMessage"] = "Los datos han sido actualizados";
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
                Seguro seguro = _dal.GetById(id);
                if (seguro.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Seguro con la Id : {id}";
                    return RedirectToAction("Index");
                }
                return View(seguro);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Seguro model)
        {
            try
            {
                bool result = _dal.Delete(model.Id);

                if (!result)
                {
                    TempData["errorMessage"] = "Error al eliminar los datos.";
                    return View();
                }

                TempData["successMessage"] = "Los datos han sido eliminados";
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
            try
            {
                Seguro seguro = _dal.GetById(id);
                if (seguro.Id == 0)
                {
                    TempData["errorMessage"] = $"No existe un Seguro con la Id : {id}";
                    return RedirectToAction("Index");
                }

                return View(seguro);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
