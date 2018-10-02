using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Store.API.IntegrationTests
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            return new StringContent(json, Encoding.Default, "application/json");
        }
    }
}
