using System.ComponentModel.DataAnnotations;

namespace BuildingBlock.Application.DTOs;

public class FilterDto
{
    [StringLength(100, ErrorMessage = "Keyword cannot exceed 100 characters.")]
    public string Keyword { get; set; } = string.Empty;
}