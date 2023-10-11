using System.Text.Json.Serialization;
using Sales.Domain.DealAggregate.Enums;

namespace Sales.Application.DTOs.DealDTOs;

public class DealDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Guid? LeadId { get; set; }
    public string? Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DealStatus DealStatus { get; set; }

    public double EstimatedRevenue { get; set; }
    public double ActualRevenue { get; set; }
}