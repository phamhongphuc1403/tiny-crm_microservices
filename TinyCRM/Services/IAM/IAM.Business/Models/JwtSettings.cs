namespace IAM.Business.Models;

public class JwtSettings
{
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public int ExpiryInMinutes { get; set; }
    public string SecretKey { get; set; }
}