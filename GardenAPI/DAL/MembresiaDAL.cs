// DAL/MembresiaDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class MembresiaDAL
    {
        public void CreateMembresia(Membresia membresia)
        {
            string query = @"
                INSERT INTO MEMBRESIAS (name, description, durationDays, price)
                VALUES (@Name, @Description, @DurationDays, @Price);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", membresia.Name),
                new SqlParameter("@Description", membresia.Description),
                new SqlParameter("@DurationDays", membresia.DurationDays),
                new SqlParameter("@Price", membresia.Price)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Membresia GetMembresiaById(int membresiaId)
        {
            string query = "SELECT * FROM MEMBRESIAS WHERE id = @MembresiaId";
            SqlParameter[] parameters = {
                new SqlParameter("@MembresiaId", membresiaId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Membresia
            {
                ID = Convert.ToInt32(row["id"]),
                Name = Convert.ToString(row["name"]),
                Description = Convert.ToString(row["description"]),
                DurationDays = Convert.ToInt32(row["durationDays"]),
                Price = Convert.ToDecimal(row["price"])
            };
        }

        public List<Membresia> GetAllMembresias()
        {
            List<Membresia> membresias = new List<Membresia>();
            string query = "SELECT * FROM MEMBRESIAS";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Membresia membresia = new Membresia
                {
                    ID = Convert.ToInt32(row["id"]),
                    Name = Convert.ToString(row["name"]),
                    Description = Convert.ToString(row["description"]),
                    DurationDays = Convert.ToInt32(row["durationDays"]),
                    Price = Convert.ToDecimal(row["price"])
                };

                membresias.Add(membresia);
            }

            return membresias;
        }

        public void UpdateMembresia(Membresia membresia)
        {
            string query = @"
                UPDATE MEMBRESIAS
                SET name = @Name,
                    description = @Description,
                    durationDays = @DurationDays,
                    price = @Price
                WHERE id = @MembresiaId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@MembresiaId", membresia.ID),
                new SqlParameter("@Name", membresia.Name),
                new SqlParameter("@Description", membresia.Description),
                new SqlParameter("@DurationDays", membresia.DurationDays),
                new SqlParameter("@Price", membresia.Price)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteMembresia(int membresiaId)
        {
            string query = "DELETE FROM MEMBRESIAS WHERE id = @MembresiaId";

            SqlParameter[] parameters = {
                new SqlParameter("@MembresiaId", membresiaId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
