using Models.Response;
using Repository.Repository.CustomerRepository;

namespace Logic.CustomerLogic
{
    public class CustomerLogic : ICustomerLogic
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerLogic(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public async Task<IEnumerable<DatePredictionModel>> SalesDatePredictionAsync(string? CustomerName)
        {
            var result = await this._customerRepository.SalesDatePredictionAsync(CustomerName);

            return result;
        }
    }
}
