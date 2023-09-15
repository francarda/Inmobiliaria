using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Inmobiliaria.Models
{
	public class RepositorioUsuario
	{
		protected readonly string connectionString;
		public RepositorioUsuario()
		{
			 connectionString= "Server=localhost;User=root; Password=; Database=inmobiliaria; sslMode=none";
		}

		public int Alta(Usuario e)
		{
			int res = -1;
			  using(MySqlConnection conn = new MySqlConnection(connectionString)){
			{
				string sql = @"INSERT INTO Usuarios 
					(nombre, apellido, avatar, email, clave, rol) 
					VALUES (@nombre, @apellido, @avatar, @email, @clave, @rol);
					SELECT LAST_INSERT_ID()";
					//SELECT SCOPE_IDENTITY();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (MySqlCommand command = new MySqlCommand(sql, conn))
				{
					//command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", e.Nombre);
					command.Parameters.AddWithValue("@apellido", e.Apellido);
					if (String.IsNullOrEmpty(e.Avatar))
						command.Parameters.AddWithValue("@avatar", DBNull.Value);
					else
						command.Parameters.AddWithValue("@avatar", e.Avatar);
					command.Parameters.AddWithValue("@email", e.Email);
					command.Parameters.AddWithValue("@clave", e.Clave);
					command.Parameters.AddWithValue("@rol", e.Rol);
					conn.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
					//res= Convert.ToInt32(command.ExecuteNonQuery());
					e.Id = res;
					conn.Close();
				}
			}
			return res;
		}
        }

		public int Baja(int id)
		{
			int res = -1;
			  using(MySqlConnection conn = new MySqlConnection(connectionString)){
				string sql = "DELETE FROM Usuarios WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, conn))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					conn.Open();
					res = command.ExecuteNonQuery();
					conn.Close();
				}
			}
			return res;
		}
		public int Modificacion(Usuario e)
		{
			int res = -1;
			using (MySqlConnection conn = new MySqlConnection(connectionString))
			{
				string sql = @"UPDATE Usuarios 
					SET Nombre=@nombre, Apellido=@apellido, Avatar=@avatar, Email=@email, Clave=@clave, Rol=@rol
					WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, conn))
				{
					//command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", e.Nombre);
					command.Parameters.AddWithValue("@apellido", e.Apellido);
					command.Parameters.AddWithValue("@avatar", e.Avatar);
					command.Parameters.AddWithValue("@email", e.Email);
					command.Parameters.AddWithValue("@clave", e.Clave);
					command.Parameters.AddWithValue("@rol", e.Rol);
					command.Parameters.AddWithValue("@id", e.Id);
					conn.Open();
					res = command.ExecuteNonQuery();
					conn.Close();
				}
			}
			return res;
		}

		public IList<Usuario> ObtenerTodos()
		{
			IList<Usuario> res = new List<Usuario>();
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = @"
					SELECT Id, Nombre, Apellido, Avatar, Email, Clave, Rol
					FROM Usuarios";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Usuario e = new Usuario
						{
							Id = reader.GetInt32("Id"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Avatar = reader.GetString("Avatar"),
							Email = reader.GetString("Email"),
							Clave = reader.GetString("Clave"),
							Rol = reader.GetInt32("Rol"),
						};
						res.Add(e);
					}
					connection.Close();
				}
			}
			return res;
		}

		public Usuario ObtenerPorId(int id)
		{
			Usuario? e = null;
			  using(MySqlConnection conn = new MySqlConnection(connectionString)){
				string sql = @"SELECT 
					Id, Nombre, Apellido, Avatar, Email, Clave, Rol 
					FROM Usuarios
					WHERE Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, conn))
				{
					command.Parameters.Add("@id", MySqlDbType.Int16).Value = id;
					command.CommandType = CommandType.Text;
					conn.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Usuario
						{
							Id = reader.GetInt32("Id"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Avatar = reader.GetString("Avatar"),
							Email = reader.GetString("Email"),
							Clave = reader.GetString("Clave"),
							Rol = reader.GetInt32("Rol"),
						};
					}
					conn.Close();
				}
			}
			return e;
		}

		public Usuario ObtenerPorEmail(string email)
		{
			Usuario? e = null;
			  using(MySqlConnection conn = new MySqlConnection(connectionString))
              {
				string sql = @"SELECT
					Id, Nombre, Apellido, Avatar, Email, Clave, Rol FROM Usuarios
					WHERE Email=@email";
				using (MySqlCommand command = new MySqlCommand(sql, conn))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
					conn.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						e = new Usuario
						{
							Id = reader.GetInt32("Id"),
							Nombre = reader.GetString("Nombre"),
							Apellido = reader.GetString("Apellido"),
							Avatar = reader.GetString("Avatar"),
							Email = reader.GetString("Email"),
							Clave = reader.GetString("Clave"),
							Rol = reader.GetInt32("Rol"),
						};
					}
					conn.Close();
				}
			}
			return e;
		}
	}
}

