namespace IAM.Business.Models;

public class AuthenticationClaim
{
    public string Type { get; set; }
    public string Value { get; set; }

    public AuthenticationClaim(string type, string value)
    {
        Type = type;
        Value = value;
    }
}