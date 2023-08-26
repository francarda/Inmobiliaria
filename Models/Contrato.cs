
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Inmobiliaria.Models;
namespace Inmobiliaria.Models;
[Table("contrato")]
public class Contrato{
    [Display(Name="Código" )]
    public int IdContrato { get; set;}
    
    [Display(Name="Cod Inquilino")]
    public int IdInquilino{ get; set;}
    
    [Display(Name="Cod Inmueble")]
    public int IdInmueble{get; set;}
    
    public Decimal Monto{ get; set;}
    
    [Display(Name="Fecha inicio")]
    public DateOnly Desde{ get; set;}
    
    [Display(Name="Fecha de finalización")]
    public DateOnly Hasta{ get; set;}
    
    [Display(Name="Inquilino")]
    public Inquilino inquilino{get; set;}
    [Display(Name="Cod Inmueble")]
    public Inmueble inmueble{get; set;}
    
    


}

/*dotnet-aspnet-codegenerator controller -name "ContratoController" -outDir "Controllers" -namespace "Inmobiliaria.Controllers" -f -actions

dotnet-aspnet-codegenerator view Index List -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Create Create -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Edit Edit -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Details Details -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f
dotnet-aspnet-codegenerator view Delete Delete -outDir "Views/Contrato" -udl --model Inmobiliaria.Models.Contrato -f*/