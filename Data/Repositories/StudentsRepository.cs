using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Interfaces.Models;

namespace DataService.Data.Repositories
{
    public class StudentsRepository: RepositoryBase<PhysicalEntity>
    {
        public StudentsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {}
        public PhysicalEntity GetStudentByRecordBook(string recordBookNumber)
        {
            var query =
               from stud in ApplicationDbContext.Entities
               where stud.RecordBookNumber == recordBookNumber
               where stud.isStudent == true
               select stud;
            return query.FirstOrDefault();
        }
        public IEnumerable<PhysicalEntity> GetStudentsByGroup(string groupNumber)
        {
            var query =
               from stud in ApplicationDbContext.Entities
               where stud.isStudent == true
               where stud.Group == groupNumber
               select stud;
            return query;
        }
        public IEnumerable<PhysicalEntity> GetStudents()
        {
            var query =
               from stud in ApplicationDbContext.Entities
               where stud.isStudent == true
               select stud;
            return query;
        }
    }
}