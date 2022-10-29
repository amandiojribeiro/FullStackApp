using FullStackBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FullStackBackend.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateEmployee(Employee employee)
        {
            if(employee == null)
                throw new ArgumentException(nameof(employee));
            
            await _context.AddAsync(employee);
        }

        public void Delete(Employee employee)
        {
             if(employee == null)
                throw new ArgumentException(nameof(employee));

             _context.Employess.Remove(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employess.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employess.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}