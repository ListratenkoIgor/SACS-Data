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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FacultyController : MyControllerBase
    {
        public FacultyController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        [HttpGet]
        public IEnumerable<Faculty> GetFaculties() => _unitOfWork.Faculties.GetFaculties();

        [HttpGet("{id}")]
        public Faculty GetFacultyById(int id) => _unitOfWork.Faculties.GetFacultyById(id);

        [HttpPost]
        public void Post([FromBody] FacultyDto value)
        {
            var faculty = _mapper.Map<Faculty>(value);
            _unitOfWork.Faculties.Add(faculty);
            _unitOfWork.SaveAsync().Wait();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] FacultyDto value)
        {
            var faculty = _mapper.Map<Faculty>(value);
            _unitOfWork.Faculties.Update(faculty);
            _unitOfWork.SaveAsync().Wait();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var faculty = _unitOfWork.Faculties.FindAsync(id);
            faculty.Wait();
            _unitOfWork.Faculties.Remove(faculty.Result);
            _unitOfWork.SaveAsync().Wait();
        }
    }
}
