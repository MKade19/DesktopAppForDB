using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class BusRouteRepository : IBusRouteRepository
    {
        private readonly SqlConnection _connection;

        public BusRouteRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(BusRoute route)
        {
            string sqlExpression = "usp_create_route";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("routeNumber", route.RouteNumber));
                command.Parameters.Add(new SqlParameter("departure", route.Departure));
                command.Parameters.Add(new SqlParameter("destination", route.Destination));
                command.Parameters.Add(new SqlParameter("distance", route.Distance));

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
            string sqlExpression = "usp_delete_route_by_id";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("id", id));

                await command.ExecuteNonQueryAsync();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<BusRoute>> GetAllAsync()
        {
            string sqlExpression = "usp_select_bus_routes";
            List<BusRoute> routes = new List<BusRoute>();

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
                            routes.Add(GetRouteFromReader(reader));
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

            return routes;
        }

        public async Task<BusRoute> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_bus_route_by_id";
            BusRoute? route = null;

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
                        route = GetRouteFromReader(reader);
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

            return route ?? new BusRoute();
        }

        public async Task UpdateByIdAsync(BusRoute route)
        {
            string sqlExpression = "usp_update_route_by_id";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("id", route.Id));
                command.Parameters.Add(new SqlParameter("routeNumber", route.RouteNumber));
                command.Parameters.Add(new SqlParameter("departure", route.Departure));
                command.Parameters.Add(new SqlParameter("destination", route.Destination));
                command.Parameters.Add(new SqlParameter("distance", route.Distance));

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

        private BusRoute GetRouteFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            string routeNumber = reader.GetString("route_number");
            string departure = reader.GetString("departure");
            string destination = reader.GetString("destination");
            int distance = reader.GetInt32("distance");
            return new BusRoute(id, routeNumber, departure, destination, distance);
        }
    }
}
