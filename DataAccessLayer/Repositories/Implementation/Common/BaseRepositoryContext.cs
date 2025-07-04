
using DataAccessLayer.Entities.Wrapper;
using DataAccessLayer.Repositories.Contract.ICommon;
using DataAccessLayer.Services.CRM.Contract;
using DataAccessLayer.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation.Common
{
    public class BaseRepositoryContext<T> : IBaseRepository<T>
         where T : Entity
    {
        protected readonly OrganizationServiceContext _context;


        public BaseRepositoryContext(ICrmServiceContextProvider provider)
        {
            _context = provider.GetContext();

        }


        public async Task<T?> GetByIdAsync(Guid id)
        {
          return await Task.FromResult(_context.CreateQuery<T>().FirstOrDefault(e => e.Id == id));
         
        }


        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.AddObject(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                _context.AddObject(entity);
            }
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.UpdateObject(entity);
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.DeleteObject(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(_context.CreateQuery<T>().ToList());
        }


        public async Task<IEnumerable<T>> GetAllAsync(string tableName, string query = null)
        {
            throw new NotImplementedException();
         
        }

        public Task<BaseSearchResult<IEnumerable<T>>> Search(string tableName, int? pageSize, int? pageNumber, string query = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string tableName, string id, string query = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateAsync(string tableName, T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(string tableName, string id, T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(string tableName, string id)
        {
            throw new NotImplementedException();
        }
    }


}
