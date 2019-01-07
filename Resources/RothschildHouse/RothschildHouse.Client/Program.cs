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
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:18000");

            if (disco.IsError)
                Console.WriteLine(disco.Error);
            else
                Console.WriteLine("Success!!!");
            
            var userResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "rothschildhousecustomerclient",
                ClientSecret = "rothschildhousesecret1",
                UserName = "charlesx@gmail.com",
                Password = "password1"
            });

            if (userResponse.IsError)
                Console.WriteLine(userResponse.Error);
            else
                Console.WriteLine("Success user!!!");
        }
    }
}
