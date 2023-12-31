﻿using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class RepairmentRepository : IRepairmentRepository
    {
        private readonly SqlConnection _connection;
        public RepairmentRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(Repairment repairment)
        {
            string sqlExpression = "usp_create_repairment";

            try
            {
                await _connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("beginDate", repairment.BeginDate));
                command.Parameters.Add(new SqlParameter("endDate", repairment.EndDate));
                command.Parameters.Add(new SqlParameter("workerId", repairment.WorkerId));
                command.Parameters.Add(new SqlParameter("busId", repairment.BusId));
                command.Parameters.Add(new SqlParameter("malfunction", repairment.Malfunction));

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

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_repairment_by_id";

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

        public async Task<IEnumerable<Repairment>> GetAllAsync()
        {
            string sqlExpression = "usp_select_repairments";
            List<Repairment> repairments = new List<Repairment>();

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
                            repairments.Add(GetRepairmentFromReader(reader));
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

            return repairments;
        }

        public async Task<IEnumerable<Repairment>> GetByBusNumberAsync(string busNumber)
        {
            string sqlExpression = "usp_search_repairments_by_bus_number";
            List<Repairment> repairments = new List<Repairment>();

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("busNumber", busNumber));

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            repairments.Add(GetRepairmentFromReader(reader));
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

            return repairments;
        }

        public async Task<Repairment> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_repairment_by_id";
            Repairment? repairment = null;

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
                        repairment = GetRepairmentFromReader(reader);
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

            return repairment ?? new Repairment();
        }

        public async Task<IEnumerable<RepairmentYearWithCount>> GetYearsWithCountAsync()
        {
            string sqlExpression = "usp_select_repairments_years_with_count";
            List<RepairmentYearWithCount> repairmentYears = new List<RepairmentYearWithCount>();

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
                            repairmentYears.Add(GetYearWithCountFromReader(reader));
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

            return repairmentYears;
        }

        public async Task UpdateByIdAsync(Repairment repairment)
        {
            string sqlExpression = "usp_update_repairment_by_id";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("id", repairment.Id));
                command.Parameters.Add(new SqlParameter("beginDate", repairment.BeginDate));
                command.Parameters.Add(new SqlParameter("endDate", repairment.EndDate));
                command.Parameters.Add(new SqlParameter("workerId", repairment.WorkerId));
                command.Parameters.Add(new SqlParameter("busId", repairment.BusId));
                command.Parameters.Add(new SqlParameter("malfunction", repairment.Malfunction));

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

        private Repairment GetRepairmentFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            DateTime beginDate = reader.GetDateTime("begin_date");
            DateTime endDate = reader.GetDateTime("end_date");
            int workerId = reader.GetInt32("worker_id");
            int busId = reader.GetInt32("bus_id");
            string malfunction = reader.GetString("malfunction");
            string workerName = reader.GetString("worker_fullname");
            string busNumber = reader.GetString("state_number");
            return new Repairment
            (
                id, 
                beginDate, 
                endDate, 
                workerId, 
                busId, 
                malfunction, 
                workerName, 
                busNumber
            );
        }

        private RepairmentYearWithCount GetYearWithCountFromReader(SqlDataReader reader)
        {
            int year = reader.GetInt32("repairments_year");
            int count = reader.GetInt32("repairments_count");
            return new RepairmentYearWithCount(year, count);
        }
    }
}
