using System.ComponentModel.DataAnnotations;

namespace Sales.Application.DTOs.DealDTOs;

public class DealCreateDto
{
    public string Title { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public Guid? LeadId { get; set; }
    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "EstimatedRevenue must be a non-negative value.")]

    public double EstimatedRevenue { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "ActualRevenue must be a non-negative value.")]

    public double ActualRevenue { get; set; }
}