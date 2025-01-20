using AutoMapper;
using Entities;
using Models.Response;
using Repository.Repository.EmployeeRepository;

namespace Logic.EmployeeLogic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IMapper _mapper;

        private readonly IEmployeeRepository _employeeRepository;


        public EmployeeLogic(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            this._mapper = mapper;
            this._employeeRepository = employeeRepository;
        }
        public async Task<IEnumerable<EmployeeModel>> GetEmployeesAsync()
        {
            var entities = await this._employeeRepository.GetEmployeesAsync();

            var result = _mapper.Map<List<EmployeeModel>>(entities);

            return result;
        }
    }
}
