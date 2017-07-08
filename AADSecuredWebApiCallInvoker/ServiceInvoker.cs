using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace AADSecuredWebApiCallInvoker
{
    public class ServiceInvoker
    {
        private static readonly string CallingServiceBaseAddress = ConfigurationManager.AppSettings["CallingServiceUrl"];
        public static HttpClient HttpClient = null;
        private static HttpRequestMessage _request = null;
        public static AuthenticationContext AuthContext = null;
        public static ClientCredential ClientCredential = null;

        private void SetUpContentForPost(string url, object postParameter)
        {
            var contentTypeValue = "application/json";
            _request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(postParameter));

            _request.Content = httpContent;
            _request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentTypeValue);
        }

        public HttpResponseMessage GetResponse(string urlParameter)
        {
            HttpResponseMessage response =
                HttpClient.GetAsync(CallingServiceBaseAddress + "?'" + urlParameter + "'").Result;
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> PostRequest(string url, object postParameter)
        {
            HttpResponseMessage response = null;

            // replace with input parameter and it should be and object
            SetUpContentForPost(url, postParameter);

            // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
            response = await HttpClient.SendAsync(_request);

            return response;
        }
    }
}
