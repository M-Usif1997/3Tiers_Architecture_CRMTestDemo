using BusinessLogicLayer.Contract.IFeatures.IEmployee;
using BusinessLogicLayer.Dtos.EmployeeDtos;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;
using Xrm;

namespace WebApi.Controllers.Employee
{

    public class EmployeeController : BaseController<cr5c1_Employee, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>
    {

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTest()
        {
            var entities = await _employeeService.GetAllEmployeesTest();
            return Ok(entities);
        }
    }
}
