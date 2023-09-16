using System.Text.Json.Serialization;
using Sales.Domain.Entities.Enums;

namespace Sales.Application.DTOs;

public class LeadDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadSource? Source { get; set; }

    public double? EstimatedRevenue { get; set; }
    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadStatus? Status { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadDisqualificationReason? DisqualificationReason { get; set; }

    public string? DisqualificationDescription { get; set; }
}