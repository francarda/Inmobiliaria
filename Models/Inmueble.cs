using System.ComponentModel.DataAnnotations.Schema;
//using Inmobiliaria.Models;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
[Table("inmuebles")]
public class Inmueble{
[Key]
[Display(Name ="Código")]
public int IdInmueble{ get; set; }
[Required]
[Display(Name ="Dirección")]
public string Direccion{get;set;}="";
public string Uso{get;set;}="";
public string Tipo{get;set;}="";
[Required]
[Display(Name ="Cantidad de ambientes")]
public int CantAmbientes{get;set;}
public string Latitud{get;set;}="";
public string Longitud{get;set;}="";
public float Precio{get;set;}
public bool Visible{get;set;}=true;
[Display(Name ="Dueño")]
public int IdPropietario{get;set;}
[ForeignKey(nameof(IdPropietario))]
public Propietario? Duenio{get;set;}
public string toString(){
    return Direccion;
}
public bool Estado{get;set;}=true;
}
/*dotnet-aspnet-codegenerator controller -name "InmuebleController" -outDir "Controllers" -namespace "Inmobiliaria.Controllers" -f -actions

dotnet-aspnet-codegenerator view Index List -outDir "Views/Inmueble" -udl --model Inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Create Create -outDir "Views/Inmueble" -udl --model Inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Edit Edit-outDir "Views/Inmueble" -udl --model Inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Details Details -outDir "Views/Inmueble" -udl --model Inmobiliaria.Models.Inmueble -f
dotnet-aspnet-codegenerator view Delete Delete -outDir "Views/Inmueble" -udl --model Inmobiliaria.Models.Inmueble -f*/