using AutoMapper;
using EmployeeManagementAPI.Dto;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeDto, Employee>();
    }
}
