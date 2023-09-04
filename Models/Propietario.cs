using Inmobiliaria;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Net.Http.Headers;

public class Propietario{
    //atributos
    private string nombre, apellido,dni, telefono,direccion, mail, ciudad;
    private int idPropietario;
    
    //constructores
    public Propietario(string nombre, string apellido, string dni, string telefono, string direccion, string mail,string ciudad, int idPropietario)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.dni = dni;
        this.telefono = telefono;
        this.direccion = direccion;
        this.mail = mail;
        this.ciudad = ciudad;
        this.idPropietario = idPropietario;

    }
    public Propietario(){}
    //getters and setter
    public string Nombre{get; set;}="";
    public string Apellido{get; set;}="";
    public string Dni{get; set;}="";
    public string Direccion{get; set;}="";
    public string Mail{get; set;}="";
    public string Ciudad{get; set;}="";
    public int IdPropietario{get;set;}
    public string Telefono{get; set;}="";
    public bool Estado{get;set;}=true;
    public override string ToString()
		{
			//return $"{Apellido}, {Nombre}";
			return $"{Nombre} {Apellido}";
		}
}

    
