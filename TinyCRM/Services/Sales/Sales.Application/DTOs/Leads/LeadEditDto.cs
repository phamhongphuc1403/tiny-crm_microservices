﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.Leads;

public class LeadEditDto
{
    [Required] public string Title { get; set; } = null!;
    [Required] public Guid CustomerId { get; set; }
    public string? Description { get; set; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadStatus Status { get; set; }

    public LeadSource? Source { get; set; }
    public double? EstimatedRevenue { get; set; }
}