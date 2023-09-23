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
	public class UsuariosController : Controller
	{
		private readonly IConfiguration configuration;
		private readonly IWebHostEnvironment environment;
		private RepositorioUsuario repositorio = new RepositorioUsuario();

		public UsuariosController(IConfiguration configuration, IWebHostEnvironment environment)
		{
			this.configuration = configuration;
			this.environment = environment;
			//this.repositorio = repositorio;
		}
		// GET: Usuarios
		[Authorize(Policy = "Administrador")]
		public ActionResult Index()
		{
			var usuarios = repositorio.ObtenerTodos();
			return View(usuarios);
		}

		// GET: Usuarios/Details/5
		[Authorize(Policy = "Administrador")]
		public ActionResult Details(int id)
		{
			var e = repositorio.ObtenerPorId(id);
			
			return View(e);
		}

		// GET: Usuarios/Create
		
		[Authorize(Policy = "Administrador")]
		public ActionResult Create()
		{
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View();
		}

		// POST: Usuarios/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Administrador")]
		public ActionResult Create(Usuario u)
		{
			if (!ModelState.IsValid){

				ViewBag.Roles = Usuario.ObtenerRoles();
				return View();
			}
			try
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: u.Clave,
								salt: System.Text.Encoding.ASCII.GetBytes("VillaMerlo"),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
				u.Clave = hashed;
				u.Rol = User.IsInRole("Administrador") ? u.Rol : (int)enRoles.Empleado;
				var nbreRnd = Guid.NewGuid();//posible nombre aleatorio
				int res = repositorio.Alta(u);
				u.Id=res;
				if (u.AvatarFile != null && u.Id > 0)
				{
					string wwwPath = environment.WebRootPath;
					string path = Path.Combine(wwwPath, "Uploads");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					//Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
					string fileName = "avatar_" + u.Id + Path.GetExtension(u.AvatarFile.FileName);
					string pathCompleto = Path.Combine(path, fileName);
					u.Avatar = Path.Combine("/Uploads", fileName);
					// Esta operación guarda la foto en memoria en la ruta que necesitamos
					using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
					{
						u.AvatarFile.CopyTo(stream);
					}
					repositorio.Modificacion(u);
				}
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ViewBag.Roles = Usuario.ObtenerRoles();
				return View();
			}
			ViewBag.Roles = Usuario.ObtenerRoles();
				return View();
		}

		// GET: Usuarios/Edit/5
		[Authorize]
		/*public ActionResult Perfil()
		{
			ViewData["Title"] = "Mi perfil";
			var u = repositorio.ObtenerPorEmail(User.Identity.Name);
			//CambioClave cc= new CambioClave();
			//cc.Id = u.Id;
			ViewBag.Roles = Usuario.ObtenerRoles();
			//return RedirectToAction(nameof(CambioClaveController.Perfila), new { cc });
			return View("Edit",u);
			//return View("Editartodo", cc);
		}*/
		
		/*
		// GET: Usuarios/Edit/5
		[Authorize(Policy = "Administrador")]
		public ActionResult Edit(int id)
		{
			ViewData["Title"] = "Editar usuario";
			var u = repositorio.ObtenerPorId(id);
			
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View(u);
		}

		// POST: Usuarios/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public ActionResult Edit(int id, Usuario u)
		{
			var vista = nameof(Edit);//de que vista provengo
			try
			{
				if (!User.IsInRole("Administrador"))//no soy admin
				{
					vista = nameof(Perfil);//solo puedo ver mi perfil
					var usuarioActual = repositorio.ObtenerPorEmail(User.Identity.Name);
					if (usuarioActual.Id != id)//si no es admin, solo puede modificarse él mismo
						return RedirectToAction(nameof(Index), "Home");
				}
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: u.Clave,
								salt: System.Text.Encoding.ASCII.GetBytes("VillaMerlo"),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
				u.Clave = hashed;
				repositorio.Modificacion(u);

				return RedirectToAction(vista);
			}
			catch (Exception ex)
			{//colocar breakpoints en la siguiente línea por si algo falla
				throw;
			}
		}
		*/
		// GET: Usuarios/Delete/5
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id)
		{
			ViewData["Title"] = "Editar usuario";
			var u = repositorio.ObtenerPorId(id);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View(u);
		}

		// POST: Usuarios/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Policy = "Administrador")]
		public ActionResult Delete(int id, Usuario usuario)
		{
			try
			{
				repositorio.Baja(id);
				var ruta = Path.Combine(environment.WebRootPath, "Uploads", $"avatar_{id}" + Path.GetExtension(usuario.Avatar));
				if (System.IO.File.Exists(ruta))
					System.IO.File.Delete(ruta);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		[Authorize]
		public IActionResult Avatar()
		{
			var u = repositorio.ObtenerPorEmail(User.Identity.Name);
			string fileName = "avatar_" + u.Id + Path.GetExtension(u.Avatar);
			string wwwPath = environment.WebRootPath;
			string path = Path.Combine(wwwPath, "Uploads");
			string pathCompleto = Path.Combine(path, fileName);

			//leer el archivo
			byte[] fileBytes = System.IO.File.ReadAllBytes(pathCompleto);
			//devolverlo
			return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
		}

		[Authorize]
		public string AvatarBase64()
		{
			var u = repositorio.ObtenerPorEmail(User.Identity.Name);
			string fileName = "avatar_" + u.Id + Path.GetExtension(u.Avatar);
			string wwwPath = environment.WebRootPath;
			string path = Path.Combine(wwwPath, "Uploads");
			string pathCompleto = Path.Combine(path, fileName);

			//leer el archivo
			byte[] fileBytes = System.IO.File.ReadAllBytes(pathCompleto);
			//devolverlo
			return Convert.ToBase64String(fileBytes);
		}

		[Authorize]
		[HttpPost("[controller]/[action]/{fileName}")]
		public IActionResult FromBase64([FromBody] string imagen, [FromRoute] string fileName)
		{
			//arma el path
			string wwwPath = environment.WebRootPath;
			string path = Path.Combine(wwwPath, "Uploads");
			string pathCompleto = Path.Combine(path, fileName);
			//convierto a arreglo de bytes
			var bytes = Convert.FromBase64String(imagen);
			//lo escribe
			System.IO.File.WriteAllBytes(pathCompleto, bytes);
			return Ok();
		}

		[Authorize]
		public ActionResult Foto()
		{
			try
			{
				var u = repositorio.ObtenerPorEmail(User.Identity.Name);
				var stream = System.IO.File.Open(
						Path.Combine(environment.WebRootPath, u.Avatar.Substring(1)),
						FileMode.Open,
						FileAccess.Read);
				var ext = Path.GetExtension(u.Avatar);
				return new FileStreamResult(stream, $"image/{ext.Substring(1)}");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[Authorize]
		public ActionResult Datos()
		{
			try
			{
				var u = repositorio.ObtenerPorEmail(User.Identity.Name);
				string buffer = "Nombre;Apellido;Email" + Environment.NewLine +
						$"{u.Nombre};{u.Apellido};{u.Email}";
				var stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(buffer));
				var res = new FileStreamResult(stream, "text/plain");
				res.FileDownloadName = "Datos.csv";
				return res;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		[AllowAnonymous]
		// GET: Usuarios/Login/
		public ActionResult LoginModal()
		{
			return PartialView("_LoginModal", new LoginView());
		}

		[AllowAnonymous]
		// GET: Usuarios/Login/
		public ActionResult Login(string returnUrl)
		{
			TempData["returnUrl"] = returnUrl;
			return View();
		}

		// POST: Usuarios/Login/
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginView login)
		{
			try
			{
				var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/Home" : TempData["returnUrl"].ToString();
				if (ModelState.IsValid)
				{
					string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
						password: login.Clave,
						//salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
						salt: System.Text.Encoding.ASCII.GetBytes("VillaMerlo"),
						prf: KeyDerivationPrf.HMACSHA1,
						iterationCount: 1000,
						numBytesRequested: 256 / 8));
					login.Clave=hashed;
					var e = repositorio.ObtenerPorEmail(login.Usuario);
					if (e == null || e.Clave != login.Clave)
					//if (e == null || e.Clave != hashed)
					{
						ModelState.AddModelError("", "El email o la clave no son correctos");
						TempData["returnUrl"] = returnUrl;
						return View();
					}

					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, e.Email),
						new Claim("FullName", e.Nombre + " " + e.Apellido),
						new Claim(ClaimTypes.Role, e.RolNombre),
						new Claim("UsuarioID", e.Id.ToString()),
					};

					var claimsIdentity = new ClaimsIdentity(
							claims, CookieAuthenticationDefaults.AuthenticationScheme);

					await HttpContext.SignInAsync(
							CookieAuthenticationDefaults.AuthenticationScheme,
							new ClaimsPrincipal(claimsIdentity));
					TempData.Remove("returnUrl");
					return Redirect(returnUrl);
				}
				TempData["returnUrl"] = returnUrl;
				return View();
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", ex.Message);
				return View();
			}
		}

		// GET: /salir
		[Route("salir", Name = "logout")]
		public async Task<ActionResult> Logout()
		{
			await HttpContext.SignOutAsync(
					CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home");
		}
		// GET: /cambiarClave
		/*
		[Authorize]
		public ActionResult CambiarClave()
		{
			CambioClave cm= new CambioClave();
			string id=User.FindFirst("UsuarioID").Value;
			/*ViewData["Title"] = "Cambio de clave";
			var u = repositorio.ObtenerPorEmail(User.Identity.Name);
			ViewBag.Roles = Usuario.ObtenerRoles();
			ViewData["id"] = id;
			return View("CambioClave");
		}
		*/
		/*public ActionResult CambioClave()
		{
			CambioClave cm= new CambioClave();
			string id=User.FindFirst("UsuarioID").Value;
			/*ViewData["Title"] = "Cambio de clave";
			var u = repositorio.ObtenerPorEmail(User.Identity.Name);
			ViewBag.Roles = Usuario.ObtenerRoles();
			ViewData["id"] = id;
			return View("CambioClave");
		}*/

	}
}