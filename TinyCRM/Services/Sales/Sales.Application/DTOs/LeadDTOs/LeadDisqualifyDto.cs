using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BuildingBlock.Application.PipelineBehaviors;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Application.DTOs.LeadDTOs;

public class LeadDisqualifyDto
{
    [JsonConverter(typeof(NullableEnumConverter<LeadDisqualificationReason>))]
    [EnumDataType(typeof(LeadDisqualificationReason))]
    public LeadDisqualificationReason? DisqualificationReason { get; set; }

    public string? DescriptionDisqualification { get; set; }
}