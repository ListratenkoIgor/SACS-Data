using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Models;
using DataService.Data;
using Interfaces.DTOs;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpecialityController : MyControllerBase
    {
        public SpecialityController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        [HttpGet]
        public IEnumerable<Speciality> GetSpecialities() => _unitOfWork.Specialities.GetSpecialities();

        [HttpGet("{id}")]
        public Speciality GetSpecialityById(int id) => _unitOfWork.Specialities.GetSpecialityById(id);

        [HttpPost]
        public void Post([FromBody] SpecialityDto value)
        {
            var speciality = _mapper.Map<Speciality>(value);
            var faculty = speciality.Faculty;
            var form = speciality.EducationForm;
            _unitOfWork.Specialities.Add(speciality);
            _unitOfWork.SaveAsync().Wait();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] SpecialityDto value)
        {
            var speciality = _mapper.Map<Speciality>(value);
            _unitOfWork.Specialities.Update(speciality);
            _unitOfWork.SaveAsync().Wait();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var qroup = _unitOfWork.Specialities.FindAsync(id);
            qroup.Wait();
            _unitOfWork.Specialities.Remove(qroup.Result);
            _unitOfWork.SaveAsync().Wait();
        }
    }
}
