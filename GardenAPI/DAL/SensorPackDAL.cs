// DAL/SensorPackDAL.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class SensorPackDAL
    {
        public void CreateSensorPack(SensorPack sensorPack)
        {
            string query = @"
                INSERT INTO SENSOR_PACKS (sensorPackTypeId, clientId)
                VALUES (@SensorPackTypeID, @ClientID);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorPackTypeID", sensorPack.SensorPackTypeID),
                new SqlParameter("@ClientID", (object)sensorPack.ClientID ?? DBNull.Value)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public SensorPack GetSensorPackById(int id)
        {
            string query = "SELECT * FROM SENSOR_PACKS WHERE ID = @ID";
            SqlParameter[] parameters = {
                new SqlParameter("@ID", id)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new SensorPack
            {
                ID = Convert.ToInt32(row["ID"]),
                SensorPackTypeID = Convert.ToInt32(row["sensorPackTypeId"]),
                ClientID = row["clientId"] != DBNull.Value ? Convert.ToInt32(row["clientId"]) : (int?)null
            };
        }

        public List<SensorPack> GetAllSensorPacks()
        {
            List<SensorPack> sensorPacks = new List<SensorPack>();
            string query = "SELECT * FROM SENSOR_PACKS";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                SensorPack sensorPack = new SensorPack
                {
                    ID = Convert.ToInt32(row["ID"]),
                    SensorPackTypeID = Convert.ToInt32(row["sensorPackTypeId"]),
                    ClientID = row["clientId"] != DBNull.Value ? Convert.ToInt32(row["clientId"]) : (int?)null
                };

                sensorPacks.Add(sensorPack);
            }

            return sensorPacks;
        }

        public void UpdateSensorPack(SensorPack sensorPack)
        {
            string query = @"
                UPDATE SENSOR_PACKS
                SET sensorPackTypeId = @SensorPackTypeID,
                    clientId = @ClientID
                WHERE ID = @ID;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@ID", sensorPack.ID),
                new SqlParameter("@SensorPackTypeID", sensorPack.SensorPackTypeID),
                new SqlParameter("@ClientID", (object)sensorPack.ClientID ?? DBNull.Value)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteSensorPack(int id)
        {
            string query = "DELETE FROM SENSOR_PACKS WHERE ID = @ID";

            SqlParameter[] parameters = {
                new SqlParameter("@ID", id)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
