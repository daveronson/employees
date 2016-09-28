namespace EmployeeDirectory.Features.Employee
{
    using AutoMapper;
    using Domain;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, Details.Model>();
            CreateMap<EmployeeRole, Details.Model.EmployeeRoleModel>();
            CreateMap<Employee, Edit.Command>().ReverseMap();
            CreateMap<Employee, Index.Model>();

        }
        
    }
}