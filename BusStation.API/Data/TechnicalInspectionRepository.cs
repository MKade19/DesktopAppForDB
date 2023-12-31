﻿using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BusStation.API.Data
{
    public class TechnicalInspectionRepository : ITechnicalInspetionRepository
    {
        private readonly SqlConnection _connection;
        public TechnicalInspectionRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(TechnicalInspection technicalInspection)
        {
            string sqlExpression = "usp_create_technical_inspection";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("busId", technicalInspection.BusId));
                command.Parameters.Add(new SqlParameter("inspectionDate", technicalInspection.InspectionDate));
                command.Parameters.Add(new SqlParameter("isAllowed", technicalInspection.IsAllowed));
                command.Parameters.Add(new SqlParameter("denialReason", technicalInspection.DenialReason == null ? DBNull.Value : technicalInspection.DenialReason));

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
            string sqlExpression = "usp_delete_technical_inspection_by_id";

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

        public async Task<IEnumerable<TechnicalInspection>> GetAllAsync()
        {
            string sqlExpression = "usp_select_technical_inspections";
            List<TechnicalInspection> technicalInspections = new List<TechnicalInspection>();

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
                            technicalInspections.Add(GetTechnicalInspectionFromReader(reader));
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

            return technicalInspections;
        }

        public async Task<TechnicalInspection> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_technical_inspection_by_id";
            TechnicalInspection? technicalInspection = null;

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
                        technicalInspection = GetTechnicalInspectionFromReader(reader);
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

            return technicalInspection ?? new TechnicalInspection();
        }

        public async Task<IEnumerable<TechnicalInspection>> GetByYearAndAllowanceAsync(int year, bool isAllowed)
        {
            string sqlExpression = "usp_select_technical_inspections_by_year_and_allowance";
            List<TechnicalInspection> technicalInspections = new List<TechnicalInspection>();

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("year", year));
                command.Parameters.Add(new SqlParameter("isAllowed", isAllowed));

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            technicalInspections.Add(GetTechnicalInspectionFromReader(reader));
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

            return technicalInspections;
        }

        public async Task UpdateByIdAsync(TechnicalInspection technicalInspection)
        {
            string sqlExpression = "usp_update_technical_inspection_by_id";

            try
            {
                await _connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("id", technicalInspection.Id));
                command.Parameters.Add(new SqlParameter("busId", technicalInspection.BusId));
                command.Parameters.Add(new SqlParameter("inspectionDate", technicalInspection.InspectionDate));
                command.Parameters.Add(new SqlParameter("isAllowed", technicalInspection.IsAllowed));
                command.Parameters.Add(new SqlParameter("denialReason", technicalInspection.DenialReason == null ? DBNull.Value : technicalInspection.DenialReason));

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

        private TechnicalInspection GetTechnicalInspectionFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            DateTime inspectionDate = reader.GetDateTime("inspection_date");
            int busId = reader.GetInt32("bus_id");
            bool isAllowed = reader.GetBoolean("is_allowed");
            string? denialReason = null;
            string busNumber = reader.GetString("state_number");

            if (!reader.IsDBNull(4))
            {
                denialReason = reader.GetString("denial_reason");
            }

            return new TechnicalInspection(id, inspectionDate, busId, isAllowed, denialReason, busNumber);
        }
    }
}
