using System.Collections.Generic;

namespace OnLineStore.WebAPI.Clients
{
#pragma warning disable CS1591
    public class ApiUrl
    {
        public ApiUrl()
        {
        }

        public ApiUrl(string protocol = "http", string host = "localhost", int port = 0, string apiSufix = "api", string apiVersion = "v1")
        {
            Protocol = protocol;
            Host = host;
            Port = port;
            ApiSufix = apiSufix;
            ApiVersion = apiVersion;
        }

        public string Protocol { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string ApiSufix { get; set; }

        public string ApiVersion { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string ID { get; set; }

        public override string ToString()
        {
            var output = new List<string>
            {
                string.Format("{0}://{1}{2}", Protocol, Host, Port > 0 ? string.Format(":{0}", Port) : string.Empty)
            };

            if (!string.IsNullOrEmpty(ApiSufix))
                output.Add(string.Format(":{0}", Port));

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
