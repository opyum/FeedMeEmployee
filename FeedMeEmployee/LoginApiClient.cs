using FeedMeEmployee.Model;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace FeedMeEmployee.HttpCall
{
    public class ApiService

    {
        private readonly HttpClient httpClient;
        //private readonly ILogger<ApiService> logger;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            this.httpClient = new HttpClient(httpClientHandler);
            //this.logger = new Logger<ApiService>();
        }

        #region Login

        public async Task<List<Facture>> GetHistoFacture()
        {
            try
            {
                Uri uri = new Uri(string.Format("https://feedmeapi.azurewebsites.net/api/FacturesHistorique", string.Empty));
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                return await ProcessResponse<List<Facture>>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                throw;
            }
        }

       
        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (typeof(T) == typeof(string))
                    return (T)(object)responseContent;

                return JsonSerializer.Deserialize<T>(responseContent, options);
            }
            else
            {
                LogError(response);
                return default;
            }
        }

        private async Task<bool> ProcessResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return true;
            else
            {
                LogError(response);
                return false;
            }
        }
        #endregion




        #region Transverse
        private async void LogError(HttpResponseMessage response)
        {
            var test = response.Content.ReadAsStringAsync();
        }
     
        #endregion
    }
}
