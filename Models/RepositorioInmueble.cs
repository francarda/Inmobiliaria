using Inmobiliaria;
namespace Inmobiliaria;

using Inmobiliaria.Models;
using MySql.Data.MySqlClient;
public class RepositorioInmueble{

    protected readonly string connectionString;
    public RepositorioInmueble(){

        connectionString= "Server=localhost;User=root; Password=; Database=inmobiliaria; sslMode=none";
    }
    public List<Inmueble> ObtenerInmuebles(){
        var res= new List<Inmueble>();
        using(MySqlConnection conn = new MySqlConnection(connectionString)){
            //conn.Open();
            var sql= "SELECT idInmueble, i.direccion, uso, tipo, cantAmbientes,precio, latitud, longitud,visible,i.idPropietario, p.Nombre, p.Apellido FROM inmuebles i INNER JOIN Propietarios p ON  i.idPropietario=p.IdPropietario";
            using(MySqlCommand cmd= new MySqlCommand(sql,conn))
            {
                conn.Open();
                using(MySqlDataReader reader= cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Inmueble
                        {
                            IdInmueble= reader.GetInt32("idInmueble"),
                            Direccion= reader.GetString("direccion"),
                            Uso= reader.GetString("uso"),
                            Tipo= reader.GetString("tipo"),
                            CantAmbientes= reader.GetInt32("cantAmbientes"),
                            Latitud= reader.GetString("latitud"),
                            Longitud= reader.GetString("longitud"),
                            Visible=reader.GetBoolean("visible"),
                            IdPropietario=reader.GetInt32("idPropietario"),
                            Precio=reader.GetFloat("precio"),
                            Duenio= new Propietario{
                                IdPropietario = reader.GetInt32("IdPropietario"),
                                Nombre=reader.GetString("Nombre"),
                                Apellido=reader.GetString("apellido")

                            }


                        });
                    }
                }
            }
        }
        return res;

    }
    public int Alta(Inmueble i){
      var res=-1;
      Console.WriteLine(i.Direccion);
            using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"INSERT INTO Inmuebles(direccion, uso, tipo, cantAmbientes, latitud, longitud, precio, visible, idPropietario)
           VALUES(@direccion, @uso, @tipo, @cantAmbientes,@precio, @latitud, @longitud, @visible, @idPropietario);
           SELECT LAST_INSERT_ID();";
  
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@direccion",i.Direccion);
            cmd.Parameters.AddWithValue("@uso",i.Uso);
            cmd.Parameters.AddWithValue("@tipo",i.Tipo); 
            cmd.Parameters.AddWithValue("@cantAmbientes",i.CantAmbientes);
            cmd.Parameters.AddWithValue("@latitud",i.Latitud);
            cmd.Parameters.AddWithValue("@longitud",i.Longitud);
            cmd.Parameters.AddWithValue("@visible",i.Visible);
            cmd.Parameters.AddWithValue("@idPropietario",i.IdPropietario);
            cmd.Parameters.AddWithValue("@precio",i.Precio);
            conn.Open();
            res= Convert.ToInt32(cmd.ExecuteNonQuery());
            i.IdInmueble= res;
            conn.Close();    
          }Console.WriteLine(i.IdInmueble);
      }

      return res;
    }
    public int Baja(int id){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"DELETE FROM inmuebles WHERE idInmueble=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            res= cmd.ExecuteNonQuery();
             
          }
      }
      return res;
    }
    public int Modificar(Inmueble i){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
         var sql= @"UPDATE inmuebles SET direccion=@direccion, uso=@uso, tipo= @tipo, cantAmbientes=@cantAmbientes, latitud=@latitud, longitud=@longitud, visible=@visible, idPropietario=@idPropietario WHERE idInmueble=@idInmueble";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@direccion",i.Direccion);
            cmd.Parameters.AddWithValue("@uso",i.Uso);
            cmd.Parameters.AddWithValue("@tipo",i.Tipo); 
            cmd.Parameters.AddWithValue("@cantAmbientes",i.CantAmbientes);
            cmd.Parameters.AddWithValue("@latitud",i.Latitud);
            cmd.Parameters.AddWithValue("@longitud",i.Longitud);
            cmd.Parameters.AddWithValue("@visible",i.Visible);
            cmd.Parameters.AddWithValue("@idPropietario",i.IdPropietario);
            cmd.Parameters.AddWithValue("@precio",i.Precio);
            cmd.Parameters.AddWithValue("@idInmueble", i.IdInmueble);
            conn.Open();
            res= cmd.ExecuteNonQuery();

            conn.Close();    
          }
      }
      return res;
    }
    public Inmueble BuscarPorId(int id){
      Inmueble i=null;
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= "SELECT idInmueble, i.direccion, uso, tipo, precio,cantAmbientes, latitud, longitud,visible,i.idPropietario, p.Nombre, p.Apellido FROM inmuebles i INNER JOIN Propietarios p ON i.idPropietario=p.IdPropietario WHERE idInmueble=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn)){
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            using(MySqlDataReader reader= cmd.ExecuteReader()){
              if(reader.Read()){
                i= new Inmueble{
                  IdInmueble=reader.GetInt32("idInmueble"),
                  Direccion= reader.GetString("direccion"),
                  Uso= reader.GetString("uso"),
                  Tipo= reader.GetString("tipo"),
                  CantAmbientes= reader.GetInt32("cantAmbientes"),
                  Latitud= reader.GetString("latitud"),
                  Longitud= reader.GetString("longitud"),
                  Visible=reader.GetBoolean("visible"),
                  IdPropietario=reader.GetInt32("idPropietario"),
                  Precio=reader.GetFloat("precio"),
                  Duenio= new Propietario{
                      IdPropietario = reader.GetInt32("IdPropietario"),
                      Nombre=reader.GetString("Nombre"),
                      Apellido=reader.GetString("apellido")

                }
              };
            }
            conn.Close();
          }
      }
      return i;
    }
  }
}

