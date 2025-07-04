using AutoMapper;
using BusinessLogicLayer.Dtos.EmployeeDtos;
using DataAccessLayer.Entities;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xrm;

namespace BusinessLogicLayer.MapperProfiles
{
    public class EmployeeProfile : Profile
    {

        public EmployeeProfile()
        {
            // Entity to GetDto
            CreateMap<cr5c1_Employee, GetEmployeeDto>()
             .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.cr5c1_EmployeeId))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.cr5c1_EmployeeName))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.cr5c1_Email))
             .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.cr5c1_Phone))
             .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.cr5c1_Position))
             .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.lm_Employee_Manager.Name));

            // CreateDto to Entity
            CreateMap<CreateEmployeeDto, cr5c1_Employee>()
            .ForMember(dest => dest.cr5c1_EmployeeName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.cr5c1_Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.cr5c1_Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.cr5c1_Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.lm_Employee_Manager, opt => opt.MapFrom(src =>
                new EntityReference("cr5c1_employee", src.ManagerID)));

            // UpdateDto to Entity
            CreateMap<UpdateEmployeeDto, cr5c1_Employee>()
            .ForMember(dest => dest.cr5c1_EmployeeName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.cr5c1_Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.cr5c1_Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.cr5c1_Position, opt => opt.MapFrom(src => src.Position))
            .ForMember(dest => dest.lm_Employee_Manager, opt => opt.MapFrom(src =>
             src.ManagerID != null ? new EntityReference("cr5c1_employee", src.ManagerID.Value) : null))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
