namespace BuildingBlock.Domain.Exceptions;

public class ValueObjectValidationException : Exception
{
    public ValueObjectValidationException()
    {
    }

    public ValueObjectValidationException(string message) : base(message)
    {
    }

    public ValueObjectValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}