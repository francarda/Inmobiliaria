using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using NuGet.Protocol.Plugins;


namespace Inmobiliaria.Controllers
{
	public class CambioClaveController : Controller
	{
	
		private readonly IConfiguration configuration;
		private readonly IWebHostEnvironment environment;
		private RepositorioUsuario repositorio = new RepositorioUsuario();

		public CambioClaveController(IConfiguration configuration, IWebHostEnvironment environment)
		{
			this.configuration = configuration;
			this.environment = environment;
			//this.repositorio = repositorio;
		}
		// GET: Usuarios
		//[Route("cambiar", Name = "cambiar")]	
		/*public ActionResult Perfila(CambioClave cc)
		{
			ViewData["Title"] = "Mi perfil";
			
			return View("Editartodo", cc);
		}*/
		
		//[Authorize(Policy = "Administrador")]
		public ActionResult CambiarTodo(int id)
		{	
			if (!User.IsInRole("Administrador"))//no soy admin
				{
					
					var usuarioActual = repositorio.ObtenerPorEmail(User.Identity.Name);
					Console.WriteLine(usuarioActual.Id);
					
					if (usuarioActual.Id != id){
						
						TempData["error"] = "No puede actualizar otro usuario";
						return RedirectToAction(nameof(Index), "Home");
					}
					
					
				}
			
			ViewData["Title"] = "Editar usuario";
			var u = repositorio.ObtenerPorId(id);
			
			ViewBag.Roles = Usuario.ObtenerRoles();
			CambioClave cm=new CambioClave();
			
			cm.Nombre=u.Nombre;
			cm.Apellido=u.Apellido;
			cm.Email=u.Email;
			cm.Id=u.Id;
			cm.Avatar=u.Avatar;
			
			//ViewBag.Roles = Usuario.ObtenerRoles();
			return View("Editartodo", cm);
		}
		//post
		[HttpGet] // Agrega este atributo para permitir solicitudes HTTP GET
		[Authorize]
		
		public ActionResult Perfil()
		{
			
    
			ViewData["Title"] = "Mi perfil";
			var u = repositorio.ObtenerPorEmail(User.Identity.Name);
			//CambioClave cc= new CambioClave();
			//cc.Id = u.Id;
			ViewBag.Roles = Usuario.ObtenerRoles();
			CambioClave cm=new CambioClave();
			
			cm.Nombre=u.Nombre;
			cm.Apellido=u.Apellido;
			cm.Email=u.Email;
			cm.Id=u.Id;
			cm.Avatar=u.Avatar;
			//return RedirectToAction(nameof(CambioClaveController.Perfila), new { cc });
			
			return View("Editartodo",cm);
			//return View("Editartodo", cc);
		}


		public ActionResult EditarNombres(CambioClave cc){
			
			if (!User.IsInRole("Administrador"))//no soy admin
				{
					
					var usuarioActual = repositorio.ObtenerPorEmail(User.Identity.Name);
					Console.WriteLine(usuarioActual.Id);
					Console.WriteLine(cc.Id);
					if (usuarioActual.Id != cc.Id){
						
						TempData["error"] = "No puede actualizar otro usuario";
						return RedirectToAction(nameof(Index), "Home");
					}
					
					
				}

			int res= repositorio.ModificarNombres(cc.Nombre,cc.Apellido,cc.Id, cc.Email);
			//ViewData["mensaje"] ="Nombres guardados";
			if(res>0){
				TempData["exito"] = "Nombres actualizados";
			}else{
			TempData["error"] = "No se pudo actualizar los datos";
			}
			return RedirectToAction("CambiarTodo", "CambioClave", new { id = cc.Id });
		}


		public ActionResult CambioClave(CambioClave cc)
		{

			if (!User.IsInRole("Administrador"))//no soy admin
				{
					
					var usuarioActual = repositorio.ObtenerPorEmail(User.Identity.Name);
					Console.WriteLine(usuarioActual.Id);
					Console.WriteLine(cc.Id);
					if (usuarioActual.Id != cc.Id){
						
						TempData["error"] = "No puede actualizar otro usuario";
						return RedirectToAction(nameof(Index), "Home");
					}
					
					
				}
			if(cc.nuevaClave == cc.confirmarClave){
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: cc.nuevaClave,
								salt: System.Text.Encoding.ASCII.GetBytes("VillaMerlo"),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
				cc.nuevaClave=hashed;
				repositorio.CambiarClave(cc.nuevaClave, cc.Id);
				TempData["exito"] = "Clave Actualizada";
				
			}else{
			TempData["error"] = "Las Contraseñas ingresadas son distintas";
			}
			return RedirectToAction("CambiarTodo", "CambioClave", new { id = cc.Id });
			
			
		}
		[HttpPost]

		public ActionResult CambiarImagen(CambioClave cc){
			if (!User.IsInRole("Administrador"))//no soy admin
				{
					
					var usuarioActual = repositorio.ObtenerPorEmail(User.Identity.Name);
					Console.WriteLine(usuarioActual.Id);
					Console.WriteLine(cc.Id);
					if (usuarioActual.Id != cc.Id){
						
						TempData["error"] = "No puede actualizar otro usuario";
						return RedirectToAction(nameof(Index), "Home");
					}
					
					
				}
		string res= repositorio.FotoDireccion(cc.Id);

		if (cc.AvatarFile != null && cc.Id > 0)
    {
			
        try
        {	
			
            string wwwPath = environment.WebRootPath;
            string uploadPath = Path.Combine(wwwPath, "Uploads");
			
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string fileName = "avatar_" + cc.Id + Path.GetExtension(cc.AvatarFile.FileName);
            string pathCompleto = Path.Combine(uploadPath, fileName);
            cc.Avatar = Path.Combine("/Uploads", fileName);
			
			

            using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
            {
                cc.AvatarFile.CopyTo(stream);
            }
			repositorio.CambiarFoto(cc.Avatar, cc.Id);
			TempData["exito"] = "Foto Actualizada";
			
            return RedirectToAction("CambiarTodo", "CambioClave", new { id = cc.Id });
        }
        catch (Exception ex)
        {
           
            return RedirectToAction("CambiarTodo", "CambioClave", new { id = cc.Id });
        }
        }
		TempData["error"] = "Error al actualizar la foto";
		return RedirectToAction("CambiarTodo", "CambioClave", new { id = cc.Id });
    }
		
    
  

	
	}
}