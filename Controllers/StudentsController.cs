using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Models;
using Interfaces.DTOs;
using DataService.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Nancy.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : MyControllerBase
    {
        public StudentsController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        [HttpGet("group/{groupNumber}")]
        public IEnumerable<StudentDto> GetStudentsByGroup(string groupNumber) => _unitOfWork.Students.GetStudentsByGroup(groupNumber).ToList().ConvertToStudentDtoList(_mapper);
        [HttpGet("{recordBookNumber}")]
        public StudentDto GetStudentByRecordBook(string recordBookNumber) => _unitOfWork.Students.GetStudentByRecordBook(recordBookNumber).ConvertToStudentDto(_mapper);
        [HttpGet("stream/{groupsNumbers}")]
        public StudentsStream GetStudentsByGroups(string groupsNumbers)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<string> groups = serializer.Deserialize<List<string>>(groupsNumbers);
            //dynamic groupsDyn= JsonConvert.DeserializeObject<dynamic>(groupsNumbers);
            //List<string> groups = groupsDyn.groupsNumbers;
            var result = new StudentsStream();
            foreach (var group in groups)
            {
                var studlist = new List<StudentDto>();
                List<PhysicalEntity> students = _unitOfWork.Students.GetStudentsByGroup(group).ToList();
                //_unitOfWork.SaveAsync();
                if (students.Count == 0) { continue; }
                foreach (var student in students)
                {
                    studlist.Add(student.ConvertToStudentDto(_mapper));
                }
                result.Add(group, studlist);
            }
            return result;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IEnumerable<StudentDto> GetStudents() => _unitOfWork.Students.GetStudents().ToList().ConvertToStudentDtoList(_mapper);

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public void Post([FromBody] StudentDto value)
        {
            var student = _mapper.Map<PhysicalEntity>(value);
            _unitOfWork.Students.Add(student);
            _unitOfWork.SaveAsync().Wait();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] StudentDto value)
        {
            var stud = _unitOfWork.Students.GetFirstWhere(s => s.Id == value.Id);
            _unitOfWork.Students.Update(stud);
            _unitOfWork.SaveAsync().Wait();

        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var student = _unitOfWork.Students.FindAsync(id);
            student.Wait();
            _unitOfWork.Students.Remove(student.Result);
            _unitOfWork.SaveAsync().Wait();
        }
    }
}
