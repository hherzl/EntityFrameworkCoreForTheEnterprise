using OnlineStore.WebAPI.Clients;

namespace OnlineStore.WebAPI.IntegrationTests.Mocks
{
    public static class ClientSettingsMocker
    {
        // todo: Get identity server url from config file
        // todo: Get token request from config file

        public static OnlineStoreIdentityClientSettings GetOnlineStoreIdentityClientSettings(string userName, string password)
            => new OnlineStoreIdentityClientSettings
            {
                Url = "http://localhost:56000",
                ClientId = "onlinestoreclient",
                ClientSecret = "onlinestoreclientsecret1",
                UserName = userName,
                Password = password
            };
    }
}
