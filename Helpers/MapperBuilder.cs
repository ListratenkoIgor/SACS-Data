using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Data;
using AutoMapper;
using Interfaces.Models;
using Interfaces.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataService.Helpers
{
    public class MapperBuilder
    {
        private readonly ApplicationDbContext _context;
        public MapperBuilder(ApplicationDbContext context)
        {
            _context = context;
        }
        public MapperBuilder(){}
        public Mapper Build()
        {
            var expression = new MapperConfigurationExpression();
            ConfigureMapper(expression);
            var config = new MapperConfiguration(expression);
            return new Mapper(config);
        }
        private void ConfigureMapper(MapperConfigurationExpression expression)
        {
            expression.CreateMap<EducationForm, EducationFormDto>().ReverseMap();
            expression.CreateMap<PhysicalEntity, EmployeeDto>();
            expression.CreateMap<Faculty, FacultyDto>().ReverseMap();
            expression.CreateMap<Speciality, SpecialityDto>()
                .ForMember(dest => dest.EducationFormId, opt => opt.MapFrom(src => src.EducationForm.Id))
                .ForMember(dest => dest.FacultyId, opt => opt.MapFrom(src => src.Faculty.Id));
            expression.CreateMap<PhysicalEntity, StudentDto>()
                //.ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Group.Id));
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => _context.StudentsGroups.Where(x => x.Number == src.Group).FirstOrDefault().Number));
            expression.CreateMap<StudentsGroup, StudentsGroupDto>()
                .ForMember(dest => dest.SpecialityId, opt => opt.MapFrom(src => src.Speciality.Id));


            expression.CreateMap<EmployeeDto, PhysicalEntity>()
                .IncludeAllDerived()
                .ForMember(dest => dest.isStudent, opt => opt.MapFrom(src => false));
            expression.CreateMap<SpecialityDto, Speciality>()
                .IncludeAllDerived()
                .ForMember(dest => dest.EducationForm, opt => opt.MapFrom(src => _context.EducationForms.Where(x => x.Id == src.EducationFormId).FirstOrDefault()))
                .ForMember(dest => dest.Faculty, opt => opt.MapFrom(src => _context.Faculties.Where(x => x.Id == src.FacultyId).FirstOrDefault()));
            expression.CreateMap<StudentDto, PhysicalEntity>()
                .IncludeAllDerived()
                .ForMember(dest => dest.isStudent, opt => opt.MapFrom(src=> true))
                .ForMember(dest=>dest.Group, opt => opt.MapFrom(src => _context.StudentsGroups.Where(x => x.Number == src.Group).FirstOrDefault().Number));
            expression.CreateMap<StudentsGroupDto, StudentsGroup>()
                .IncludeAllDerived()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => _context.Specialities.Where(x => x.Id == src.SpecialityId).FirstOrDefault()));
        }
    }
}


