using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Inmobiliaria.Models;
namespace Inmobiliaria.Models;

public class Pago{
    
  
    [Display(Name="Id Pago")]

    public int IdPago{get;set;}=0;
    [Display(Name="Id Contrato")]
    [Required]
    public int IdContrato{get;set;}=0;
   
    [Display(Name="Numero de pago")]
    public int NumeroDePago{get;set;}=0;
   [Display(Name="Fecha de pago")]
    public DateOnly FechaDePago{get;set;}= DateOnly.FromDateTime(DateTime.Now);
    
    public bool Estado{get;set;}=true;
    public Contrato? contrato{get;set;}
     [Display(Name="Monto")]
    public float Monto{get;set;}=0;

}
/*dotnet-aspnet-codegenerator controller -name "PagoController" -outDir "Controllers" -namespace "Inmobiliaria.Controllers" -f -actions

dotnet-aspnet-codegenerator view Index List -outDir "Views/Pago" -udl --model Inmobiliaria.Models.Pago -f
dotnet-aspnet-codegenerator view Create Create -outDir "Views/Pago" -udl --model Inmobiliaria.Models.Pago -f
dotnet-aspnet-codegenerator view Edit Edit -outDir "Views/Pago" -udl --model Inmobiliaria.Models.Pago -f
dotnet-aspnet-codegenerator view Details Details -outDir "Views/Pago" -udl --model Inmobiliaria.Models.Pago -f
dotnet-aspnet-codegenerator view Delete Delete -outDir "Views/Pago" -udl --model Inmobiliaria.Models.Pago -f*/