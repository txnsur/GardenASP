// DAL/PlantillaDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class PlantillaDAL
    {
        public void CreatePlantilla(Plantilla plantilla)
        {
            string query = @"
                INSERT INTO PLANTILLAS (name, description, idealLight, idealTemperature, idealMoisture)
                VALUES (@Name, @Description, @IdealLight, @IdealTemperature, @IdealMoisture);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", plantilla.Name),
                new SqlParameter("@Description", plantilla.Description),
                new SqlParameter("@IdealLight", plantilla.IdealLight),
                new SqlParameter("@IdealTemperature", plantilla.IdealTemperature),
                new SqlParameter("@IdealMoisture", plantilla.IdealMoisture)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Plantilla GetPlantillaById(int plantillaId)
        {
            string query = "SELECT * FROM PLANTILLAS WHERE id = @PlantillaId";
            SqlParameter[] parameters = {
                new SqlParameter("@PlantillaId", plantillaId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Plantilla
            {
                ID = Convert.ToInt32(row["id"]),
                Name = Convert.ToString(row["name"]),
                Description = Convert.ToString(row["description"]),
                IdealLight = Convert.ToInt32(row["idealLight"]),
                IdealTemperature = Convert.ToInt32(row["idealTemperature"]),
                IdealMoisture = Convert.ToInt32(row["idealMoisture"])
            };
        }

        public List<Plantilla> GetAllPlantillas()
        {
            List<Plantilla> plantillas = new List<Plantilla>();
            string query = "SELECT * FROM PLANTILLAS";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Plantilla plantilla = new Plantilla
                {
                    ID = Convert.ToInt32(row["id"]),
                    Name = Convert.ToString(row["name"]),
                    Description = Convert.ToString(row["description"]),
                    IdealLight = Convert.ToInt32(row["idealLight"]),
                    IdealTemperature = Convert.ToInt32(row["idealTemperature"]),
                    IdealMoisture = Convert.ToInt32(row["idealMoisture"])
                };

                plantillas.Add(plantilla);
            }

            return plantillas;
        }

        public void UpdatePlantilla(Plantilla plantilla)
        {
            string query = @"
                UPDATE PLANTILLAS
                SET name = @Name,
                    description = @Description,
                    idealLight = @IdealLight,
                    idealTemperature = @IdealTemperature,
                    idealMoisture = @IdealMoisture
                WHERE id = @PlantillaId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@PlantillaId", plantilla.ID),
                new SqlParameter("@Name", plantilla.Name),
                new SqlParameter("@Description", plantilla.Description),
                new SqlParameter("@IdealLight", plantilla.IdealLight),
                new SqlParameter("@IdealTemperature", plantilla.IdealTemperature),
                new SqlParameter("@IdealMoisture", plantilla.IdealMoisture)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeletePlantilla(int plantillaId)
        {
            string query = "DELETE FROM PLANTILLAS WHERE id = @PlantillaId";

            SqlParameter[] parameters = {
                new SqlParameter("@PlantillaId", plantillaId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
