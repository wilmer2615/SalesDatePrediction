namespace Entities
{
    public partial class OrderValue
    {
        public int OrderId { get; set; }

        public int? CustId { get; set; }

        public int EmpId { get; set; }

        public int ShipperId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal? Val { get; set; }
    }
}
