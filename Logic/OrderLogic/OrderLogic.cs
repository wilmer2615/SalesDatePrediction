using AutoMapper;
using Entities;
using Models.Request;
using Models.Response;
using Repository.Repository.OrderRepository;

namespace Logic.OrderLogic
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderRepository;

        public OrderLogic(IMapper mapper, IOrderRepository orderRepository)
        {
            this._mapper = mapper;
            this._orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderModel>> GetClientOrdersAsync(int clientId)
        {
            var entities = await this._orderRepository.GetClientOrdersAsync(clientId);

            var result = _mapper.Map<List<OrderModel>>(entities);

            return result;
        }

        public async Task<int> CreateOrderAsync(CreateOrderRequest request)
        {
            Order order = _mapper.Map<Order>(request);
            List<OrderDetail> orderDetails = _mapper.Map<List<OrderDetail>>(request.OrderDetailList);

            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (orderDetails == null || !orderDetails.Any())
                throw new ArgumentException("Order details cannot be null or empty.", nameof(orderDetails));

            /// Llama al repositorio para insertar la orden y retorna el nuevo OrderId:
            int orderId = await _orderRepository.InsertOrderAsync(order, orderDetails);
            return orderId;
        }
    }
}
