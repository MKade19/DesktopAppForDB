using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            string sqlExpression = "usp_select_users_by_username";
            User? user = null;

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("username", username));

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        user = GetUserFromReader(reader);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }


            return user ?? new User();
        }

        public async Task CreateOne(User user)
        {
            string sqlExpression = "usp_create_user";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("username", user.Username));
                command.Parameters.Add(new SqlParameter("password", user.Password));
                command.Parameters.Add(new SqlParameter("salt", user.Salt));

                await command.ExecuteReaderAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        private User GetUserFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string username = reader.GetString("username");
            string password = reader.GetString("password");
            byte[] salt = reader.GetSqlBytes(3).Value;
            string role = reader.GetString("role");
            
            return new User(id, username, password, salt, role);
        }
    }
}
