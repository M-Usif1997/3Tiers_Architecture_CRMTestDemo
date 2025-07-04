using DataAccessLayer.Entities.Wrapper;
using DataAccessLayer.Services.CRM.Contract;
using DataAccessLayer.Utils;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.CRM.Implementation
{
    public class CrmResponseParser<TEntity> : ICrmResponseParser<TEntity>
    {
        public async Task<TEntity> ParseSingleAsync(HttpResponseMessage response, bool isPost = false)
        {
            var text = await response.Content.ReadAsStringAsync();

            if (isPost && response.StatusCode == HttpStatusCode.NoContent)
            {
                var data = new ApiPostResponse { Status = true, LocationPath = response?.Headers?.Location, EntityId = default };
                if (response.Headers.TryGetValues("OData-EntityId", out var entityId))
                {
                    data.EntityId = entityId.FirstOrDefault().GetGuidId();
                }
                text = JsonConvert.SerializeObject(data);
            }

            return JsonConvert.DeserializeObject<TEntity>(text);
        }

        public async Task<IEnumerable<TEntity>> ParseListAsync(HttpResponseMessage response)
        {
            var text = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<CrmResponseWrapper<IEnumerable<TEntity>>>(text);
            return data?.Value ?? Enumerable.Empty<TEntity>();
        }

        public BaseSearchResult<IEnumerable<TEntity>> Paginate(IEnumerable<TEntity> items, int? pageSize, int? pageNumber)
        {
            var totalCount = items.Count();
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                items = items.Skip((pageNumber.Value - 1) * pageSize.Value);
            }
            if (pageSize.HasValue)
            {
                items = items.Take(pageSize.Value);
            }

            return new BaseSearchResult<IEnumerable<TEntity>>
            {
                TotalCount = totalCount,
                Results = items
            };
        }
    }
}
