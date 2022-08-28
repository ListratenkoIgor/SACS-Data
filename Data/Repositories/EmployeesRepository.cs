using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Interfaces.Models;

namespace DataService.Data.Repositories
{
    public class EmployeesRepository: RepositoryBase<PhysicalEntity>
    {
        public EmployeesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        { }

        public PhysicalEntity GetEmployeeByUrlId(string urlId)
        {
            var query =
               from employee in ApplicationDbContext.Entities
               where employee.UrlId == urlId
               where employee.isStudent == false
               select employee;
            return query.FirstOrDefault();
        }

        internal IEnumerable<PhysicalEntity> GetEmployees()
        {
            var query =
               from employee in ApplicationDbContext.Entities
               where employee.isStudent == false
               select employee;
            return query;
        }
    }
}