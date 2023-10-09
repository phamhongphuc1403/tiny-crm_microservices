namespace BuildingBlock.Domain.Exceptions;

public class SeedDataException : Exception
{
    public SeedDataException(string message) : base(message)
    {
    }
}