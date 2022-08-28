using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Models;
using Interfaces.DTOs;
using Interfaces;
using DataService.Data;
using AutoMapper;

namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EducationFormsController : MyControllerBase
    {
        public EducationFormsController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        [HttpGet]
        public IEnumerable<EducationForm> GetFaculties() => _unitOfWork.EducationForms.GetAll().ToList();

        [HttpGet("{id}")]
        public Faculty GetFacultyById(int id) => _unitOfWork.Faculties.GetFacultyById(id);

        [HttpPost]
        public void Post([FromBody] EducationFormDto value)
        {
            var faculty = _mapper.Map<EducationForm>(value);
            _unitOfWork.EducationForms.Add(faculty);
            _unitOfWork.SaveAsync().Wait();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EducationFormDto value)
        {
            var faculty = _mapper.Map<EducationForm>(value);
            _unitOfWork.EducationForms.Update(faculty);
            _unitOfWork.SaveAsync().Wait();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var faculty = _unitOfWork.EducationForms.FindAsync(id);
            faculty.Wait();
            _unitOfWork.EducationForms.Remove(faculty.Result);
            _unitOfWork.SaveAsync().Wait();
        }
    }
}
