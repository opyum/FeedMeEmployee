﻿using FeedMeEmployee.Model;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Transactions;

namespace TodoREST.Services
{
    public class RestService : IRestService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        IHttpsClientHandlerService _httpsClientHandlerService;

        public List<Facture> Items { get; private set; }

        public RestService(IHttpsClientHandlerService service)
        {
#if DEBUG
            _httpsClientHandlerService = service;
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            if (handler != null)
                _client = new HttpClient(handler);
            else
                _client = new HttpClient();
#else
            _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<Facture>> GetHistoFacture()
        {
            try
            {
                //Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
                Uri uri = new Uri(string.Format("https://feedmeapi.azurewebsites.net/api/FacturesHistorique", string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);
                return await ProcessResponse<List<Facture>>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                throw;
            }
        }

        public async Task<bool> CreateTransaction()
        {
            Uri uri = new Uri(string.Format("https://feedmeapi.azurewebsites.net/api/Transaction", string.Empty));
            StringContent content = new StringContent(null, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync(uri, content);
            return await ProcessResponse(response);
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                if (typeof(T) == typeof(string))
                    return (T)(object)responseContent;

                return JsonSerializer.Deserialize<T>(responseContent, _serializerOptions);
            }
            else
            {
                return default;
            }
        }

        public async Task<List<TransactionRecord>> GetAllTransactions()
        {
            try
            {
                Uri uri = new Uri(string.Format("https://feedmeapi.azurewebsites.net/api/Transaction/1/false", string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);
                return await ProcessResponse<List<TransactionRecord>>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                throw;
            }
        }

        private async Task<bool> ProcessResponse(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode;
        }

        //public async Task<List<TodoItem>> RefreshDataAsync()
        //{
        //    Items = new List<TodoItem>();

        //    Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
        //    try
        //    {
        //        HttpResponseMessage response = await _client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string content = await response.Content.ReadAsStringAsync();
        //            Items = JsonSerializer.Deserialize<List<TodoItem>>(content, _serializerOptions);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }

        //    return Items;
        //}

        //public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        //{
        //    Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

        //    try
        //    {
        //        string json = JsonSerializer.Serialize<TodoItem>(item, _serializerOptions);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = null;
        //        if (isNewItem)
        //            response = await _client.PostAsync(uri, content);
        //        else
        //            response = await _client.PutAsync(uri, content);

        //        if (response.IsSuccessStatusCode)
        //            Debug.WriteLine(@"\tTodoItem successfully saved.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }
        //}

        //public async Task DeleteTodoItemAsync(string id)
        //{
        //    Uri uri = new Uri(string.Format(Constants.RestUrl, id));

        //    try
        //    {
        //        HttpResponseMessage response = await _client.DeleteAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //            Debug.WriteLine(@"\tTodoItem successfully deleted.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(@"\tERROR {0}", ex.Message);
        //    }
        //}
    }
}
