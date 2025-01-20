using Logic.CustomerLogic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerLogic _customerLogic;

        public CustomersController(ICustomerLogic customerLogic)
        {
            this._customerLogic = customerLogic;
        }


        /// <summary>
        /// Acción que permite listar la fecha de próxima orden de cada cliente o filtrar por nombre del cliente.
        /// </summary>
        /// <param name="CustomerName">Nombre del cliente (opcional).</param>
        /// <returns>Listado de próxima orden o datos filtrados por cliente.</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SalesDatePrediction([FromQuery] string? CustomerName)
        {
            var result = await _customerLogic.SalesDatePredictionAsync(CustomerName);
            return Ok(result);
        }
    }
}
