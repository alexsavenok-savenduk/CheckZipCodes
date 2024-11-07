using RestSharp;
using RestSharp.Authenticators;

namespace CheckZipCodes;

public class APIAuthentificator : AuthenticatorBase
{
    protected string _baseUrl = "http://localhost:8080/";
    protected string _clientId = "0oa157tvtugfFXEhU4x7";
    protected string _clientSecret = "X7eBCXqlFC7x-mjxG5H91IRv_Bqe1oq7ZwXNA8aq";
    protected RestClientOptions Options { get; }
    private static RestClient? _client;
    
    public APIAuthentificator() : base("")
    {
        Options = new RestClientOptions(_baseUrl)
        {
            Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret)
        };

        _client = new RestClient(Options);
    }

    protected override async ValueTask<Parameter>  GetAuthenticationParameter(string accessToken)
    {
        Token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
        return new HeaderParameter(KnownHeaders.Authorization, Token);
    }
    
    private async Task<string> GetToken()
    {
        /*var options = new RestClientOptions(_baseUrl)
        {
            Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret),
        };*/
        //using var client = new RestClient(Options);

        var request = new RestRequest("oauth/token")
            .AddParameter("grant_type", "client_credentials")
            .AddParameter("scope", "read");
        var response = await _client.PostAsync<AuthentificationResponse>(request);
        
        return $"{response.TokenType} {response.AccessToken}";
    }
}