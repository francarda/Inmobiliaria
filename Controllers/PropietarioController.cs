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
    /*public IActionResult Edit(int id)
    {
        RepositorioPropietario repositorio = new RepositorioPropietario();
        Propietario propietario = repositorio.BuscarPorId(id);
        return View();
    }*/

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
