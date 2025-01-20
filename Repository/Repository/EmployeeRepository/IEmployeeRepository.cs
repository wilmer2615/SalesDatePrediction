using Entities;

namespace Repository.Repository.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}
