using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AplicationDbContext _context;

        public EmployeeRepository(AplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await this._context.Set<Employee>()
                .ToListAsync();
        }
    }
}
