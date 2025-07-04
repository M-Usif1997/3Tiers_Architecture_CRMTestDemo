using DataAccessLayer.Utils;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Xrm.Sdk;
using System.Linq;
using System.Linq.Expressions;


namespace DataAccessLayer.Repositories.Contract.ICommon
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        ///  ///  ///  ///  ///  ///  ///  ///  ///  ///  /// 


        Task<BaseSearchResult<IEnumerable<TEntity>>> Search(string tableName, int? pageSize, int? pageNumber, string query = null);
        Task<TEntity> GetByIdAsync(string tableName, string id, string query = null);
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string query = null);
        Task<TEntity> CreateAsync(string tableName, TEntity entity);
        Task<TEntity> UpdateAsync(string tableName, string id, TEntity entity);
        Task<TEntity> DeleteAsync(string tableName, string id);

    }
}
