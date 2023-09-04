using MySql.Data.MySqlClient;
namespace Inmobiliaria;
public class RepositorioPropietario
{
    protected readonly string connectionString;
    public RepositorioPropietario(){

        connectionString= "Server=localhost;User=root; Password=; Database=inmobiliaria; sslMode=none";
    }
    public List<Propietario> ObtenerPropietarios(){
        var res= new List<Propietario>();
        using(MySqlConnection conn = new MySqlConnection(connectionString)){
            //conn.Open();
            var sql= "SELECT * FROM propietarios WHERE estado=1";
            using(MySqlCommand cmd= new MySqlCommand(sql,conn))
            {
                conn.Open();
                using(MySqlDataReader reader= cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Propietario
                        {
                            IdPropietario= reader.GetInt32("idPropietario"),
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
    public int Alta(Propietario p){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"INSERT INTO propietarios(nombre,apellido,dni,telefono,direccion,mail,ciudad, estado)
           VALUES(@nombre,@apellido,@dni,@telefono,@direccion,@mail,@ciudad, @estado);
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
            cmd.Parameters.AddWithValue("@estado",1);
            conn.Open();
            res= Convert.ToInt32(cmd.ExecuteNonQuery());
            p.IdPropietario= res;
            conn.Close();    
          }
      }
      return res;
    }
    public int Baja(int id){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          
          var sql= @"UPDATE propietarios SET estado=0 WHERE idPropietario=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            res= cmd.ExecuteNonQuery();
             
          }
      }
      return res;
    }
    public int Modificar(Propietario p){
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"UPDATE propietarios SET nombre=@nombre, apellido=@apellido, dni=@dni, telefono=@telefono, direccion=@direccion, mail=@mail, ciudad=@ciudad WHERE idPropietario=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn))
          {
            cmd.Parameters.AddWithValue("@id",p.IdPropietario);
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
    public Propietario BuscarPorId(int id){
      Propietario p=null;
      var res=-1;
      using(MySqlConnection conn= new MySqlConnection(connectionString)){
          var sql= @"SELECT * FROM propietarios WHERE idPropietario=@id";
          using (MySqlCommand cmd= new MySqlCommand(sql,conn)){
            cmd.Parameters.AddWithValue("@id",id);
            conn.Open();
            using(MySqlDataReader reader= cmd.ExecuteReader()){
              if(reader.Read()){
                p= new Propietario{
                  IdPropietario= reader.GetInt32("idPropietario"),
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