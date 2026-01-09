using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StokTakip.DataAccess.Context
{
    /// <summary>
    /// Veritabanı bağlantısı ve temel CRUD işlemleri için helper sınıfı
    /// </summary>
    public static class SqlHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["StokTakipDB"].ConnectionString;

        /// <summary>
        /// Connection string'i döner
        /// </summary>
        public static string ConnectionString => connectionString;

        /// <summary>
        /// Yeni bir SqlConnection oluşturur ve açar
        /// </summary>
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// SELECT sorgusu çalıştırır ve DataTable döner
        /// </summary>
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Sorgu çalıştırılırken hata oluştu: {ex.Message}", ex);
            }

            return dataTable;
        }

        /// <summary>
        /// INSERT, UPDATE, DELETE sorguları çalıştırır
        /// </summary>
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int affectedRows = 0;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        affectedRows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Komut çalıştırılırken hata oluştu: {ex.Message}", ex);
            }

            return affectedRows;
        }

        /// <summary>
        /// Tek bir değer dönen sorguları çalıştırır (COUNT, MAX, MIN, vb.)
        /// </summary>
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object result = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        result = command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Scalar sorgu çalıştırılırken hata oluştu: {ex.Message}", ex);
            }

            return result;
        }

        /// <summary>
        /// INSERT işlemi yapar ve eklenen kaydın ID'sini döner
        /// </summary>
        public static int ExecuteInsertWithIdentity(string query, SqlParameter[] parameters = null)
        {
            int newId = 0;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    using (SqlCommand command = new SqlCommand(query + "; SELECT SCOPE_IDENTITY();", connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        object result = command.ExecuteScalar();
                        newId = result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"INSERT işlemi sırasında hata oluştu: {ex.Message}", ex);
            }

            return newId;
        }

        /// <summary>
        /// Transaction içinde birden fazla sorgu çalıştırır
        /// </summary>
        public static bool ExecuteTransaction(Action<SqlConnection, SqlTransaction> transactionAction)
        {
            using (SqlConnection connection = GetConnection())
            {
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    transactionAction(connection, transaction);
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Transaction sırasında hata oluştu: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Veritabanı bağlantısını test eder
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
