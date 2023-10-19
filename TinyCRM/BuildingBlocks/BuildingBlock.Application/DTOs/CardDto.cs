namespace BuildingBlock.Application.DTOs;

public class CardDto
{
    public string Name { get; set; } = null!;
    public double Value { get; set; }
    public bool IsPrice { get; set; }
}