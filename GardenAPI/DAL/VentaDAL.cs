using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using GardenAPI.Models;
using GardenAPI.DataAccess;

namespace GardenAPI.DAL
{
    public class VentaDAL
    {
        public void CreateVenta(Venta venta)
        {
            string query = @"
                INSERT INTO VENTAS (purchaseDate, totalPrice, clientId, sensorPackId)
                VALUES (@PurchaseDate, @TotalPrice, @ClientId, @SensorPackId);
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@PurchaseDate", venta.PurchaseDate),
                new SqlParameter("@TotalPrice", venta.TotalPrice),
                new SqlParameter("@ClientId", venta.ClientID),
                new SqlParameter("@SensorPackId", venta.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public Venta GetVentaById(int ventaId)
        {
            string query = "SELECT * FROM VENTAS WHERE id = @VentaId";
            SqlParameter[] parameters = {
                new SqlParameter("@VentaId", ventaId)
            };

            DataTable result = SqlServerConnection.ExecuteQuery(query, parameters);

            if (result.Rows.Count == 0) return null;

            DataRow row = result.Rows[0];
            return new Venta
            {
                ID = Convert.ToInt32(row["id"]),
                PurchaseDate = Convert.ToDateTime(row["purchaseDate"]),
                TotalPrice = Convert.ToDecimal(row["totalPrice"]),
                ClientID = Convert.ToInt32(row["clientId"]),
                SensorPackID = Convert.ToInt32(row["sensorPackId"])
            };
        }

        public List<Venta> GetAllVentas()
        {
            List<Venta> ventas = new List<Venta>();
            string query = "SELECT * FROM VENTAS";

            DataTable result = SqlServerConnection.ExecuteQuery(query);

            foreach (DataRow row in result.Rows)
            {
                Venta venta = new Venta
                {
                    ID = Convert.ToInt32(row["id"]),
                    PurchaseDate = Convert.ToDateTime(row["purchaseDate"]),
                    TotalPrice = Convert.ToDecimal(row["totalPrice"]),
                    ClientID = Convert.ToInt32(row["clientId"]),
                    SensorPackID = Convert.ToInt32(row["sensorPackId"])
                };

                ventas.Add(venta);
            }

            return ventas;
        }

        public void UpdateVenta(Venta venta)
        {
            string query = @"
                UPDATE VENTAS
                SET purchaseDate = @PurchaseDate,
                    totalPrice = @TotalPrice,
                    clientId = @ClientId,
                    sensorPackId = @SensorPackId
                WHERE id = @VentaId;
            ";

            SqlParameter[] parameters = {
                new SqlParameter("@VentaId", venta.ID),
                new SqlParameter("@PurchaseDate", venta.PurchaseDate),
                new SqlParameter("@TotalPrice", venta.TotalPrice),
                new SqlParameter("@ClientId", venta.ClientID),
                new SqlParameter("@SensorPackId", venta.SensorPackID)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }

        public void DeleteVenta(int ventaId)
        {
            string query = "DELETE FROM VENTAS WHERE id = @VentaId";

            SqlParameter[] parameters = {
                new SqlParameter("@VentaId", ventaId)
            };

            SqlServerConnection.ExecuteNonQuery(query, parameters);
        }
    }
}
