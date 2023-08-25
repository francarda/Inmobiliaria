using Inmobiliaria;

public class Inquilino{
private string dni, nombre, apellido, telefono, mail, direccion, ciudad;
private int idInquilino;

public Inquilino(string nombre, string apellido,string dni, string telefono, string mail, string direccion,string ciudad){
    this.nombre = nombre;
    this.apellido = apellido;
    this.dni = dni;
    this.telefono = telefono;
    this.mail = mail;
    this.direccion = direccion;
    this.ciudad = ciudad;
}
public Inquilino(){}


public string Dni{get;set;}
public string Nombre{get;set;}
public string Apellido{get;set;}
public string Telefono{get;set;}
public string Mail{get;set;}
public string Direccion{get;set;}
public string Ciudad{get;set;}

}