using System.Net.Http;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public abstract class HttpDataServiceBase
    {
        private const string BASE_ADRESS = "https://localhost:7000/api/bus-station/";

        private static readonly HttpClient _client = new HttpClient();

        protected async Task<string> GetAsync(string urlExtension)
        {
            HttpResponseMessage response = await _client.GetAsync(BASE_ADRESS + urlExtension);
            return await response.Content.ReadAsStringAsync();
        }
        protected async Task PostAsync<T>(T postContent, string urlExtension) where T : HttpContent
        {
            HttpResponseMessage response = await _client.PostAsync(BASE_ADRESS + urlExtension, postContent);
        }

        protected async Task PutAsync<T>(T putContent, string urlExtension) where T : HttpContent
        {
            HttpResponseMessage response = await _client.PutAsync(BASE_ADRESS + urlExtension, putContent);
        }

        protected async Task DeleteAsync(string uriExtention)
        {
            HttpResponseMessage response = await _client.DeleteAsync(BASE_ADRESS + uriExtention);
        }
    }
}
