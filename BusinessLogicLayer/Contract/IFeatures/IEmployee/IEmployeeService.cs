using BusinessLogicLayer.Contract.IFeatures.ICommon;
using BusinessLogicLayer.Dtos.EmployeeDtos;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm;


namespace BusinessLogicLayer.Contract.IFeatures.IEmployee
{
    public interface IEmployeeService : IBaseService<cr5c1_Employee, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>
    {
    }
}
