using AutoMapper;
using System;
using WebApplication1.Dto;
using WebApplication1.Model;

namespace WebApplication1.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            //CreateMap<Employee, EmployeeDto>()
            //   .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.EmployeeSkills.Select(es => es.Skill)))
            //   .ReverseMap()
            //   .ForPath(dest => dest.EmployeeSkills, opt => opt.MapFrom(src => src.Skills.Select(s => new EmployeeSkill { SkillId = s.SkillId })));

            //CreateMap<Address, AddressDto>().ReverseMap();
            //CreateMap<Project, ProjectDto>().ReverseMap();
            //CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<Loggin, LogginDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
        }
    }
}
