using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DataService.Data.Repositories;

namespace DataService.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private bool _disposed = false;

        private EducationFormsRepository _educationFormsRepository;
        private EmployeesRepository _employeesRepository;
        private FacultiesRepository _facultiesRepository;
        private SpecialitiesRepository _specialitiesRepository;
        private StudentsGroupsRepository _studentsGroupsRepository;
        private StudentsRepository _studentsRepository;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _applicationDbContextFactory = (IDbContextFactory<ApplicationDbContext>)serviceProvider.GetService(typeof(IDbContextFactory<ApplicationDbContext>));
        }
        private ApplicationDbContext _applicationDbContext;
        public ApplicationDbContext ApplicationDbContext => _applicationDbContext = _applicationDbContext ?? _applicationDbContextFactory.CreateDbContext();

        public EducationFormsRepository EducationForms => _educationFormsRepository = _educationFormsRepository ?? new EducationFormsRepository(ApplicationDbContext);
        public EmployeesRepository Employees => _employeesRepository = _employeesRepository ?? new EmployeesRepository(ApplicationDbContext);
        public FacultiesRepository Faculties => _facultiesRepository = _facultiesRepository ?? new FacultiesRepository(ApplicationDbContext);
        public SpecialitiesRepository Specialities => _specialitiesRepository = _specialitiesRepository ?? new SpecialitiesRepository(ApplicationDbContext);
        public StudentsGroupsRepository StudentsGroups => _studentsGroupsRepository = _studentsGroupsRepository ?? new StudentsGroupsRepository(ApplicationDbContext);
        public StudentsRepository Students => _studentsRepository = _studentsRepository ?? new StudentsRepository(ApplicationDbContext);

        public async Task<bool> SaveAsync()
        {
            try
            {
                await ApplicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
                return false;
            }
        }
        public IDbContextTransaction BeginTransaction()
        {
            return ApplicationDbContext.Database.BeginTransaction();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ApplicationDbContext.Dispose();
                }
                _disposed = true;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
