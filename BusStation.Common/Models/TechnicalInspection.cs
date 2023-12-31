﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusStation.Common.Models
{
    public class TechnicalInspection
    {
        [JsonPropertyName("id")]
        [Required]
        public int Id { get; set; } = -1;

        [JsonPropertyName("inspectionDate")]
        [Required]
        public DateTime InspectionDate { get; set; } = DateTime.MinValue;

        [JsonPropertyName("busId")]
        [Required]
        public int BusId { get; set; } = -1;

        [JsonPropertyName("busNumber")]
        public string? BusNumber { get; set; }

        [JsonPropertyName("isAllowed")]
        [Required]
        public bool IsAllowed { get; set; } = false;

        [JsonPropertyName("denialReason")]
        public string? DenialReason { get; set; }

        [JsonConstructor]
        public TechnicalInspection(int id, DateTime inspectionDate, int busId, bool isAllowed, string? denialReason, string? busNumber)
        {
            Id = id;
            InspectionDate = inspectionDate;
            BusId = busId;
            IsAllowed = isAllowed;
            DenialReason = denialReason;
            BusNumber = busNumber;
        }

        public TechnicalInspection() { }
    }
}
