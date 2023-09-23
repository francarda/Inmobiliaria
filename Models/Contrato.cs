
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inmobiliaria.Models;
namespace Inmobiliaria.Models;
[Table("contrato")]
public class Contrato{
    [Display(Name="Código contrato" )]
    public int IdContrato { get; set;}=0;
    
    [Display(Name="Cod Inquilino")]
    public int IdInquilino{ get; set;}=0;
    
    [Display(Name="Cod Inmueble")]
    public int IdInmueble{get; set;}=0;
    [Display(Name="Monto")]
    public Decimal Monto{ get; set;}=0;
    
    [Display(Name="Fecha inicio")]
    public DateOnly Desde{ get; set;}
    
    [Display(Name="Fecha de finalización")]
    public DateOnly Hasta{ get; set;}
    
    [Display(Name="Inquilino")]
    public Inquilino? inquilino{get; set;}=null;
   
    [Display(Name="Inmueble")]
    public Inmueble? inmueble{get; set;}=null;
    public bool Activo{get;set;}=true;
    public string toString()
    {
        string mensaje="Id: " + IdContrato + ", Inquilino: " + inquilino.toString() + ", Inmueble: " + inmueble.toString() + ", Monto: " + Monto + ", Desde: " + Desde + ", Hasta: " + Hasta;
        return mensaje;
    }
    public string mensaje()
    {
    string mens="<p> Inquilino: " + inquilino.toString() + ". \n<br /> Inmueble: " + inmueble.toString() + ". \n<br> Desde: " + Desde + ".\n<br> Hasta: " + Hasta + "</p>";
    return mens;
    }


}

/*dotnet-aspnet-codegenerator controller -name "ContratoController" -outDir "Controllers" -namespace "Inmobiliaria.Controllers" -f -actions

dotnet-aspnet-codegenerator view Index List -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Create Create -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Edit Edit -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Details Details -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Delete Delete -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f*/