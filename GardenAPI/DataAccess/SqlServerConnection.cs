// DataAccess/SqlServerConnection.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace GardenAPI.DataAccess
{
    public static class SqlServerConnection
    {
        public static string ConnectionString { get; set; } = @"
            server=TXN;
            database=Garden;
            Integrated Security=true;
        ";

        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"SQL Exception: {e.Message}\nStack Trace: {e.StackTrace}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
                }
            }

            return table;
        }

        public static void ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine($"SQL Exception: {e.Message}\nStack Trace: {e.StackTrace}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}\nStack Trace: {ex.StackTrace}");
                }
            }
        }
    }
}
