using System.Collections.Generic;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

public class Clients
{
    public static IEnumerable<Client> Get()
    {
        return new List<Client>
            {
                new Client
                {
                    ClientId = "employeeClient",
                    ClientName = "Employee client application using client credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = new List<Secret> {new Secret("RsCaseSecret".Sha256())}, // change me!
                    AllowedScopes = new List<string> {"employeeapi.read" , "employeeapi.write"}
                }
            };
    }
}