namespace BuildingBlock.Domain.Exceptions;

public class AuthorizationPolicyException : Exception
{
    public AuthorizationPolicyException()
    {
    }

    public AuthorizationPolicyException(string message) : base(message)
    {
    }

    public AuthorizationPolicyException(string message, Exception inner) : base(message, inner)
    {
    }
}