using DataAccessLayer.Repositories.Contract.ICommon;
using DataAccessLayer.Services.CRM.Contract;
using DataAccessLayer.Services.TokenProvider.Contract;
using DataAccessLayer.Utils;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation.Common
{
    public class BaseRepositoryClient<TEntity> : IBaseRepository<TEntity>
    {
        private readonly ICrmHttpClient _crmHttpClient;
        private readonly ITokenProvider _tokenProvider;
        private readonly ICrmResponseParser<TEntity> _responseParser;

        public BaseRepositoryClient(ICrmHttpClient crmHttpClient, ITokenProvider tokenProvider, ICrmResponseParser<TEntity> responseParser)
        {
            _crmHttpClient = crmHttpClient;
            _tokenProvider = tokenProvider;
            _responseParser = responseParser;

            var token = _tokenProvider.GetBearerToken();
            _crmHttpClient.SetAuthorizationHeader(token);
        }

        public async Task<BaseSearchResult<IEnumerable<TEntity>>> Search(string tableName, int? pageSize, int? pageNumber, string query = null)
        {
            var response = await _crmHttpClient.GetAsync(tableName, null, query);
            response.EnsureSuccessStatusCode();

            var items = await _responseParser.ParseListAsync(response);
            return _responseParser.Paginate(items, pageSize, pageNumber);
        }

        public async Task<TEntity> GetByIdAsync(string tableName, string id, string query = null)
        {
            var response = await _crmHttpClient.GetAsync(tableName, id, query);
            response.EnsureSuccessStatusCode();
            return await _responseParser.ParseSingleAsync(response);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string query = null)
        {
            var response = await _crmHttpClient.GetAsync(tableName, null, query);
            response.EnsureSuccessStatusCode();
            return await _responseParser.ParseListAsync(response);
        }

        public async Task<TEntity> CreateAsync(string tableName, TEntity entity)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _crmHttpClient.PostAsync(tableName, content);
            response.EnsureSuccessStatusCode();
            return await _responseParser.ParseSingleAsync(response, true);
        }

        public async Task<TEntity> UpdateAsync(string tableName, string id, TEntity entity)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _crmHttpClient.PatchAsync(tableName, id, content);
            response.EnsureSuccessStatusCode();
            return await _responseParser.ParseSingleAsync(response, true);
        }

        public async Task<TEntity> DeleteAsync(string tableName, string id)
        {
            var response = await _crmHttpClient.DeleteAsync(tableName, id);
            response.EnsureSuccessStatusCode();
            return await _responseParser.ParseSingleAsync(response, true);
        }

        public Task<TEntity?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
