using System.Text.Json.Serialization;
using BuildingBlock.Domain.Model;
using Sales.Domain.Entities.Enums;

namespace Sales.Application.DTOs;

public class LeadDto : GuidEntity
{
    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadSources? Source { get; set; }

    public double? EstimatedRevenue { get; set; }
    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadStatuses? Status { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadDisqualificationReasons? DisqualificationReason { get; set; }

    public string? DisqualificationDescription { get; set; }
}