using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
    //evaluar roles
	
	public class CambioClave
	{
        /*public enum enRoles
	{
		SuperAdministrador = 1,
		Administrador = 2,
		Empleado = 3,
	}*/

       
        
        [Display(Name = "Clave nueva")]
         public string nuevaClave {get;set;}

        [Display(Name = "Confirmar clave")]
        //[ DataType(DataType.Password)]
        public string confirmarClave {get;set;}="";    
        public string claveActual{get;set;}="";
        public int Id { get; set; }
	
		public string Nombre { get; set; }
	
		public string Apellido { get; set; }
		
		public string Email { get; set; }
	
		
		public string Avatar { get; set; }="";
		[NotMapped]//Para EF
		public IFormFile AvatarFile { get; set; }
        /* public int Rol { get; set; }
		[NotMapped]//Para EF
		public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";

		public static IDictionary<int, string> ObtenerRoles()
		{
			SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
			Type tipoEnumRol = typeof(enRoles);
			foreach (var valor in Enum.GetValues(tipoEnumRol))
			{
				roles.Add((int)valor, Enum.GetName(tipoEnumRol, valor));
			}
			return roles;
		}*/
	}
        
    
		
		
		
	
}