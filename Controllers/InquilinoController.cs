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
    public class InquilinoController : Controller
    {
        [Authorize]
        // GET: Inquilino
        public ActionResult Index()
        {
        RepositorioInquilino repositorio = new RepositorioInquilino();
        List<Inquilino> inquilinos = repositorio.ObtenerInquilinos();
        return View(inquilinos);
            
        }

        // GET: Inquilino/Details/5
        public ActionResult Details(int id)
        {
            RepositorioInquilino repositorio = new RepositorioInquilino();
            Inquilino inquilino = repositorio.BuscarPorId(id);
            return View(inquilino);
        }

        // GET: Inquilino/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {
             RepositorioInquilino repositorio = new RepositorioInquilino();
             repositorio.Alta(inquilino);
                


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilino/Edit/5
        public ActionResult Edit(int id)
        {
            RepositorioInquilino repo= new RepositorioInquilino();
            Inquilino inquilino= repo.BuscarPorId(id);

            return View(inquilino);
        }

        // POST: Inquilino/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Inquilino inqu)
        {
            try
            {
               RepositorioInquilino repo= new RepositorioInquilino();
               int res= repo.Modificar(inqu);
                ViewData["idMod"]=res;
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilino/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
            RepositorioInquilino repo= new RepositorioInquilino();
            Inquilino inqui= repo.BuscarPorId(id);
            return View("Delete",inqui);
        }

        // POST: Inquilino/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, Inquilino inquilino)
        {
            try
            {
               RepositorioInquilino repo= new RepositorioInquilino();
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