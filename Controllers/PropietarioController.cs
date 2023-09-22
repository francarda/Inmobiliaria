using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inmobiliaria.Controllers
{

[Authorize]
public class PropietarioController : Controller
{
   
    private readonly ILogger<HomeController> _logger;

    public PropietarioController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        List<Propietario> propietarios = repositorio.ObtenerPropietarios();
        return View(propietarios);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Propietario propietario)
    {
       
       
       
       try
       {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        repositorio.Alta(propietario);
        return RedirectToAction("Index");

       }
       catch(System.Exception)
       {
            return View();
       }
    }
    
     public IActionResult Editar(int id)
		{
			try
			{
				RepositorioPropietario repositorio = new RepositorioPropietario();
                var propietario = repositorio.BuscarPorId(id);
				return View(propietario);
			}
			catch (Exception ex)
			{//poner breakpoints para detectar errores
				throw;
			}
		}
    [HttpPost]
    public IActionResult Editar(Propietario propietario)
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        repositorio.Modificar(propietario);
        return RedirectToAction("Index");
    }
    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        //Propietario propietario = repositorio.BuscarPorId(id);
        try
        {
            repositorio.Baja(id);
        }
        catch (System.Exception)
        {
            
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Detalles(int id){
        RepositorioPropietario repo= new RepositorioPropietario();
        Propietario propietario= repo.BuscarPorId(id);
        
        return View(propietario);
    }
}
}
        // GET: Propietario/Details/5
/*     
        // GET: Propietario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Propietario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
/*
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;



namespace Inmobiliaria.Controllers;
[Authorize]
public class PropietarioController : Controller
{
   
    private readonly ILogger<HomeController> _logger;

    public PropietarioController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        List<Propietario> propietarios = repositorio.ObtenerPropietarios();
        return View(propietarios);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Propietario propietario)
    {
       try
       {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        repositorio.Alta(propietario);
        return RedirectToAction("Index");

       }
       catch(System.Exception)
       {
        throw;
       }
    }
    
    public IActionResult Editar(int id)
		{
			try
			{
				RepositorioPropietario repositorio = new RepositorioPropietario();
                var propietario = repositorio.BuscarPorId(id);
				return View(propietario);
			}
			catch (Exception ex)
			{//poner breakpoints para detectar errores
				throw;
			}
		}
    [HttpPost]
    public IActionResult Editar(Propietario propietario)
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        repositorio.Modificar(propietario);
        return RedirectToAction("Index");
    }
    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        //Propietario propietario = repositorio.BuscarPorId(id);
        try
        {
            repositorio.Baja(id);
        }
        catch (System.Exception)
        {
            
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Detalles(int id){
        RepositorioPropietario repo= new RepositorioPropietario();
        Propietario propietario= repo.BuscarPorId(id);
        
        return View(propietario);
    }
    //[HttpPost]



}
*/