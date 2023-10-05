using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.Leads;

public class LeadDisqualifyDto
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [EnumDataType(typeof(LeadDisqualificationReason))]
    public LeadDisqualificationReason DisqualificationReason { get; set; }

    public string? DescriptionDisqualification { get; set; }
}