using IdentityModel;
using IdentityServer4.Models;

namespace MyIdentityServer;

public static class Configuration
{
    public static IEnumerable<Client> AddClients() => new List<Client>
    {
        new()
        {
            ClientId = "client_id",
            ClientSecrets = {new Secret("client_secret".ToSha256())},
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes =
            {
                "MyApi"
            }
        }
    };

    public static IEnumerable<ApiResource> AddApiResources() => new List<ApiResource>
    {
        new("MyApi")
        {
            Scopes = { "MyApi" }
        }
    };

    public static IEnumerable<ApiScope> AddApiScopes() => new List<ApiScope>
    {
        new("MyApi")
    };

    public static IEnumerable<IdentityResource> AddIdentityResource() => new List<IdentityResource>
    {
        new IdentityResources.OpenId()
    };
}