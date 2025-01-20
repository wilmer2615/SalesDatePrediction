namespace Entities
{
    public partial class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        public decimal UnitPrice { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual Supplier Supplier { get; set; } = null!;
    }
}
