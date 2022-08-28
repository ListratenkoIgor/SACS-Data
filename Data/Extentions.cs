using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Interfaces.Models;
using Interfaces.DTOs;

namespace DataService.Data
{
    public static class Extentions
    {
        public static StudentDto ConvertToStudentDto(this PhysicalEntity student, IMapper _mapper)
        {
            return _mapper.Map<StudentDto>(student);
        }
        public static EmployeeDto ConvertToEmployeesDto(this PhysicalEntity employee, IMapper _mapper)
        {
            return _mapper.Map<EmployeeDto>(employee);
        }
        public static List<StudentDto> ConvertToStudentDtoList(this List<PhysicalEntity> students, IMapper _mapper)
        {
            List<StudentDto> studs = new List<StudentDto>();
            foreach (var s in students)
                studs.Add(_mapper.Map<StudentDto>(s));
            return studs;
        }
        public static List<EmployeeDto> ConvertToEmployeesDtoList(this List<PhysicalEntity> employees, IMapper _mapper)
        {
            List<EmployeeDto> emps = new List<EmployeeDto>();
            foreach (var s in employees)
                emps.Add(_mapper.Map<EmployeeDto>(s));
            return emps;
        }
    }
}
