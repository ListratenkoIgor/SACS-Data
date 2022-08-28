using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Models;
using Interfaces.DTOs;
using Interfaces;
using DataService.Data;
using AutoMapper;
using Newtonsoft.Json;


namespace DataService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MigartionController : MyControllerBase
    {
        public MigartionController(UnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { 
            
        }
        [HttpGet("FromLocal")]
        public void Local() {
            List<Faculty> faculties = _unitOfWork.Faculties.GetAll().ToList();
            List<Speciality> specialities = _unitOfWork.Specialities.GetAll().ToList();
            List<EducationForm> educationForms = _unitOfWork.EducationForms.GetAll().ToList();
            List<StudentsGroup> studentsGroups = _unitOfWork.StudentsGroups.GetAll().ToList();
            string json = "";
            //json = JsonConvert.SerializeObject(faculties);
            string path = @"C:\faculties.json"; 
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(faculties, Formatting.Indented));
            path = @"C:\specialities.json";
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(specialities, Formatting.Indented));
            path = @"C:\educationForms.json";
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(educationForms, Formatting.Indented));
            path = @"C:\studentsGroups.json";
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(studentsGroups, Formatting.Indented));
        }
        [HttpGet("ToNext")]
        public void Next() {
            List<Faculty> faculties = _unitOfWork.Faculties.GetAll().ToList();
            List<Speciality> specialities = _unitOfWork.Specialities.GetAll().ToList();
            List<EducationForm> educationForms = _unitOfWork.EducationForms.GetAll().ToList();
            List<StudentsGroup> studentsGroups = _unitOfWork.StudentsGroups.GetAll().ToList();           

            string path = @"C:\faculties.json";
            string data = System.IO.File.ReadAllText(path, Encoding.ASCII);
            faculties = JsonConvert.DeserializeObject<List<Faculty>>(data);
            path = @"C:\specialities.json";
            data = System.IO.File.ReadAllText(path, Encoding.ASCII);
            specialities = JsonConvert.DeserializeObject<List<Speciality>>(data);
            path = @"C:\educationForms.json";
            data = System.IO.File.ReadAllText(path, Encoding.ASCII);
            educationForms = JsonConvert.DeserializeObject<List<EducationForm>>(data);
            path = @"C:\studentsGroups.json";
            data = System.IO.File.ReadAllText(path, Encoding.ASCII);
            studentsGroups = JsonConvert.DeserializeObject<List<StudentsGroup>>(data);
            foreach (var x in faculties) {
                _unitOfWork.Faculties.Add(x);
                _unitOfWork.SaveAsync().Wait();
            }
            foreach (var x in specialities)
            {
                _unitOfWork.Specialities.Add(x);
                _unitOfWork.SaveAsync().Wait();
            }
            foreach (var x in educationForms)
            {
                _unitOfWork.EducationForms.Add(x);
                _unitOfWork.SaveAsync().Wait();

            }
            foreach (var x in studentsGroups)
            {
                _unitOfWork.StudentsGroups.Add(x);
                _unitOfWork.SaveAsync().Wait();
            }
        }

    }
}
