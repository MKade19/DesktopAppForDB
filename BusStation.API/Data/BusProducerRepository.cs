using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class BusProducerRepository : IBusProducerRepository
    {
        private readonly SqlConnection _connection;

        public BusProducerRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }

        public async Task CreateOneAsync(BusProducer producer)
        {
            string sqlExpression = "usp_create_bus_producer";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("title", producer.Title));
            command.Parameters.Add(new SqlParameter("town", producer.Town));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<IEnumerable<BusProducer>> GetAllAsync()
        {
            string sqlExpression = "usp_select_bus_producers";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            List<BusProducer> busProducers = new List<BusProducer>();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        busProducers.Add(GetProducerFromReader(reader));
                    }
                }
            }

            await _connection.CloseAsync();

            return busProducers;
        }

        public async Task<BusProducer> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_bus_producer_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            BusProducer busProducer = null;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    busProducer = GetProducerFromReader(reader);
                }
            }

            await _connection.CloseAsync();

            return busProducer ?? new BusProducer();
        }

        public async Task UpdateByIdAsync(BusProducer producer)
        {
            string sqlExpression = "usp_update_bus_producer_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", producer.Id));
            command.Parameters.Add(new SqlParameter("title", producer.Title));
            command.Parameters.Add(new SqlParameter("town", producer.Town));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_bus_producer_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        private BusProducer GetProducerFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string title = reader.GetString("title");
            string town = reader.GetString("town");
            return new BusProducer(id, title, town);
        }
    }
}
