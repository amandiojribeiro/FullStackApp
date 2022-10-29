using FullStackBackend.Models;

namespace FullStackBackend.Data
{
    public interface IEmployeeRepository
    {
        Task SaveChangesAsync();
        Task CreateEmployee(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        void Delete(Employee employee);
    }
}