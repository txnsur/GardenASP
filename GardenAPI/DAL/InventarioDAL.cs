using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class InventarioDAL
    {
        public void CreateInventario(Inventario inventario)
        {
            string query = @"
                INSERT INTO INVENTARIO (sensorPackTypeId, stock)
                VALUES (@SensorPackTypeId, @Stock);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@SensorPackTypeId", inventario.SensorPackTypeID),
                new SqlParameter("@Stock", inventario.Stock)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Inventario GetInventarioById(int inventarioId)
        {
            string query = "SELECT * FROM INVENTARIO WHERE id = @InventarioId";
            SqlParameter[] parameters = {
                new SqlParameter("@InventarioId", inventarioId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Inventario
            {
                ID = Convert.ToInt32(row["id"]),
                SensorPackTypeID = Convert.ToInt32(row["sensorPackTypeId"]),
                Stock = Convert.ToInt32(row["stock"])
            };
        }

        public List<Inventario> GetAllInventarios()
        {
            List<Inventario> inventarios = new List<Inventario>();
            string query = "SELECT * FROM INVENTARIO";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Inventario inventario = new Inventario
                {
                    ID = Convert.ToInt32(row["id"]),
                    SensorPackTypeID = Convert.ToInt32(row["sensorPackTypeId"]),
                    Stock = Convert.ToInt32(row["stock"])
                };

                inventarios.Add(inventario);
            }

            return inventarios;
        }

        public void UpdateInventario(Inventario inventario)
        {
            string query = @"
                UPDATE INVENTARIO
                SET sensorPackTypeId = @SensorPackTypeId,
                    stock = @Stock
                WHERE id = @InventarioId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@InventarioId", inventario.ID),
                new SqlParameter("@SensorPackTypeId", inventario.SensorPackTypeID),
                new SqlParameter("@Stock", inventario.Stock)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteInventario(int inventarioId)
        {
            string query = "DELETE FROM INVENTARIO WHERE id = @InventarioId";

            SqlParameter[] parameters = {
                new SqlParameter("@InventarioId", inventarioId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
