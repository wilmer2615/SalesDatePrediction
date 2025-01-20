using System.ComponentModel.DataAnnotations;

namespace Models.Request
{
    public class CreateOrderDetailRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser mayor a 0.")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public short Qty { get; set; }

        [Range(0, 1, ErrorMessage = "El descuento debe estar entre 0 y 1.")]
        public decimal Discount { get; set; }
    }
}
