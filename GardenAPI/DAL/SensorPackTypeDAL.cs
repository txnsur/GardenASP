using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class SensorPackTypeDAL
    {
        public void CreateSensorPackType(SensorPackType sensorPackType)
        {
            string query = @"
                INSERT INTO SENSOR_PACK_TYPES (name, description, salePrice)
                VALUES (@Name, @Description, @SalePrice);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@Name", sensorPackType.Name),
                new SqlParameter("@Description", sensorPackType.Description),
                new SqlParameter("@SalePrice", sensorPackType.SalePrice)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public SensorPackType GetSensorPackTypeById(int sensorPackTypeId)
        {
            string query = "SELECT * FROM SENSOR_PACK_TYPES WHERE id = @SensorPackTypeId";
            SqlParameter[] parameters = {
                new SqlParameter("@SensorPackTypeId", sensorPackTypeId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new SensorPackType
            {
                ID = Convert.ToInt32(row["id"]),
                Name = Convert.ToString(row["name"]),
                Description = Convert.ToString(row["description"]),
                SalePrice = Convert.ToDecimal(row["salePrice"])
            };
        }

        public List<SensorPackType> GetAllSensorPackTypes()
        {
            List<SensorPackType> sensorPackTypes = new List<SensorPackType>();
            string query = "SELECT * FROM SENSOR_PACK_TYPES";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                SensorPackType sensorPackType = new SensorPackType
                {
                    ID = Convert.ToInt32(row["id"]),
                    Name = Convert.ToString(row["name"]),
                    Description = Convert.ToString(row["description"]),
                    SalePrice = Convert.ToDecimal(row["salePrice"])
                };

                sensorPackTypes.Add(sensorPackType);
            }

            return sensorPackTypes;
        }

        public void UpdateSensorPackType(SensorPackType sensorPackType)
        {
            string query = @"
                UPDATE SENSOR_PACK_TYPES
                SET name = @Name,
                    description = @Description,
                    salePrice = @SalePrice
                WHERE id = @SensorPackTypeId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorPackTypeId", sensorPackType.ID),
                new SqlParameter("@Name", sensorPackType.Name),
                new SqlParameter("@Description", sensorPackType.Description),
                new SqlParameter("@SalePrice", sensorPackType.SalePrice)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteSensorPackType(int sensorPackTypeId)
        {
            string query = "DELETE FROM SENSOR_PACK_TYPES WHERE id = @SensorPackTypeId";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorPackTypeId", sensorPackTypeId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
