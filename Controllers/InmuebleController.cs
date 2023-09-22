using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class InmuebleController : Controller
    {
        private RepositorioInmueble repo= new RepositorioInmueble();
        private Inmueble inmu;
        // GET: Inmueble
        public ActionResult Index()
        {
            List<Inmueble> lista= repo.ObtenerInmuebles();

            return View(lista);
        }

        // GET: Inmueble/Details/5
        public ActionResult Details(int id)
        {
            RepositorioInmueble repoIn= new RepositorioInmueble();
            inmu=repoIn.BuscarPorId(id);
            return View(inmu);
        }

        // GET: Inmueble/Create
        public ActionResult Create()
        {
            RepositorioPropietario propi= new RepositorioPropietario();
            List<Propietario> lista;
            lista= propi.ObtenerPropietarios();

            ViewData["lista"]= lista;
            return View();
        }

        // POST: Inmueble/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble inmueble)
        {
            try
            {
                
                repo.Alta(inmueble);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmueble/Edit/5
        public ActionResult Edit(int id)
        {
             RepositorioPropietario propi= new RepositorioPropietario();
            List<Propietario> lista;
            lista= propi.ObtenerPropietarios();

            ViewData["lista"]= lista;
            inmu=repo.BuscarPorId(id);
            return View(inmu);
        }

        // POST: Inmueble/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inmueble inmueble)
        {
           
            
            try
            {
                repo.Modificar(inmueble);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmueble/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            inmu=repo.BuscarPorId(id);
            return View(inmu);
        }

        // POST: Inmueble/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                repo.Baja(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}