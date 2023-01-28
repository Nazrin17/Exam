using ExamTask.Core.DAL.Repository.Implementations;
using ExamTask.DAL.Repository.Interfaces;
using ExamTask.Models;

namespace ExamTask.DAL.Repository.Implementations
{
    public class EmployeeRepository:BaseRepository<Employee>,IEmployeeRepository
    {
        private readonly ExamDbContext _context;

        public EmployeeRepository(ExamDbContext context):base(context)
        {
            _context = context;
        }
    }
}
