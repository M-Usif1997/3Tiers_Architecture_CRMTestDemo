using BusinessLogicLayer.Contract.IFeatures.ICommon;
using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Entities.Common;
using Microsoft.Xrm.Sdk;

namespace WebApi.Controllers.Common
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public abstract class BaseController<TEntity, TGetDto, TCreateDto, TUpdateDto> : ControllerBase
  where TEntity : Entity
  where TGetDto : class
  where TCreateDto : class
  where TUpdateDto : class

    {
        protected readonly IBaseService<TEntity, TGetDto, TCreateDto, TUpdateDto> _service;

        protected BaseController(IBaseService<TEntity, TGetDto, TCreateDto, TUpdateDto> service)
        {
            _service = service;
        }


        /// <response code="200">Data Get Successfully</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var entity = await _service.GetByIdAsync(id);
            return Ok(entity);



        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }




        /// <response code="201">Data created successfully with Guid response of Created Data</response>
        /// <response code="400">if Error related to Save Changes => may the Ids was not Founds in DB that are Posted 'Check First' </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public virtual async Task<IActionResult> Create([FromBody] TCreateDto createDto)
        {
            try
            {
                var Entity = await _service.CreateAsync(createDto);

                return Created("Create", Entity.Id);
            }

            catch (SaveChangesFailedException scfex)
            {

                return BadRequest(scfex.Message);

            }

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CreateRange([FromBody] IEnumerable<TCreateDto> createDtos)
        {
            try
            {
                var statusOfCreate = await _service.CreateRangeAsync(createDtos);

                return Created("CreateRange", statusOfCreate);
            }

            catch (SaveChangesFailedException scfex)
            {

                return BadRequest(scfex.Message);

            }


        }


        /// <response code="200">Data Updated successfully with Guid response of Updated Data</response>

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Update(Guid id, [FromBody] TUpdateDto updateDto)
        {
            try
            {
                await _service.UpdateAsync(id, updateDto);
                return Ok(id);
            }
            catch (SaveChangesFailedException scfex)
            {
                return BadRequest(scfex.Message);

            }

        }


        /// <response code="202">Accept to Delete the Data</response>
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            await _service.DeleteAsync(id);
            return Accepted();

        }

    }
}
