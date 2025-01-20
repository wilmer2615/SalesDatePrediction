using Models.Response;

namespace Repository.Repository.CustomerRepository
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<DatePredictionModel>> SalesDatePredictionAsync(string? CustomerName);
    }
}
