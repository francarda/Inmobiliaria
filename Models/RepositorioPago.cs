using Inmobiliaria;
namespace Inmobiliaria;

using Inmobiliaria.Models;
using MySql.Data.MySqlClient;
public class RepositorioPago{

    protected readonly string connectionString;
    public RepositorioPago(){

        connectionString= "Server=localhost;User=root; Password=; Database=inmobiliaria; sslMode=none";
    }
    public List<Pago> ObtenerPagos(){
        var res= new List<Pago>();
        using(MySqlConnection conn = new MySqlConnection(connectionString)){
            //conn.Open();
            var sql= "SELECT idPago, numeroDePago, fechaDePago, estado, p.idContrato, idInmueble, idInquilino, p.monto FROM Pagos p INNER JOIN  contratos co ON  p.idContrato=co.idContrato WHERE p.estado=1";
            using(MySqlCommand cmd= new MySqlCommand(sql,conn))
            {
                conn.Open();
                using(MySqlDataReader reader= cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Pago
                        {
                            IdPago= reader.GetInt32("idPago"),
                            Monto=reader.GetFloat("monto"),
                            IdContrato= reader.GetInt32("idContrato"),
                            NumeroDePago= reader.GetInt32("numeroDePago"),
                            FechaDePago = DateOnly.FromDateTime(reader.GetDateTime("fechaDePago")),
                            Estado=reader.GetBoolean("estado"),
                            contrato= new Contrato{
                               IdContrato= reader.GetInt32("idContrato"),
                               IdInmueble=reader.GetInt32("idInmueble"),
                               IdInquilino= reader.GetInt32("idInquilino"),

                            }


                        });
                    }
                } conn.Close();
            }
        }
        return res;

    }
    public int Alta(Pago i){
      var res=-1;

            using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"INSERT INTO Pagos(idContrato, numeroDePago, fechaDePago, estado, monto)
           VALUES(@idContrato, @numeroDePago, @fechaDePago,@estado, @monto);
           SELECT LAST_INSERT_ID()";
  
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@idContrato",i.IdContrato);
            cmd.Parameters.AddWithValue("@numeroDePago",i.NumeroDePago);
            cmd.Parameters.AddWithValue("@fechaDePago",i.FechaDePago.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@estado",i.Estado);
            cmd.Parameters.AddWithValue("@monto",i.Monto);
            conn.Open();
            res= Convert.ToInt32(cmd.ExecuteNonQuery());
            i.IdPago= res;
            conn.Close();    
          }
      }

      return res;
    }
    public int Baja(int id){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"UPDATE Pagos SET estado=0 WHERE idPago=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            res= cmd.ExecuteNonQuery();
             
          }
      }
      return res;
    }
    public int Modificar(Pago i){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"UPDATE Pagos SET idContrato=@idContrato, numeroDePago=@numeroDePago, fechaDePago=@fechaDePago, monto=@monto
           WHERE idPago=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",i.IdPago);
            cmd.Parameters.AddWithValue("@idContrato",i.IdContrato);
            cmd.Parameters.AddWithValue("@numeroDePago",i.NumeroDePago);
            cmd.Parameters.AddWithValue("@fechaDePago",i.FechaDePago.ToString("yyyy-MM-dd")); 
            cmd.Parameters.AddWithValue("@monto",i.Monto);
            conn.Open();
            res= cmd.ExecuteNonQuery();
            conn.Close();    
          }
      }return res;
    }


    public Pago BuscarPorId(int id){
      Pago pago=null;
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= "SELECT idPago, numeroDePago, fechaDePago, p.estado, p.idContrato, idInmueble,idInquilino, p.monto FROM Pagos p INNER JOIN  Contratos co ON  p.idContrato=co.idContrato WHERE idPago=@id";
         
            using(MySqlCommand cmd= new MySqlCommand(sql,conn))
            {
                conn.Open();
                 cmd.Parameters.AddWithValue("@id",id);
                using(MySqlDataReader reader= cmd.ExecuteReader())
                if(reader.Read()){
                    pago= new Pago{
                            IdPago= reader.GetInt32("idPago"),
                            IdContrato= reader.GetInt32("idContrato"),
                            NumeroDePago= reader.GetInt32("numeroDePago"),
                            FechaDePago = DateOnly.FromDateTime(reader.GetDateTime("fechaDePago")),
                            Estado=reader.GetBoolean("estado"),
                            Monto= reader.GetFloat("monto"),
                            contrato= new Contrato{
                               IdContrato= reader.GetInt32("idContrato"),
                               IdInmueble=reader.GetInt32("idInmueble"),
                               IdInquilino= reader.GetInt32("idInquilino"),

                            }


                        };
                    
                } conn.Close();
            }return pago;
        }
       

  }
}

