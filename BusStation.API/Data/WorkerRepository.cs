using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly SqlConnection _connection;
        public WorkerRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(Worker worker)
        {
            string sqlExpression = "usp_create_worker";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("fullname", worker.Fullname));
            command.Parameters.Add(new SqlParameter("birthDate", worker.BirthDate));
            command.Parameters.Add(new SqlParameter("experience", worker.Experience));
            command.Parameters.Add(new SqlParameter("positionId", worker.PositionId));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_worker_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            string sqlExpression = "usp_select_workers";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            List<Worker> workers = new List<Worker>();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        workers.Add(GetWorkerFromReader(reader));
                    }
                }
            }

            await _connection.CloseAsync();

            return workers;
        }

        public async Task<Worker> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_worker_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            Worker worker = null;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    worker = GetWorkerFromReader(reader);
                }
            }

            await _connection.CloseAsync();

            return worker ?? new Worker();
        }

        public async Task UpdateByIdAsync(Worker worker)
        {
            string sqlExpression = "usp_update_worker_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", worker.Id));
            command.Parameters.Add(new SqlParameter("fullname", worker.Fullname));
            command.Parameters.Add(new SqlParameter("birthDate", worker.BirthDate));
            command.Parameters.Add(new SqlParameter("experience", worker.Experience));
            command.Parameters.Add(new SqlParameter("positionId", worker.PositionId));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
        private Worker GetWorkerFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string fullname = reader.GetString("fullname");
            DateTime birthDate = reader.GetDateTime("birth_date");
            int experience = reader.GetInt32("experience");
            int positionId = reader.GetInt32("position_id");
            return new Worker(id, fullname, birthDate, experience, positionId);
        }
    }
}
