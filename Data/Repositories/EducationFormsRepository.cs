using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Interfaces.Models;

namespace DataService.Data.Repositories
{
    public class EducationFormsRepository: RepositoryBase<EducationForm>
    {
        public EducationFormsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        { }
    }
}