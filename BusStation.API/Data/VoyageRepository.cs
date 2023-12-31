﻿using BusStation.API.Data.Abstract;
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

            try
            {
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
            string sqlExpression = "usp_delete_voyage_by_id";

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

        public async Task<IEnumerable<Voyage>> GetAllAsync()
        {
            string sqlExpression = "usp_select_voyages";
            List<Voyage> voyages = new List<Voyage>();

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
                            voyages.Add(GetVoyageFromReader(reader));
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

            return voyages;
        }

        public async Task<Voyage> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_voyage_by_id";
            Voyage? voyage = null;

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
                        voyage = GetVoyageFromReader(reader);
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

            return voyage ?? new Voyage();
        }

        public async Task<IEnumerable<Voyage>> GetByRouteNumberAsync(string routeNumber)
        {
            string sqlExpression = "usp_select_voyages_by_route_number";
            List<Voyage> voyages = new List<Voyage>();

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("routeNumber", routeNumber));

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
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await _connection.CloseAsync();

            }

            return voyages;
        }

        public async Task UpdateByIdAsync(Voyage voyage)
        {
            string sqlExpression = "usp_update_voyage_by_id";

            try
            {
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

        private Voyage GetVoyageFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            DateTime voyageDate = reader.GetDateTime("voyage_date");
            DateTime departureTime = reader.GetDateTime("departure_time");
            DateTime arrivalTime = reader.GetDateTime("arrival_time");
            int routeId = reader.GetInt32("route_id");
            int workerId = reader.GetInt32("worker_id");
            int busId = reader.GetInt32("bus_id");
            string routeNumber = reader.GetString("route_number");
            string workerFullname = reader.GetString("worker_fullname");
            string busNumber = reader.GetString("state_number");

            return new Voyage
            (
                id, 
                voyageDate, 
                departureTime, 
                arrivalTime, 
                routeId, 
                workerId, 
                busId,
                routeNumber,
                workerFullname,
                busNumber
            );
        }
    }
}
