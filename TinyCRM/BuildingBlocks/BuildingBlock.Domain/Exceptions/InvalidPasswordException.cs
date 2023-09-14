namespace BuildingBlock.Domain.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string message) : base(message)
    {
    }

    public InvalidPasswordException(string message, Exception inner) : base(message, inner)
    {
    }

    public InvalidPasswordException()
    {
    }
}