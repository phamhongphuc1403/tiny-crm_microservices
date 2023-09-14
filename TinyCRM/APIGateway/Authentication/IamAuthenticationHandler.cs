using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ApiGateway.Authentication;

public class IamAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IamService _iamService;

    public IamAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IamService iamService) : base(options, logger, encoder, clock)
    {
        _iamService = iamService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Access token not provided!");
        }
        var accessToken = Context.Request.Headers["Authorization"].ToString();
        
        var claims = await _iamService.AuthenticateAsync(accessToken);
        if (claims == null)
        {
            return AuthenticateResult.Fail("Invalid access token!");
        }

        var authenticationClaims = claims.ToList();
        return AuthenticateResult.Success(GetTicket(authenticationClaims));
    }
    
    private AuthenticationTicket GetTicket(IEnumerable<AuthenticationClaim> claims)
    {
        var identity = new ClaimsIdentity("Iam");
        identity.AddClaims(claims.Select(x => new Claim(x.Type, x.Value)));
        
        return new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
    }
}