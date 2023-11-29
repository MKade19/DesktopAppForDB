using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class VoyageRepository : IVoyageRepository
    {
        private readonly SqlConnection _connection;
        public VoyageRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(Voyage voyage)
        {
            string sqlExpression = "usp_create_voyage";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("voyageDate", voyage.VoyageDate));
            command.Parameters.Add(new SqlParameter("departureTime", voyage.DepartureTime));
            command.Parameters.Add(new SqlParameter("arrivalTime", voyage.ArrivalTime));
            command.Parameters.Add(new SqlParameter("routeId", voyage.BusRouteId));
            command.Parameters.Add(new SqlParameter("workerId", voyage.WorkerId));
            command.Parameters.Add(new SqlParameter("busId", voyage.BusId));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_voyage_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<IEnumerable<Voyage>> GetAllAsync()
        {
            string sqlExpression = "usp_select_voyages";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            List<Voyage> voyages = new List<Voyage>();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        voyages.Add(GetVoyageFromReader(reader));
                    }
                }
            }

            await _connection.CloseAsync();

            return voyages;
        }

        public async Task<Voyage> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_voyage_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            Voyage voyage = null;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    voyage = GetVoyageFromReader(reader);
                }
            }

            await _connection.CloseAsync();

            return voyage ?? new Voyage();
        }

        public async Task UpdateByIdAsync(Voyage voyage)
        {
            string sqlExpression = "usp_update_voyage_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", voyage.Id));
            command.Parameters.Add(new SqlParameter("voyageDate", voyage.VoyageDate));
            command.Parameters.Add(new SqlParameter("departureTime", voyage.DepartureTime));
            command.Parameters.Add(new SqlParameter("arrivalTime", voyage.ArrivalTime));
            command.Parameters.Add(new SqlParameter("routeId", voyage.BusRouteId));
            command.Parameters.Add(new SqlParameter("workerId", voyage.WorkerId));
            command.Parameters.Add(new SqlParameter("busId", voyage.BusId));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        private Voyage GetVoyageFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            DateTime voyageDate = reader.GetDateTime("voyage_date");
            TimeSpan departureTime = reader.GetTimeSpan(2);
            TimeSpan arrivalTime = reader.GetTimeSpan(3);
            int routeId = reader.GetInt32("route_id");
            int workerId = reader.GetInt32("worker_id");
            int busId = reader.GetInt32("bus_id");
            return new Voyage(id, voyageDate, departureTime, arrivalTime, routeId, workerId, busId);
        }
    }
}
