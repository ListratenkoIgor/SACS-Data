using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Interfaces.Models;

namespace DataService.Data.Repositories
{
    public class StudentsGroupsRepository: RepositoryBase<StudentsGroup>
    {
        public StudentsGroupsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        { }

        public StudentsGroup GetGroupByNumber(string groupNumber)
        {
            var  query =
               from groups in ApplicationDbContext.StudentsGroups
               join spec in ApplicationDbContext.Specialities
               on groups.Speciality.Id equals spec.Id
               where groups.Number == groupNumber
               select groups;
            return query.FirstOrDefault();

        }

        public IEnumerable<StudentsGroup> GetGroups()
        {
            IEnumerable<StudentsGroup> query =
                from groups in ApplicationDbContext.StudentsGroups
                join spec in ApplicationDbContext.Specialities
                on groups.Speciality.Id equals spec.Id
                select groups;
            return query;
        }
    }
}