using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;
using Microsoft.Extensions.Logging;

namespace GardenAPI.DAL
{
    public class UsuarioDAL
    {
        private readonly ILogger<UsuarioDAL> _logger;

        public UsuarioDAL(ILogger<UsuarioDAL> logger)
        {
            _logger = logger;
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            _logger.LogInformation("Fetching user with email: {Email}", email);

            string query = "SELECT * FROM USUARIOS WHERE email = @Email";
            SqlParameter[] parameters = {
                new SqlParameter("@Email", email)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0)
            {
                _logger.LogWarning("No user found with email: {Email}", email);
                return null;
            }

            DataRow row = result.Rows[0];
            var usuario = new Usuario
            {
                ID = Convert.ToInt32(row["id"]),
                Email = Convert.ToString(row["email"]),
                Password = Convert.ToString(row["password"]),
                Role = Convert.ToString(row["role"]),
                FirstName = Convert.ToString(row["firstName"]),
                LastName = Convert.ToString(row["lastName"]),
                Street = Convert.ToString(row["street"]),
                Zip = Convert.ToString(row["zip"]),
                City = Convert.ToString(row["city"]),
                State = Convert.ToString(row["state"]),
                Country = Convert.ToString(row["country"])
            };

            _logger.LogInformation("User found: {@Usuario}", usuario);
            return usuario;
        }
        public void CreateUsuario(Usuario usuario)
        {
            string query = @"
                INSERT INTO USUARIOS (email, password, role, firstName, lastName, street, zip, city, state, country)
                VALUES (@Email, @Password, @Role, @FirstName, @LastName, @Street, @Zip, @City, @State, @Country);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@Password", usuario.Password),
                new SqlParameter("@Role", usuario.Role ?? "client"),
                new SqlParameter("@FirstName", usuario.FirstName),
                new SqlParameter("@LastName", usuario.LastName),
                new SqlParameter("@Street", usuario.Street ?? (object)DBNull.Value),
                new SqlParameter("@Zip", usuario.Zip ?? (object)DBNull.Value),
                new SqlParameter("@City", usuario.City ?? (object)DBNull.Value),
                new SqlParameter("@State", usuario.State ?? (object)DBNull.Value),
                new SqlParameter("@Country", usuario.Country ?? (object)DBNull.Value)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Usuario GetUsuarioById(int usuarioId)
        {
            string query = "SELECT * FROM USUARIOS WHERE id = @UsuarioId";
            SqlParameter[] parameters = {
                new SqlParameter("@UsuarioId", usuarioId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Usuario
            {
                ID = Convert.ToInt32(row["id"]),
                Email = Convert.ToString(row["email"]),
                Password = Convert.ToString(row["password"]),
                Role = Convert.ToString(row["role"]),
                FirstName = Convert.ToString(row["firstName"]),
                LastName = Convert.ToString(row["lastName"]),
                Street = row["street"] != DBNull.Value ? Convert.ToString(row["street"]) : null,
                Zip = row["zip"] != DBNull.Value ? Convert.ToString(row["zip"]) : null,
                City = row["city"] != DBNull.Value ? Convert.ToString(row["city"]) : null,
                State = row["state"] != DBNull.Value ? Convert.ToString(row["state"]) : null,
                Country = row["country"] != DBNull.Value ? Convert.ToString(row["country"]) : null
            };
        }

        public List<Usuario> GetAllUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = "SELECT * FROM USUARIOS";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Usuario usuario = new Usuario
                {
                    ID = Convert.ToInt32(row["id"]),
                    Email = Convert.ToString(row["email"]),
                    Password = Convert.ToString(row["password"]),
                    Role = Convert.ToString(row["role"]),
                    FirstName = Convert.ToString(row["firstName"]),
                    LastName = Convert.ToString(row["lastName"]),
                    Street = row["street"] != DBNull.Value ? Convert.ToString(row["street"]) : null,
                    Zip = row["zip"] != DBNull.Value ? Convert.ToString(row["zip"]) : null,
                    City = row["city"] != DBNull.Value ? Convert.ToString(row["city"]) : null,
                    State = row["state"] != DBNull.Value ? Convert.ToString(row["state"]) : null,
                    Country = row["country"] != DBNull.Value ? Convert.ToString(row["country"]) : null
                };

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public void UpdateUsuario(Usuario usuario)
        {
            string query = @"
                UPDATE USUARIOS
                SET email = @Email,
                    password = @Password,
                    role = @Role,
                    firstName = @FirstName,
                    lastName = @LastName,
                    street = @Street,
                    zip = @Zip,
                    city = @City,
                    state = @State,
                    country = @Country
                WHERE id = @UsuarioId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@UsuarioId", usuario.ID),
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@Password", usuario.Password),
                new SqlParameter("@Role", usuario.Role ?? "client"),
                new SqlParameter("@FirstName", usuario.FirstName),
                new SqlParameter("@LastName", usuario.LastName),
                new SqlParameter("@Street", usuario.Street ?? (object)DBNull.Value),
                new SqlParameter("@Zip", usuario.Zip ?? (object)DBNull.Value),
                new SqlParameter("@City", usuario.City ?? (object)DBNull.Value),
                new SqlParameter("@State", usuario.State ?? (object)DBNull.Value),
                new SqlParameter("@Country", usuario.Country ?? (object)DBNull.Value)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteUsuario(int usuarioId)
        {
            string query = "DELETE FROM USUARIOS WHERE id = @UsuarioId";

            SqlParameter[] parameters = {
                new SqlParameter("@UsuarioId", usuarioId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
