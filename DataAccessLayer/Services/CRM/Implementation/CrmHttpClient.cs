using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Services.CRM.Contract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.CRM.Implementation
{
    public class CrmHttpClient : ICrmHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly CRMConfiguration _crmConfig;

        public CrmHttpClient(HttpClient httpClient, IOptions<CRMConfiguration> crmConfig)
        {
            _crmConfig = crmConfig.Value;

            if (_crmConfig.IsOnlineEnv)
            {
                _httpClient = httpClient;
            }
            else
            {
                _httpClient = new HttpClient(new HttpClientHandler
                {
                    Credentials = new NetworkCredential(_crmConfig.Username, _crmConfig.Password),
                })
                {
                    BaseAddress = new Uri(_crmConfig.Url)
                };
                _httpClient.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations=OData.Community.Display.V1.FormattedValue");
            }
        }

        public void SetAuthorizationHeader(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private string BuildUrl(string tableName, string id = null, string query = null)
        {
            var url = id == null ? $"{_crmConfig.Url}/{tableName}" : $"{_crmConfig.Url}/{tableName}({id})";
            return string.IsNullOrWhiteSpace(query) ? url : $"{url}?{query}";
        }

        public Task<HttpResponseMessage> GetAsync(string tableName, string id = null, string query = null) =>
            _httpClient.GetAsync(BuildUrl(tableName, id, query));

        public Task<HttpResponseMessage> PostAsync(string tableName, HttpContent content) =>
            _httpClient.PostAsync(BuildUrl(tableName), content);

        public Task<HttpResponseMessage> PatchAsync(string tableName, string id, HttpContent content) =>
            _httpClient.PatchAsync(BuildUrl(tableName, id), content);

        public Task<HttpResponseMessage> DeleteAsync(string tableName, string id) =>
            _httpClient.DeleteAsync(BuildUrl(tableName, id));
    }
}

