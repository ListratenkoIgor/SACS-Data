using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Interfaces.Models;

namespace DataService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<StudentsGroup> StudentsGroups { get; set; }
        public DbSet<PhysicalEntity> Entities { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
