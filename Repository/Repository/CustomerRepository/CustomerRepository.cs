using Microsoft.EntityFrameworkCore;
using Models.Response;

namespace Repository.Repository.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AplicationDbContext _context;

        public CustomerRepository(AplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<DatePredictionModel>> SalesDatePredictionAsync(string? customerName)
        {
            var dateRange = from order in _context.Orders
                            join customer in _context.Customers
                            on order.CustId equals customer.CustId
                            where EF.Functions.Like(customer.CompanyName, $"%{customerName}%") || string.IsNullOrEmpty(customerName)
                            group order by order.CustId into groupedOrders
                            select new
                            {
                                CustId = groupedOrders.Key,
                                FirstOrderDate = groupedOrders.Min(o => o.OrderDate),
                                LastOrderDate = groupedOrders.Max(o => o.OrderDate)
                            };

            var totalDateDiff = from range in dateRange
                                select new
                                {
                                    range.CustId,
                                    TotalDiff = (range.LastOrderDate - range.FirstOrderDate).Days
                                };

            var orderCount = from order in _context.Orders
                             group order by order.CustId into g
                             select new
                             {
                                 CustId = g.Key,
                                 OrderCount = g.Count()
                             };

            var avgDateDiff = from total in totalDateDiff
                              join count in orderCount on total.CustId equals count.CustId
                              select new
                              {
                                  total.CustId,
                                  AvgDiff = Math.Floor((double)total.TotalDiff / count.OrderCount)
                              };

            var lastOrder = from order in _context.Orders
                            group order by order.CustId into g
                            select new
                            {
                                Custid = g.Key,
                                LastOrderDate = g.Max(o => o.OrderDate)
                            };

            var result = from lo in lastOrder
                         join range in dateRange on lo.Custid equals range.CustId
                         join avg in avgDateDiff on lo.Custid equals avg.CustId
                         join count in orderCount on lo.Custid equals count.CustId
                         join customer in _context.Customers on lo.Custid equals customer.CustId
                         select new 
                         {
                             customer.CustId,
                             CustomerName = customer.CompanyName,
                             lo.LastOrderDate,
                             PredictedNextOrder = lo.LastOrderDate.AddDays(avg.AvgDiff)
                         };


            var resultList = await result.ToListAsync();

            var predictions = resultList.Select(r => new DatePredictionModel
            {
                CustId = r.CustId,
                CustomerName = r.CustomerName,
                LastOrderDate = r.LastOrderDate,
                NextPredictedOrder = r.PredictedNextOrder
            });

            return predictions;

        }
    }
}
