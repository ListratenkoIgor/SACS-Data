using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Models;
using Interfaces.DTOs;
using DataService.Data;
using AutoMapper;

namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : MyControllerBase
    {
        public TestController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        [HttpGet("StudentTest")]
        public List<StudentsGroup> GetFaculty1ById() {
            var collection = new List<StudentsGroup>();
            var group = _unitOfWork.StudentsGroups.GetGroups().FirstOrDefault();
            var speciality  = group.Speciality;
            var faculty = speciality.Faculty;
            var form = group.Speciality.EducationForm;
            var student = new PhysicalEntity
            {
                Id = 1,
                FirstName = "11",
                LastName = "22",
                MiddleName = "33",
                RecordBookNumber = "80",
                Group = group.Number
            };

            var faculty2 = _mapper.Map<FacultyDto>(faculty);
            faculty = null;
            faculty = _mapper.Map<Faculty>(faculty2);

            var speciality2 = _mapper.Map<SpecialityDto>(speciality);
            speciality = null;
            speciality = _mapper.Map<Speciality>(speciality2);

            var group2 = _mapper.Map<StudentsGroupDto>(group);
            group = null;
            group = _mapper.Map<StudentsGroup>(group2);
            
            var student2 = _mapper.Map<StudentDto>(student);
            student = null;
            student = _mapper.Map<PhysicalEntity>(student2);
            collection.Add(group);
            return collection;
        }
        [HttpGet]
        public Faculty GetAddStudent()
        {
            var collection = new List<StudentsGroup>();
            /*
            var group = _unitOfWork.StudentsGroups.GetGroups().FirstOrDefault();
            var speciality = group.Speciality;
            var faculty = speciality.Faculty;
            var form = group.Speciality.EducationForm;
            var student = new Student
            {
                Id = 1,
                FirstName = "11",
                LastName = "22",
                MiddleName = "33",
                RecordbookNumber = "80",
                Group = group
            };
            */
            var faculty = new Faculty
            {
                Abbrev = "asdfafafafa",
                Name = "fdafds"
            };
            faculty.Id = 2;
            _unitOfWork.Faculties.Add(faculty);
            _unitOfWork.SaveAsync().Wait();
            faculty = _unitOfWork.Faculties.GetFacultyById(1);
            _unitOfWork.Faculties.Remove(faculty); 
            _unitOfWork.SaveAsync().Wait();
            return faculty;
        }
        [HttpGet("group")]
        public List<StudentsGroup> GetFacultyBy1Id()
        {
            var collection = new List<StudentsGroup>();
            var faculty = new Faculty
            {
                Id = 45678,
                Abbrev = "FKSiS",
                Name = "seti"
            };
            var form = new EducationForm
            {
                Id = 34567,
                Name = "Daily"
            };
            var speciality = new Speciality
            {
                Id = 1234,
                Abbrev = "poit",
                Name = "prog obes info tech",
                Code = "1-08 09",
                EducationForm = form,
                Faculty = faculty
            };
            var group = new StudentsGroup
            {
                Id = 11,
                Course = 4,
                Number = "08",
                Speciality = speciality
            };
            var student = new PhysicalEntity
            {
                Id = 1,
                FirstName = "11",
                LastName = "22",
                MiddleName = "33",
                RecordBookNumber = "80",
                Group = group.Number
            };

            collection.Add(group);

            return collection;
        }
        [HttpGet("studentdto")]
        public List<StudentDto> GetFacu1ltyBy1Id()
        {
            var collection = new List<StudentDto>();
            var faculty = new Faculty
            {
                Id = 45678,
                Abbrev = "FKSiS",
                Name = "seti"
            };
            var form = new EducationForm
            {
                Id = 34567,
                Name = "Daily"
            };
            var speciality = new Speciality
            {
                Id = 1234,
                Abbrev = "poit",
                Name = "prog obes info tech",
                Code = "1-08 09",
                EducationForm = form,
                Faculty = faculty
            };
            var group = new StudentsGroup
            {
                Id = 11,
                Course = 4,
                Number = "08",
                Speciality = speciality
            };
            var student = new PhysicalEntity
            {
                Id = 1,
                FirstName = "11",
                LastName = "22",
                MiddleName = "33",
                RecordBookNumber = "80",
                Group = group.Number
            };

            var student2 = _mapper.Map<StudentDto>(student);
            collection.Add(student2);

            return collection;
        }
    }
}
