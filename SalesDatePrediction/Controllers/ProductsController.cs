using Logic.ProductLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductLogic _productLogic;

        public ProductsController(IProductLogic productLogic)
        {
            this._productLogic = productLogic;
        }

        /// <summary>
        /// Accion que permite listar los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        [HttpGet()]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productLogic.GetProductsAsync();
            if (result == null || !result.Any())
                return NotFound(new ErrorResponse { Message = "No hay productos registrados en la base de datos!" });

            return Ok(result);
        }
    }
}
