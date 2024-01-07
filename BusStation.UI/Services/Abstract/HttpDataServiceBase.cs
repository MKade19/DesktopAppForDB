using BusStation.UI.Exceptions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BusStation.UI.Services.Abstract
{
    public abstract class HttpDataServiceBase
    {
        private const string BASE_ADRESS = "https://localhost:7000/api/bus-station/";

        private static HttpClient _client = new HttpClient();

        /// <summary>
        /// Makes get request to the web service.
        /// </summary>
        /// <param name="urlExtension">Extention to the url.</param>
        /// <returns>Serialized data from the web service.</returns>
        protected async Task<string> GetAsync(string urlExtension)
        {
            AttachHeaders();
            HttpResponseMessage response = await _client.GetAsync(BASE_ADRESS + urlExtension);
            VerifyResponseSuccess(response);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Makes post request to the web service.
        /// </summary>
        /// <param name="urlExtension">Extention to the url.</param>
        /// <returns>Serialized data from the web service.</returns>
        protected async Task<string> PostAsync<T>(T postContent, string urlExtension) where T : HttpContent
        {
            AttachHeaders();
            HttpResponseMessage response = await _client.PostAsync(BASE_ADRESS + urlExtension, postContent);
            VerifyResponseSuccess(response);
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Makes put request to the web service.
        /// </summary>
        /// <param name="urlExtension">Extention to the url.</param>
        protected async Task PutAsync<T>(T putContent, string urlExtension) where T : HttpContent
        {
            AttachHeaders();
            HttpResponseMessage response = await _client.PutAsync(BASE_ADRESS + urlExtension, putContent);
            VerifyResponseSuccess(response);
        }

        /// <summary>
        /// Makes delete request to the web service.
        /// </summary>
        /// <param name="urlExtension">Extention to the url.</param>
        protected async Task DeleteAsync(string uriExtention)
        {
            AttachHeaders();
            HttpResponseMessage response = await _client.DeleteAsync(BASE_ADRESS + uriExtention);
            VerifyResponseSuccess(response);
        }

        /// <summary>
        /// Attaches headers to the client.
        /// </summary>
        private void AttachHeaders()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.AccessToken);
        }

        /// <summary>
        /// Throws HttpException if <paramref name="response"/> has error status code 
        /// </summary>
        /// <param name="response">Response from web service</param>
        /// <exception cref="HttpException"></exception>
        private async void VerifyResponseSuccess(HttpResponseMessage response)
        {
            string responseMessage = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException(responseMessage);
            }
        }
    }
}
