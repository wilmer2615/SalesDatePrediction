using Entities;

namespace Repository.Repository.OrderRepository
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetClientOrdersAsync(int clientId);

        public Task<int> InsertOrderAsync(Order order, List<OrderDetail> orderDetails);
    }
}
