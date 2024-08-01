using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class JardinDAL
    {
        public void CreateJardin(Jardin jardin)
        {
            string query = @"
                INSERT INTO JARDINES (name, description, longitude, latitude, userId, sensorPackId)
                VALUES (@Name, @Description, @Longitude, @Latitude, @UserId, @SensorPackId);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", jardin.Name),
                new SqlParameter("@Description", jardin.Description),
                new SqlParameter("@Longitude", jardin.Longitude),
                new SqlParameter("@Latitude", jardin.Latitude),
                new SqlParameter("@UserId", jardin.UserID),
                new SqlParameter("@SensorPackId", jardin.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Jardin GetJardinById(int jardinId)
        {
            string query = "SELECT * FROM JARDINES WHERE id = @JardinId";
            SqlParameter[] parameters = {
                new SqlParameter("@JardinId", jardinId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Jardin
            {
                ID = Convert.ToInt32(row["id"]),
                Name = Convert.ToString(row["name"]),
                Description = Convert.ToString(row["description"]),
                Longitude = Convert.ToDecimal(row["longitude"]),
                Latitude = Convert.ToDecimal(row["latitude"]),
                UserID = Convert.ToInt32(row["userId"]),
                SensorPackID = Convert.ToInt32(row["sensorPackId"])
            };
        }

        public List<Jardin> GetAllJardines()
        {
            List<Jardin> jardines = new List<Jardin>();
            string query = "SELECT * FROM JARDINES";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Jardin jardin = new Jardin
                {
                    ID = Convert.ToInt32(row["id"]),
                    Name = Convert.ToString(row["name"]),
                    Description = Convert.ToString(row["description"]),
                    Longitude = Convert.ToDecimal(row["longitude"]),
                    Latitude = Convert.ToDecimal(row["latitude"]),
                    UserID = Convert.ToInt32(row["userId"]),
                    SensorPackID = Convert.ToInt32(row["sensorPackId"])
                };

                jardines.Add(jardin);
            }

            return jardines;
        }

        public void UpdateJardin(Jardin jardin)
        {
            string query = @"
                UPDATE JARDINES
                SET name = @Name,
                    description = @Description,
                    longitude = @Longitude,
                    latitude = @Latitude,
                    userId = @UserId,
                    sensorPackId = @SensorPackId
                WHERE id = @JardinId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@JardinId", jardin.ID),
                new SqlParameter("@Name", jardin.Name),
                new SqlParameter("@Description", jardin.Description),
                new SqlParameter("@Longitude", jardin.Longitude),
                new SqlParameter("@Latitude", jardin.Latitude),
                new SqlParameter("@UserId", jardin.UserID),
                new SqlParameter("@SensorPackId", jardin.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteJardin(int jardinId)
        {
            string query = "DELETE FROM JARDINES WHERE id = @JardinId";

            SqlParameter[] parameters = {
                new SqlParameter("@JardinId", jardinId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
