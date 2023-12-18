using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class BusModelRepository : IBusModelRepository
    {
        private readonly SqlConnection _connection;

        public BusModelRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }

        public async Task CreateOneAsync(BusModel model)
        {
            string sqlExpression = "usp_create_bus_model";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("title", model.Title));
                command.Parameters.Add(new SqlParameter("producerId", model.ProducerId));

                await command.ExecuteNonQueryAsync();
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

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_bus_model_by_id";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("id", id));

                await command.ExecuteNonQueryAsync();
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

        public async Task<IEnumerable<BusModel>> GetAllAsync()
        {
            string sqlExpression = "usp_select_bus_models";
            List<BusModel> busModels = new List<BusModel>();

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            busModels.Add(GetModelFromReader(reader));
                        }
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

            return busModels;
        }

        public async Task<BusModel> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_bus_model_by_id";
            BusModel? busModel = null;

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("id", id));

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        busModel = GetModelFromReader(reader);
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

            return busModel ?? new BusModel();
        }

        public async Task<BusModel> GetByTitleAsync(string title)
        {
            string sqlExpression = "usp_select_bus_model_by_title";
            BusModel? busModel = null;

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
                        busModel = GetModelFromReader(reader);
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

            return busModel ?? new BusModel();
        }

        public async Task UpdateByIdAsync(BusModel model)
        {
            string sqlExpression = "usp_update_bus_model_by_id";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("id", model.Id));
                command.Parameters.Add(new SqlParameter("title", model.Title));
                command.Parameters.Add(new SqlParameter("producerId", model.ProducerId));

                await command.ExecuteNonQueryAsync();
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

        private BusModel GetModelFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string title = reader.GetString("title");
            int producerId = reader.GetInt32("producer_id");
            string produserName = reader.GetString("producer_name");
            return new BusModel(id, title, producerId, produserName);
        }
    }
}
