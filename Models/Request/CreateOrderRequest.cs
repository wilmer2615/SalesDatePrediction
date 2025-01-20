using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class CreateOrderRequest
    {
        [Required]
        public int CustId { get; set; }

        [Required]
        public int EmpId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Required]
        public int ShipperId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El valor de Freight debe ser mayor o igual a 0.")]
        public decimal Freight { get; set; }

        [Required]
        [MaxLength(40, ErrorMessage = "El nombre de envío no debe exceder 40 caracteres.")]
        public string ShipName { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "La dirección de envío no debe exceder 60 caracteres.")]
        public string ShipAddress { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "La ciudad de envío no debe exceder 15 caracteres.")]
        public string ShipCity { get; set; }

        [MaxLength(15, ErrorMessage = "La región de envío no debe exceder 15 caracteres.")]
        public string? ShipRegion { get; set; }

        [MaxLength(10, ErrorMessage = "El código postal de envío no debe exceder 10 caracteres.")]
        public string? ShipPostalCode { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "El país de envío no debe exceder 15 caracteres.")]
        public string ShipCountry { get; set; }

        [Required]
        public List<CreateOrderDetailRequest> OrderDetailList { get; set; }
    }
}
