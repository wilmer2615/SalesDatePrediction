using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Repository.Repository.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly AplicationDbContext _context;

        public OrderRepository(AplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<Order>> GetClientOrdersAsync(int clientId)
        {
            return await _context.Orders
                .Where(order => order.CustId == clientId)
                .ToListAsync();
        }

        public async Task<int> InsertOrderAsync(
            Order order,
            List<OrderDetail> orderDetails)
        {
            // Crea un DataTable para el parámetro de tipo tabla
            var orderDetailsTable = new DataTable();
            orderDetailsTable.Columns.Add("productid", typeof(int));
            orderDetailsTable.Columns.Add("unitprice", typeof(decimal));
            orderDetailsTable.Columns.Add("qty", typeof(short));
            orderDetailsTable.Columns.Add("discount", typeof(decimal));

            // Llena el DataTable con los datos de orderDetails
            foreach (var detail in orderDetails)
            {
                orderDetailsTable.Rows.Add(detail.ProductId, detail.UnitPrice, detail.Qty, detail.Discount);
            }

            // Define los parámetros
            var parameters = new[]
            {
                new SqlParameter("@custid", order.CustId),
                new SqlParameter("@empid", order.EmpId),
                new SqlParameter("@orderdate", order.OrderDate),
                new SqlParameter("@requireddate", order.RequiredDate),
                new SqlParameter("@shippeddate", order.ShippedDate ?? (object)DBNull.Value),
                new SqlParameter("@shipperid", order.ShipperId),
                new SqlParameter("@freight", order.Freight),
                new SqlParameter("@shipname", order.ShipName),
                new SqlParameter("@shipaddress", order.ShipAddress),
                new SqlParameter("@shipcity", order.ShipCity),
                new SqlParameter("@shipregion", order.ShipRegion ?? (object)DBNull.Value),
                new SqlParameter("@shippostalcode", order.ShipPostalCode ?? (object)DBNull.Value),
                new SqlParameter("@shipcountry", order.ShipCountry),
                new SqlParameter("@OrderDetails", SqlDbType.Structured)
                {
                    TypeName = "Sales.OrderDetailsType",
                    Value = orderDetailsTable
                },
                new SqlParameter("@OrderId", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            // Ejecutar el procedimiento almacenado
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC [Sales].[InsertOrder] @custid, @empid, @orderdate, @requireddate, @shippeddate, @shipperid, @freight, @shipname, @shipaddress, @shipcity, @shipregion, @shippostalcode, @shipcountry, @OrderDetails, @OrderId OUTPUT",
                parameters
            );

            // Obtener el OrderId después de la ejecución del procedimiento
            var orderId = (int)parameters[14].Value;

            return orderId;
        }
    }
}
