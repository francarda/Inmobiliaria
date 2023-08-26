using MySql.Data.MySqlClient;

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
            var sql= "SELECT * FROM Contratos";
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
                            inquilino = repoInquilino.BuscarPorId(reader.GetInt32("idInquilino")),
                            inmueble = repoInmueble.BuscarPorId(reader.GetInt32("idInmueble"))
                            
                        });
                    }
                }
            }

        }
        return res;

    }
    public int Alta(Contrato c){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"INSERT INTO Contratos(idInmueble, idInquilino, desde, hasta, monto)
           VALUES(@idContrato, @idInmueble, @idInquilino, @desde, @hasta, @monto);
           SELECT LAST_INSERT_ID()";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@idInmueble",c.IdInmueble);
            cmd.Parameters.AddWithValue("@idInquilino",c.IdInquilino);
            cmd.Parameters.AddWithValue("@desde",c.Desde); 
            cmd.Parameters.AddWithValue("@hasta",c.Hasta);
            cmd.Parameters.AddWithValue("@monto",c.Monto);
            
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
          
          var sql= @"DELETE FROM Contratos WHERE idContrato=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            res= cmd.ExecuteNonQuery();
             
          }
      }
      return res;
    }
    public int Modificar(Contrato p){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"UPDATE Contratos SET idInmueble=@iInmueble, idInquilino=@idInquilino, desde=@desde, hasta=@hasta, monto= @monto
          WHERE idContrato=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",p.IdContrato);
            cmd.Parameters.AddWithValue("@idInmueble",p.IdInmueble);
            cmd.Parameters.AddWithValue("@idInquilino",p.IdInquilino);
            cmd.Parameters.AddWithValue("@desde",p.Desde); 
            cmd.Parameters.AddWithValue("@hasta",p.Hasta);
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
}