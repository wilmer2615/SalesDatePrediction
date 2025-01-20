using Entities;
using Models.Request;
using Models.Response;

namespace Logic.OrderLogic
{
    public interface IOrderLogic
    {
        public Task<IEnumerable<OrderModel>> GetClientOrdersAsync(int clientId);

        Task<int> CreateOrderAsync(CreateOrderRequest request);
    }
}
