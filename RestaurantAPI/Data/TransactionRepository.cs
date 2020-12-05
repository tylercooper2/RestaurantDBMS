using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestaurantAPI.Models;
using Npgsql;
using System;
using NpgsqlTypes;

namespace RestaurantAPI.Data
{
    public class TransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Connection");
        }

        public async Task<List<Transaction>> GetAll()
        {
            using (NpgsqlConnection sql = new NpgsqlConnection(_connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("\"spTransaction_GetAll\"", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Transaction>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }

        private Transaction MapToValue(NpgsqlDataReader reader)
        {
            return new Transaction()
            {
                Transaction_ID = (int)reader["Transaction_ID"],
                Amount = (double)reader["Amount"],
                Date_Time = (DateTime)reader["Date_Time"]
            };
        }

        public async Task<Transaction> GetById(int id)
        {
            using (NpgsqlConnection sql = new NpgsqlConnection(_connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("\"spTransaction_GetById\"", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("transaction_id", NpgsqlDbType.Integer));
                    cmd.Parameters[0].Value = id;

                    Transaction response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }

                    return response;
                }
            }
        }

        public async Task Insert(Transaction Transaction)
        {
            using (NpgsqlConnection sql = new NpgsqlConnection(_connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("\"spTransaction_InsertValue\"", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("transaction_id", NpgsqlDbType.Integer));
                    cmd.Parameters.Add(new NpgsqlParameter("amount", NpgsqlDbType.Double));
                    cmd.Parameters.Add(new NpgsqlParameter("datetime", System.Data.DbType.DateTime));
                    cmd.Parameters[0].Value = Transaction.Transaction_ID;
                    cmd.Parameters[1].Value = Transaction.Amount;
                    cmd.Parameters[2].Value = Transaction.Date_Time;
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task ModifyById(Transaction Transaction)
        {
            using (NpgsqlConnection sql = new NpgsqlConnection(_connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("\"spTransaction_ModifyById\"", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("transaction_id", NpgsqlDbType.Integer));
                    cmd.Parameters.Add(new NpgsqlParameter("amount", NpgsqlDbType.Double));
                    cmd.Parameters.Add(new NpgsqlParameter("datetime", System.Data.DbType.DateTime));
                    cmd.Parameters[0].Value = Transaction.Transaction_ID;
                    cmd.Parameters[1].Value = Transaction.Amount;
                    cmd.Parameters[2].Value = Transaction.Date_Time;
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task DeleteById(int id)
        {
            using (NpgsqlConnection sql = new NpgsqlConnection(_connectionString))
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand("\"spTransaction_DeleteById\"", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new NpgsqlParameter("transaction_id", NpgsqlDbType.Integer));
                    cmd.Parameters[0].Value = id;
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}

