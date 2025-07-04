using DataAccessLayer.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Contract.IFeatures.ICommon
{
    public interface IBaseService<TEntity, TGetDto, TCreateDto, TUpdateDto>
    {
        Task<TGetDto> GetByIdAsync(Guid id);

        Task<IEnumerable<TGetDto>> GetAllAsync();

        Task<TEntity> CreateAsync(TCreateDto createDto);

        Task<bool> CreateRangeAsync(IEnumerable<TCreateDto> createDtos);

        Task UpdateAsync(Guid id, TUpdateDto updateDto);

        Task DeleteAsync(Guid id);




    }
      
}
