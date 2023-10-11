using System.Text.Json.Serialization;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.LeadDTOs;

public class LeadSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string CustomerName { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LeadStatus Status { get; set; }
}