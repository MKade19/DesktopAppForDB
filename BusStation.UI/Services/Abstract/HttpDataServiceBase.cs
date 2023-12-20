using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public abstract class HttpDataServiceBase
    {
        private const string BASE_ADRESS = "https://localhost:7000/api/bus-station/";

        private static HttpClient _client = new HttpClient();

        protected async Task<string> GetAsync(string urlExtension)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);
            HttpResponseMessage response = await _client.GetAsync(BASE_ADRESS + urlExtension);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> PostAsync<T>(T postContent, string urlExtension) where T : HttpContent
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);
            HttpResponseMessage response = await _client.PostAsync(BASE_ADRESS + urlExtension, postContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task PutAsync<T>(T putContent, string urlExtension) where T : HttpContent
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);
            HttpResponseMessage response = await _client.PutAsync(BASE_ADRESS + urlExtension, putContent);
            response.EnsureSuccessStatusCode();
        }

        protected async Task DeleteAsync(string uriExtention)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);
            HttpResponseMessage response = await _client.DeleteAsync(BASE_ADRESS + uriExtention);
            response.EnsureSuccessStatusCode();
        }
    }
}
