using BusStation.API.Data.Abstract;
using BusStation.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace BusStation.API.Data
{
    public class MedicalInspectionRepository : IMedicalInspetionRepository
    {
        private readonly SqlConnection _connection;
        public MedicalInspectionRepository()
        {
            _connection = BusStationDatabase.Instance.Connection;
        }
        public async Task CreateOneAsync(MedicalInspection medicalInspection)
        {
            string sqlExpression = "usp_create_medical_inspection";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("inspectionDate", medicalInspection.InspectionDate));
            command.Parameters.Add(new SqlParameter("workerId", medicalInspection.WorkerId));
            command.Parameters.Add(new SqlParameter("isAllowed", medicalInspection.IsAllowed));
            command.Parameters.Add(new SqlParameter("denialReason", medicalInspection.DenialReason == null ? DBNull.Value : medicalInspection.DenialReason));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            string sqlExpression = "usp_delete_medical_inspection_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        public async Task<IEnumerable<MedicalInspection>> GetAllAsync()
        {
            string sqlExpression = "usp_select_medical_inspections";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            List<MedicalInspection> medicalInspections = new List<MedicalInspection>();

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        medicalInspections.Add(GetMedicalInspectionFromReader(reader));
                    }
                }
            }

            await _connection.CloseAsync();

            return medicalInspections;
        }

        public async Task<MedicalInspection> GetByIdAsync(int id)
        {
            string sqlExpression = "usp_select_medical_inspection_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", id));
            MedicalInspection medicalInspection = null;

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    medicalInspection = GetMedicalInspectionFromReader(reader);
                }
            }

            await _connection.CloseAsync();

            return medicalInspection ?? new MedicalInspection();
        }

        public async Task UpdateByIdAsync(MedicalInspection medicalInspection)
        {
            string sqlExpression = "usp_update_medical_inspection_by_id";
            await _connection.OpenAsync();

            SqlCommand command = new SqlCommand(sqlExpression, _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("id", medicalInspection.Id));
            command.Parameters.Add(new SqlParameter("inspectionDate", medicalInspection.InspectionDate));
            command.Parameters.Add(new SqlParameter("workerId", medicalInspection.WorkerId));
            command.Parameters.Add(new SqlParameter("isAllowed", medicalInspection.IsAllowed));
            command.Parameters.Add(new SqlParameter("denialReason", medicalInspection.DenialReason == null ? DBNull.Value : medicalInspection.DenialReason));

            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }

        private MedicalInspection GetMedicalInspectionFromReader(SqlDataReader reader)
        {
            int id = reader.GetInt32("id");
            DateTime inspectionDate = reader.GetDateTime("inspection_date");
            int workerId = reader.GetInt32("worker_id");
            bool isAllowed = reader.GetBoolean("is_allowed");
            string? denialReason = null;

            if (!reader.IsDBNull(4)) 
            {
                denialReason = reader.GetString("denial_reason");
            }
             
            return new MedicalInspection(id, inspectionDate, workerId, isAllowed, denialReason);
        }
    }
}
