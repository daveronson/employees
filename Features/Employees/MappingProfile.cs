namespace EmployeeDirectory.Features.Employees
{
    using AutoMapper;
    using Domain;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, RepresentationModel>();
        }

    }
}