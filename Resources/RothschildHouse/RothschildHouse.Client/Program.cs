using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace RothschildHouse.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

            Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            /* Discover endpoints from metadata */

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:18000");

            if (disco.IsError)
                Console.WriteLine(disco.Error);
            else
                Console.WriteLine("Discovery document it was successfully.");

            /* Create request */

            var tokenRequest = new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "onlinestoreclient",
                ClientSecret = "onlinestoreclientsecret1",
                UserName = "administrator@onlinestore.com",
                Password = "onlinestore1"
            };

            var tokenResponse = await client.RequestPasswordTokenAsync(tokenRequest);

            if (tokenResponse.IsError)
                Console.WriteLine(tokenResponse.Error);
            else
                Console.WriteLine("Connection for '{0}' user was successfully.", tokenRequest.UserName);
        }
    }
}
