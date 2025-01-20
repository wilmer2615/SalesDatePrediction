using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public partial class Order
    {
        public int OrderId { get; set; }

        public int? CustId { get; set; }

        public int EmpId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int ShipperId { get; set; }

        public decimal Freight { get; set; }

        public string ShipName { get; set; } = null!;

        public string ShipAddress { get; set; } = null!;

        public string ShipCity { get; set; } = null!;

        public string? ShipRegion { get; set; }

        public string? ShipPostalCode { get; set; }

        public string ShipCountry { get; set; } = null!;

        public virtual Customer? Cust { get; set; }

        public virtual Employee Emp { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Shipper Shipper { get; set; } = null!;
    }
}
