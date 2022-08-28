using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Data;
using Interfaces.Models;
using Interfaces.DTOs;
using Interfaces;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsGroupsController : MyControllerBase
    {
        public StudentsGroupsController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        [HttpGet]
        public IEnumerable<StudentsGroup> GetGroups() => _unitOfWork.StudentsGroups.GetGroups();

        [HttpGet("{groupNumber}")]
        public StudentsGroup GetGroupByNumber(string groupNumber) => _unitOfWork.StudentsGroups.GetGroupByNumber(groupNumber);

        [HttpPost]
        public void Post([FromBody] StudentsGroupDto value)
        {
            var group = _mapper.Map<StudentsGroup>(value);
            _unitOfWork.StudentsGroups.Add(group);
            _unitOfWork.SaveAsync().Wait();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] StudentsGroupDto value)
        {
            var group = _mapper.Map<StudentsGroup>(value);
            _unitOfWork.StudentsGroups.Update(group);
            _unitOfWork.SaveAsync().Wait();

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var qroup = _unitOfWork.StudentsGroups.FindAsync(id);
            qroup.Wait();
            _unitOfWork.StudentsGroups.Remove(qroup.Result);
            _unitOfWork.SaveAsync().Wait();
        }
    }
}
