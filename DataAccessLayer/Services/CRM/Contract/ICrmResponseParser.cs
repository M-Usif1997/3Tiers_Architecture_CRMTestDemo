using DataAccessLayer.Utils;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.CRM.Contract
{
    public interface ICrmResponseParser<TEntity>
    {
        Task<TEntity> ParseSingleAsync(HttpResponseMessage response, bool isPost = false);
        Task<IEnumerable<TEntity>> ParseListAsync(HttpResponseMessage response);
        BaseSearchResult<IEnumerable<TEntity>> Paginate(IEnumerable<TEntity> items, int? pageSize, int? pageNumber);
    }
}
