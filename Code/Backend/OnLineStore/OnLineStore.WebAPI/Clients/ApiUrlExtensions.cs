namespace OnlineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public static class ApiUrlExtensions
    {
        public static ApiUrl Controller(this ApiUrl apiUrl, string controller)
        {
            apiUrl.Controller = controller;

            return apiUrl;
        }

        public static ApiUrl Action(this ApiUrl apiUrl, string action)
        {
            apiUrl.Action = action;

            return apiUrl;
        }
    }
#pragma warning restore CS1591
}
