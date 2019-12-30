namespace OnlineStore.API.Common.IntegrationTests.Mocks
{
    public static class ClientSettingsMocker
    {
        // todo: Get identity server url from config file
        // todo: Get token request from config file

        public static OnlineStoreIdentityClientSettings GetOnlineStoreIdentityClientSettings(string userName, string password)
            => new OnlineStoreIdentityClientSettings
            {
                Url = "http://localhost:5100",
                ClientId = "OnlineStoreAPI.Client",
                ClientSecret = "OnlineStoreAPIClientSecret1",
                UserName = userName,
                Password = password
            };
    }
}
