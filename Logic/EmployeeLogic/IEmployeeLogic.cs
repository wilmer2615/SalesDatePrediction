using Entities;
using Models.Response;

namespace Logic.EmployeeLogic
{
    public interface IEmployeeLogic
    {
        public Task<IEnumerable<EmployeeModel>> GetEmployeesAsync();
    }
}
