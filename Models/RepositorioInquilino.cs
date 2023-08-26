using Inmobiliaria;
namespace Inmobiliaria;

using Inmobiliaria.Models;
using MySql.Data.MySqlClient;
public class RepositorioInquilino{

    protected readonly string connectionString;
    public RepositorioInquilino(){

        connectionString= "Server=localhost;User=root; Password=; Database=inmobiliaria; sslMode=none";
    }
    public List<Inquilino> ObtenerInquilinos(){
        var res= new List<Inquilino>();
        using(MySqlConnection conn = new MySqlConnection(connectionString)){
            //conn.Open();
            var sql= "SELECT * FROM Inquilinos";
            using(MySqlCommand cmd= new MySqlCommand(sql,conn))
            {
                conn.Open();
                using(MySqlDataReader reader= cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Inquilino
                        {
                            IdInquilino= reader.GetInt32("idInquilino"),
                            Nombre= reader.GetString("nombre"),
                            Apellido= reader.GetString("apellido"),
                            Dni= reader.GetString("dni"),
                            Telefono= reader.GetString("telefono"),
                            Direccion= reader.GetString("direccion"),
                            Mail= reader.GetString("mail"),
                            Ciudad= reader.GetString("ciudad")

                        });
                    }
                }
            }
        }
        return res;

    }
    public int Alta(Inquilino p){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"INSERT INTO Inquilinos(nombre,apellido,dni,telefono,direccion,mail,ciudad)
           VALUES(@nombre,@apellido,@dni,@telefono,@direccion,@mail,@ciudad);
           SELECT LAST_INSERT_ID()";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@nombre",p.Nombre);
            cmd.Parameters.AddWithValue("@apellido",p.Apellido);
            cmd.Parameters.AddWithValue("@dni",p.Dni); 
            cmd.Parameters.AddWithValue("@telefono",p.Telefono);
            cmd.Parameters.AddWithValue("@direccion",p.Direccion);
            cmd.Parameters.AddWithValue("@mail",p.Mail);
            cmd.Parameters.AddWithValue("@ciudad",p.Ciudad);
            conn.Open();
            res= Convert.ToInt32(cmd.ExecuteNonQuery());
            p.IdInquilino= res;
            conn.Close();    
          }
      }
      return res;
    }
    public int Baja(int id){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"DELETE FROM Inquilinos WHERE idInquilino=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            res= cmd.ExecuteNonQuery();
             
          }
      }
      return res;
    }
    public int Modificar(Inquilino p){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"UPDATE inquilinos SET nombre=@nombre, apellido=@apellido, dni=@dni, telefono=@telefono, direccion=@direccion, mail=@mail, ciudad=@ciudad WHERE idInquilino=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",p.IdInquilino);
            cmd.Parameters.AddWithValue("@nombre",p.Nombre);
            cmd.Parameters.AddWithValue("@apellido",p.Apellido);
            cmd.Parameters.AddWithValue("@dni",p.Dni); 
            cmd.Parameters.AddWithValue("@telefono",p.Telefono);
            cmd.Parameters.AddWithValue("@direccion",p.Direccion);
            cmd.Parameters.AddWithValue("@mail",p.Mail);
            cmd.Parameters.AddWithValue("@ciudad",p.Ciudad);
            conn.Open();
            res= cmd.ExecuteNonQuery();
            conn.Close();
          }
      }
      return res;
    }
    public Inquilino BuscarPorId(int id){
      Inquilino p=null;
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"SELECT * FROM Inquilinos WHERE idInquilino=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn)){
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            using(MySqlDataReader reader= cmd.ExecuteReader()){
              if(reader.Read()){
                p= new Inquilino{
                  IdInquilino= reader.GetInt32("idInquilino"),
                  Nombre= reader.GetString("nombre"),
                  Apellido= reader.GetString("apellido"),
                  Dni= reader.GetString("dni"),
                  Telefono= reader.GetString("telefono"),
                  Direccion= reader.GetString("direccion"),
                  Mail= reader.GetString("mail"),
                  Ciudad= reader.GetString("ciudad")
                };
              }
            }
            conn.Close();
          }
      }
      return p;
    }
}

