using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Interfaces.Models;

namespace DataService.Data.Repositories
{
    public class SpecialitiesRepository: RepositoryBase<Speciality>
    {
        public SpecialitiesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        { }

        public IEnumerable<Speciality> GetSpecialities()
        {
            var query =
               from spec in ApplicationDbContext.Specialities
               select spec;
            return query;
        }

        public Speciality GetSpecialityById(int id)
        {
            var query =
               from spec in ApplicationDbContext.Specialities
               where spec.Id == id
               select spec;
            return query.FirstOrDefault();
        }
    }
}