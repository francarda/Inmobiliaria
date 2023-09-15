using MySql.Data.MySqlClient;
using System.Data.SqlClient;
namespace Inmobiliaria.Models;

public class RepositorioContrato{

    protected readonly string connectionString;
    RepositorioInmueble repoInmueble= new RepositorioInmueble();
    RepositorioInquilino repoInquilino= new RepositorioInquilino();

    public RepositorioContrato(){

        connectionString= "Server=localhost;User=root; Password=; Database=inmobiliaria; sslMode=none";
    }
    public List<Contrato> ObtenerContratos(){
        var res= new List<Contrato>();
        using(MySqlConnection conn = new MySqlConnection(connectionString)){
            //conn.Open();

            /*string sql = @"SELECT Id, Direccion, Ambientes, Superficie, Latitud, Longitud, PropietarioId,
					p.Nombre, p.Apellido
					FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.IdPropietario";*/
            var sql= "SELECT c.idContrato, c.idInmueble, c.idInquilino, desde, hasta, monto, inqui.dni, nombre, apellido,inmu.direccion,uso,tipo,cantAmbientes,precio,latitud,longitud,visible FROM Contratos c inner join Inmuebles inmu on c.idInmueble=inmu.idInmueble inner join Inquilinos inqui on c.idInquilino=inqui.idInquilino WHERE activo=1";
            using(MySqlCommand cmd= new MySqlCommand(sql,conn))
            {
                conn.Open();
                using(MySqlDataReader reader= cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        
                        res.Add(new Contrato
                        {
                            IdContrato = reader.GetInt32("idContrato"), 
                            IdInmueble = reader.GetInt32("idInmueble"),
                            IdInquilino = reader.GetInt32("idInquilino"),
                            Desde = DateOnly.FromDateTime(reader.GetDateTime("desde")),
                            Hasta = DateOnly.FromDateTime(reader.GetDateTime("hasta")),
                            Monto= reader.GetDecimal("monto"),
                            inquilino = new Inquilino()
                              {
                                IdInquilino = reader.GetInt32("idInquilino"),
                                Dni = reader.GetString("dni"), 
                                Nombre = reader.GetString("nombre"), 
                                Apellido = reader.GetString("apellido")

                              },
                              inmueble = new Inmueble()
                             {
                                IdInmueble = reader.GetInt32("idInmueble"),
                                Direccion = reader.GetString("direccion"),
                                Uso = reader.GetString("uso"),
                                Tipo = reader.GetString("tipo"),
                                CantAmbientes = reader.GetInt32("cantAmbientes"),
                                Precio = (float)reader.GetDecimal("precio"),
                                Latitud = reader.GetString("latitud"),
                                Longitud = reader.GetString("longitud"),
                                Visible = reader.GetBoolean("visible"),
                            }

                            
                        });
                    }
                }
                conn.Close();
            }

        }
        return res;

    }
    public int Alta(Contrato c){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"INSERT INTO Contratos(idInmueble, idInquilino, desde, hasta, monto, activo)
           VALUES(@idInmueble, @idInquilino, @desde, @hasta, @monto,@activo);
           SELECT LAST_INSERT_ID()";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@idInmueble",c.IdInmueble);
            cmd.Parameters.AddWithValue("@idInquilino",c.IdInquilino);
            
            cmd.Parameters.AddWithValue("@desde",c.Desde.ToString("yyyy-MM-dd")); 
            cmd.Parameters.AddWithValue("@hasta",c.Hasta.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@monto",c.Monto);
            cmd.Parameters.AddWithValue("@activo",c.Activo);
            conn.Open();
            res= Convert.ToInt32(cmd.ExecuteNonQuery());
            c.IdContrato= res;
            conn.Close();    
          }
      }
      return res;
    }
    public int Baja(int id){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"UPDATE Contratos SET activo=0 WHERE idContrato=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            res= cmd.ExecuteNonQuery();
             conn.Close();
          }
      }
      return res;
    }
    public int Modificar(Contrato p){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"UPDATE Contratos SET idInmueble=@idInmueble, idInquilino=@idInquilino, desde=@desde, hasta=@hasta, monto= @monto
          WHERE idContrato=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",p.IdContrato);
            cmd.Parameters.AddWithValue("@idInmueble",p.IdInmueble);
            cmd.Parameters.AddWithValue("@idInquilino",p.IdInquilino);
            cmd.Parameters.AddWithValue("@desde",p.Desde.ToString("yyyy-MM-dd")); 
            cmd.Parameters.AddWithValue("@hasta",p.Hasta.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@monto",p.Monto);
            conn.Open();
            res= cmd.ExecuteNonQuery();
            conn.Close();
          }
      }
      return res;
    }
    public Contrato BuscarPorId(int id){
      Contrato p=null;
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"SELECT * FROM Contratos WHERE idContrato=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn)){
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            using(MySqlDataReader reader= cmd.ExecuteReader()){
              if(reader.Read()){
                p= new Contrato{
                    IdContrato = reader.GetInt32("idContrato"), 
                    IdInmueble = reader.GetInt32("idInmueble"),
                    IdInquilino = reader.GetInt32("idInquilino"),
                    Desde = DateOnly.FromDateTime(reader.GetDateTime("desde")),
                    Hasta = DateOnly.FromDateTime(reader.GetDateTime("hasta")),
                    Monto= reader.GetDecimal("monto"),
                    inquilino = repoInquilino.BuscarPorId(reader.GetInt32("idInquilino")),
                    inmueble = repoInmueble.BuscarPorId(reader.GetInt32("idInmueble"))
                };
              }
            }
            conn.Close();
          }
      }
      return p;
    }
     public Contrato BuscarPorIdConDatos(int id){
      Contrato p=null;
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"SELECT c.idContrato, c.idInmueble, c.idInquilino, desde, hasta, monto, inqui.dni, nombre, apellido,inmu.direccion,uso,tipo,cantAmbientes,precio,latitud,longitud,visible FROM Contratos c inner join Inmuebles inmu on c.idInmueble=inmu.idInmueble inner join Inquilinos inqui on c.idInquilino=inqui.idInquilino WHERE c.idContrato=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn)){
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
             using(MySqlDataReader reader= cmd.ExecuteReader()){
              if(reader.Read()){
                p= new Contrato{
                            IdContrato=id,
                            IdInmueble = reader.GetInt32("idInmueble"),
                            IdInquilino = reader.GetInt32("idInquilino"),
                            Desde = DateOnly.FromDateTime(reader.GetDateTime("desde")),
                            Hasta = DateOnly.FromDateTime(reader.GetDateTime("hasta")),
                            Monto= reader.GetDecimal("monto"),
                            inquilino = new Inquilino()
                              {
                                IdInquilino = reader.GetInt32("idInquilino"),
                                Dni = reader.GetString("dni"), 
                                Nombre = reader.GetString("nombre"), 
                                Apellido = reader.GetString("apellido")

                              },
                              inmueble = new Inmueble()
                             {
                                IdInmueble = reader.GetInt32("idInmueble"),
                                Direccion = reader.GetString("direccion"),
                                Uso = reader.GetString("uso"),
                                Tipo = reader.GetString("tipo"),
                                CantAmbientes = reader.GetInt32("cantAmbientes"),
                                Precio = (float)reader.GetDecimal("precio"),
                                Latitud = reader.GetString("latitud"),
                                Longitud = reader.GetString("longitud"),
                                Visible = reader.GetBoolean("visible"),
                            }

                            
                        };
                    }
                }
            conn.Close();
          }
      }
      return p;
    }
}