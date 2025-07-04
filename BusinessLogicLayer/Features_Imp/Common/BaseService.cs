using AutoMapper;
using BusinessLogicLayer.Contract.IFeatures.ICommon;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Entities.Common;
using DataAccessLayer.Repositories.Contract.ICommon;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Features_Imp.Common
{
    public class BaseService<TEntity, TGetDto, TCreateDto, TUpdateDto> : IBaseService<TEntity, TGetDto, TCreateDto, TUpdateDto>
        where TEntity : Entity
        where TGetDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<TEntity>();
            _mapper = mapper;
        }

        public virtual async Task<TGetDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity).Name +  " with this id");

            return _mapper.Map<TGetDto>(entity);
        }

        public virtual async Task<IEnumerable<TGetDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();

            //var entities = await _repository.GetAllAsync(typeof(TEntity).Name);

            return _mapper.Map<IEnumerable<TGetDto>>(entities);
        }

        public virtual async Task<TEntity> CreateAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);

            try
            {
                _repository.Add(entity);


                await _unitOfWork.SaveAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new SaveChangesFailedException();
            }
        }

        public virtual async Task<bool> CreateRangeAsync(IEnumerable<TCreateDto> createDtos)
        {
            try
            {
                var entities = _mapper.Map<List<TEntity>>(createDtos);

                foreach (var entity in entities)
                {
                    _repository.Add(entity); // CRM context queues these
                }

                await _unitOfWork.SaveAsync();

                return entities.Any();
            }
            catch (Exception)
            {
                throw new SaveChangesFailedException();
            }
        }

        public virtual async Task UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity).Name + " with this id");

            try
            {
                _mapper.Map(updateDto, entity); // Apply new values
                _repository.Update(entity);     // Queue update
                await _unitOfWork.SaveAsync();  // Commit to CRM
            }
            catch (Exception)
            {
                throw new SaveChangesFailedException();
            }
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new EntityNotFoundException(typeof(TEntity).Name + " with this id");

            try
            {
                _repository.Delete(entity);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                throw new SaveChangesFailedException();
            }
        }
    }
}
