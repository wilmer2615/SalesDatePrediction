namespace Entities
{
    public partial class Employee
    {
        public int Empid { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string TitleOfCourtesy { get; set; } = null!;

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string Country { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int? MgrId { get; set; }

        public virtual ICollection<Employee> InverseMgr { get; set; } = new List<Employee>();

        public virtual Employee? Mgr { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
