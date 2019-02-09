using System.Collections.Generic;

namespace OnlineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public class ApiUrl
    {
        public ApiUrl()
        {
        }

        public ApiUrl(string baseUrl = "http://localhost", string apiSufix = "api", string apiVersion = "v1")
        {
            BaseUrl = baseUrl;
            ApiSufix = apiSufix;
            ApiVersion = apiVersion;
        }

        public string BaseUrl { get; set; }

        public string ApiSufix { get; set; }

        public string ApiVersion { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string ID { get; set; }

        public override string ToString()
        {
            var output = new List<string>
            {
                
                BaseUrl
            };

            if (!string.IsNullOrEmpty(ApiSufix))
                output.Add(ApiSufix);

            if (!string.IsNullOrEmpty(ApiVersion))
                output.Add(ApiVersion);

            output.Add(Controller);

            if (!string.IsNullOrEmpty(Action))
                output.Add(Action);

            if (!string.IsNullOrEmpty(ID))
                output.Add(ID);

            return string.Join("/", output);
        }
    }
#pragma warning restore CS1591
}
