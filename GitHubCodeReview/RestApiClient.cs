using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubTask
{
    public class RestApiClient : HttpClient
    {
        public RestApiClient(Uri baseAdress)
        {
            BaseAddress = baseAdress;
        }

        public Task<T> Get<T>(Uri requestUri) =>
            Get<T>(new HttpRequestMessage(HttpMethod.Get, requestUri));

        public async Task<T> Get<T>(HttpRequestMessage requestMessage)
        {
            try
            {
                var response = await SendAsync(requestMessage);
                var stringResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(stringResponse);
            }
            catch (Exception e) //TODO: Narrow down the Exception
            {
                throw new HttpRequestException("Invalid request", e);
            }
        }
    }
}
