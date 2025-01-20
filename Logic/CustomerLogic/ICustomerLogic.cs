using Models.Response;

namespace Logic.CustomerLogic
{
    public interface ICustomerLogic
    {
        public Task<IEnumerable<DatePredictionModel>> SalesDatePredictionAsync(string? CustomerName);
    }
}
