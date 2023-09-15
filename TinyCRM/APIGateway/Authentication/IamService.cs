namespace APIGateway.Authentication;

public class IamService
{
    private readonly HttpClient _httpClient;

    public IamService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(configuration["Iam:BaseUrl"]!);
    }

    public async Task<IEnumerable<AuthenticationClaim>?> AuthenticateAsync(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);
        var response = await _httpClient.GetAsync("api/auth");
        if (response.IsSuccessStatusCode)
            return (await response.Content.ReadFromJsonAsync<IEnumerable<AuthenticationClaim>>())!;

        return null;
    }
}