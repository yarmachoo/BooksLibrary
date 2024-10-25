using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace BooksLibraryIdentity
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScope =>
            new List<ApiScope>()
            {
                new ApiScope("BooksLibraryWebAPI", "Web API")
            };
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>    
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("BooksLibraryWebAPI", "Web API", new []
                { JwtClaimTypes.Name })
                {
                    Scopes = { "BooksLibraryWebAPI" }
                }
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "bookslibrary-web-api",
                    ClientName = "BooksLibrary WebAPI",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http:/.../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "BooksLibraryWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };

    }
}
