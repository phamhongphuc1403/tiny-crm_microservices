namespace BuildingBlock.Domain.Exceptions;

public class ExceptionResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}