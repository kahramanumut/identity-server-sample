using System.Collections.Generic;
using Duende.IdentityServer.Models;

public class Resources
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new[]
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
    }

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new[]
        {
                new ApiResource
                {
                    Name = "employeeapi",
                    DisplayName = "Employee API",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string> {"employeeapi.read", "employeeapi.write"},
                    ApiSecrets = new List<Secret> {new Secret("RsCaseScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new[]
        {
                new ApiScope("employeeapi.read", "Read Access to Employee API"),
                new ApiScope("employeeapi.write", "Write Access to Employee API")
            };
    }
}