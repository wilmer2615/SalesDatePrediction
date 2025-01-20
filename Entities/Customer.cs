namespace Entities
{
    public partial class Customer
    {
        public int CustId { get; set; }

        public string CompanyName { get; set; } = null!;

        public string ContactName { get; set; } = null!;

        public string ContactTitle { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string Country { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Fax { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
