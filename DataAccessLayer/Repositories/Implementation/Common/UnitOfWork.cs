using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Common;
using DataAccessLayer.Repositories.Contract.ICommon;
using DataAccessLayer.Services.CRM.Contract;
using DataAccessLayer.Services.CRM.Implementation;
using DataAccessLayer.Services.TokenProvider.Contract;
using DataAccessLayer.Services.TokenProvider.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly HttpClient _httpClient;
        //private readonly IOptions<CRMConfiguration> _crmConfig;
        //private readonly ITokenProvider _tokenProvider;

        private readonly ICrmServiceContextProvider _provider;

        public ICrmServiceContextProvider CrmServiceContextProvider { get; }

        public UnitOfWork(ICrmServiceContextProvider provider)
        {

            _provider = provider;
        }

        // Explicit interface implementation to resolve CS0425
        IBaseRepository<T> IUnitOfWork.GetRepository<T>()
        {
            //var crmHttpClient = new CrmHttpClient(_httpClient, _crmConfig);
            //var parser = new CrmResponseParser<T>();
            return new BaseRepositoryContext<T>(_provider);
        }

        //public Task SaveAsync() => Task.CompletedTask;

        public async Task SaveAsync()
        {
            var context = _provider.GetContext();
            context.SaveChanges(SaveChangesOptions.None);
            await Task.CompletedTask;
        }
    }
}

