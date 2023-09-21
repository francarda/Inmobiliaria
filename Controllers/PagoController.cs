using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmobiliaria.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        RepositorioPago repoPago = new RepositorioPago();
        RepositorioContrato repoContrato= new RepositorioContrato();
        Pago pago= new Pago();
        // GET: Pago
        public ActionResult Index()
        {
            List<Pago> pagos= repoPago.ObtenerPagos(); 
           
            return View(pagos);
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            Pago pago= repoPago.BuscarPorId(id);
            Contrato cont= repoContrato.BuscarPorIdConDatos(pago.IdContrato);
            pago.contrato=cont;
            return View(pago);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            List<Contrato> contratos=repoContrato.ObtenerContratos();
            ViewData["contratos"]= contratos;
            return View();
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago p)
        {
            try
            {
                
                repoPago.Alta(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {   
            pago= repoPago.BuscarPorId(id);
            List<Contrato> contratos=repoContrato.ObtenerContratos();
            ViewData["contratos"]= contratos;
            return View(pago);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago pag)
        {
            try
            {
                repoPago.Modificar(pag);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id)
        {
             pago= repoPago.BuscarPorId(id);
            return View(pago);
        }

        // POST: Pago/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
              repoPago.Baja(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}