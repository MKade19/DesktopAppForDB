using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace BusStation.API.Data
{
    public class BusRepository : IBusRepository
    {
        private readonly SqlConnection _connection;
        public BusRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(Bus bus)
        {
            string sqlExpression = "usp_create_bus";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("stateNumber", bus.StateNumber));
            command.Parameters.Add(new SqlParameter("deliveryDate", bus.DeliveryDate));
            command.Parameters.Add(new SqlParameter("color", bus.Color));
            command.Parameters.Add(new SqlParameter("garageNumber", bus.GarageNumber));
            command.Parameters.Add(new SqlParameter("modelId", bus.BusModelId));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_bus_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<IEnumerable<Bus>> GetAllAsync()
        {
            string sqlExpression = "usp_select_buses";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            List<Bus> buses = new List<Bus>();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        buses.Add(GetBusFromReader(reader));
                    }
                }
            }

            await _connection.CloseAsync();

            return buses;
        }

        public async Task<Bus> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_bus_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            Bus bus = null;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    bus = GetBusFromReader(reader);
                }
            }

            await _connection.CloseAsync();

            return bus ?? new Bus();
        }

        public async Task UpdateByIdAsync(Bus bus)
        {
            string sqlExpression = "usp_update_bus_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", bus.Id));
            command.Parameters.Add(new SqlParameter("stateNumber", bus.StateNumber));
            command.Parameters.Add(new SqlParameter("deliveryDate", bus.DeliveryDate));
            command.Parameters.Add(new SqlParameter("color", bus.Color));
            command.Parameters.Add(new SqlParameter("garageNumber", bus.GarageNumber));
            command.Parameters.Add(new SqlParameter("modelId", bus.BusModelId));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        private Bus GetBusFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string stateNumber = reader.GetString("state_number");
            DateTime deliveryDate = reader.GetDateTime("delivery_date");
            string color = reader.GetString("color");
            int garageNumber = reader.GetInt32("garage_number");
            int modelId = reader.GetInt32("model_id");
            return new Bus(id, stateNumber, deliveryDate, color, garageNumber, modelId);
        }
    }
}
