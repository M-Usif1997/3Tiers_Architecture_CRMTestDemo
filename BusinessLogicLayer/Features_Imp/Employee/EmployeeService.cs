using AutoMapper;
using BusinessLogicLayer.Contract.IFeatures.ICommon;
using BusinessLogicLayer.Contract.IFeatures.IEmployee;
using BusinessLogicLayer.Dtos.EmployeeDtos;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Features_Imp.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Contract.ICommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm;

namespace BusinessLogicLayer.Features_Imp.Employee
{
    public class EmployeeService : BaseService<cr5c1_Employee, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>, IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


    }
}
