using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class SensorDAL
    {
        public void CreateSensor(Sensor sensor)
        {
            string query = @"
                INSERT INTO SENSORES (sensorType, status, sensorPackId)
                VALUES (@SensorType, @Status, @SensorPackId);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorType", sensor.SensorType),
                new SqlParameter("@Status", sensor.Status),
                new SqlParameter("@SensorPackId", sensor.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Sensor GetSensorById(int sensorId)
        {
            string query = "SELECT * FROM SENSORES WHERE id = @SensorId";
            SqlParameter[] parameters = {
                new SqlParameter("@SensorId", sensorId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Sensor
            {
                ID = Convert.ToInt32(row["id"]),
                SensorType = Convert.ToString(row["sensorType"]),
                Status = Convert.ToBoolean(row["status"]),
                SensorPackID = Convert.ToInt32(row["sensorPackId"])
            };
        }

        public List<Sensor> GetAllSensors()
        {
            List<Sensor> sensors = new List<Sensor>();
            string query = "SELECT * FROM SENSORES";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Sensor sensor = new Sensor
                {
                    ID = Convert.ToInt32(row["id"]),
                    SensorType = Convert.ToString(row["sensorType"]),
                    Status = Convert.ToBoolean(row["status"]),
                    SensorPackID = Convert.ToInt32(row["sensorPackId"])
                };

                sensors.Add(sensor);
            }

            return sensors;
        }

        public void UpdateSensor(Sensor sensor)
        {
            string query = @"
                UPDATE SENSORES
                SET sensorType = @SensorType,
                    status = @Status,
                    sensorPackId = @SensorPackId
                WHERE id = @SensorId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorId", sensor.ID),
                new SqlParameter("@SensorType", sensor.SensorType),
                new SqlParameter("@Status", sensor.Status),
                new SqlParameter("@SensorPackId", sensor.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteSensor(int sensorId)
        {
            string query = "DELETE FROM SENSORES WHERE id = @SensorId";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorId", sensorId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
