using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class PositionRepository : IPositionRepository
    {
        private readonly SqlConnection _connection;
        public PositionRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(Position position)
        {
            string sqlExpression = "usp_create_position";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("title", position.Title));
            command.Parameters.Add(new SqlParameter("salary", position.Salary));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_position_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            string sqlExpression = "usp_select_positions";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            List<Position> positions = new List<Position>();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        positions.Add(GetPositionFromReader(reader));
                    }
                }
            }

            await _connection.CloseAsync();

            return positions;
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_position_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            Position position = null;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    position = GetPositionFromReader(reader);
                }
            }

            await _connection.CloseAsync();

            return position ?? new Position();
        }

        public async Task<Position> GetByTitleAsync(string title)
        {
            string sqlExpression = "usp_select_position_by_title";
            Position? position = null;

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("title", title));

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        position = GetPositionFromReader(reader);
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


            return position ?? new Position();
        }

        public async Task UpdateByIdAsync(Position position)
        {
            string sqlExpression = "usp_update_position_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", position.Id));
            command.Parameters.Add(new SqlParameter("title", position.Title));
            command.Parameters.Add(new SqlParameter("salary", position.Salary));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        private Position GetPositionFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string title = reader.GetString("title");
            decimal salary = reader.GetDecimal("salary");
            return new Position(id, title, salary);
        }
    }
}
