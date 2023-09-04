using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Controllers
{
    public class ContratoController : Controller
    {
        RepositorioContrato repoContrato=new RepositorioContrato();
        RepositorioInquilino repoInquilino=new RepositorioInquilino();
        RepositorioInmueble repoInmueble=new RepositorioInmueble();
        // GET: Contrato
        public ActionResult Index()
        {
            List<Contrato> lista= repoContrato.ObtenerContratos();
            return View(lista);
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int id)
        {
            Contrato con= repoContrato.BuscarPorId(id);
            return View(con);
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            List<Inquilino> listaInquilinos= repoInquilino.ObtenerInquilinos();
            List<Inmueble> listaInmuebles= repoInmueble.ObtenerInmuebles();
            ViewData["Inquilinos"]= listaInquilinos;
            ViewData["Inmuebles"]= listaInmuebles;
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(Contrato contrato)
        {
             
            try
            {
                
                repoContrato.Alta(contrato);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(int id)
        {
            Contrato contrato= repoContrato.BuscarPorId(id);
            List<Inquilino> listaInquilinos= repoInquilino.ObtenerInquilinos();
            List<Inmueble> listaInmuebles= repoInmueble.ObtenerInmuebles();
            ViewData["Inquilinos"]= listaInquilinos;
            ViewData["Inmuebles"]= listaInmuebles;
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato contrato)
        {
            try
            {
                repoContrato.Modificar(contrato);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Delete/5
        public ActionResult Delete(int id)
        {
            Contrato contrato= repoContrato.BuscarPorId(id);
            return View(contrato);
        }

        // POST: Contrato/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                repoContrato.Baja(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}