using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.CRM.Contract
{
    public interface ICrmHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string tableName, string id = null, string query = null);
        Task<HttpResponseMessage> PostAsync(string tableName, HttpContent content);
        Task<HttpResponseMessage> PatchAsync(string tableName, string id, HttpContent content);
        Task<HttpResponseMessage> DeleteAsync(string tableName, string id);
        void SetAuthorizationHeader(string token);
    }
}
