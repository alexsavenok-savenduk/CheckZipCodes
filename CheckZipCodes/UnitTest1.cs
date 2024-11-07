using RestSharp;

namespace CheckZipCodes;

public class Tests
{
    [Test]
    public void GetZipCodes_Test()
    {
        var options = new RestClientOptions("http://localhost:8080")
        {
            Authenticator = new APIAuthentificator()
        };
        var client = new RestClient(options);
        var request = new RestRequest("/zip-codes");
        var response = client.GetAsync(request).Result;
    }
}