namespace Sales.Domain.LeadAggregate.Exceptions;

public class LeadMatchAccountException : Exception
{
    public LeadMatchAccountException()
    {
    }

    public LeadMatchAccountException(string? message) : base(message)
    {
    }

    public LeadMatchAccountException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}