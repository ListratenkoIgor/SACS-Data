using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Interfaces.Models;

namespace DataService.Data.Repositories
{
    public class FacultiesRepository: RepositoryBase<Faculty>
    {
        public FacultiesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        { }

        public Faculty GetFacultyById(int id)
        {
            var query =
                from faculty in ApplicationDbContext.Faculties
                where faculty.Id == id
                select faculty;
            return query.FirstOrDefault();
        }

        public IEnumerable<Faculty> GetFaculties()
        {
            var query =
               from faculty in ApplicationDbContext.Faculties
               select faculty;
            return query;
        }
    }
}