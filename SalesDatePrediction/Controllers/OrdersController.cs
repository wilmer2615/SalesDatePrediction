using Logic.OrderLogic;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderLogic _orderLogic;

        public OrdersController(IOrderLogic orderLogic)
        {
            this._orderLogic = orderLogic;
        }

        /// <summary>
        /// Accion que permite listar ordenes de un cliente.
        /// <param name="clientId">Identificador de los registros.</param>
        /// </summary>
        /// <returns>istado de ordenes.</returns>
        [HttpGet("{clientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientOrders(int clientId)
        {
            var result = await _orderLogic.GetClientOrdersAsync(clientId);

            if (result == null || !result.Any())
            {
                return NotFound(new { Message = "El cliente no tiene órdenes registradas en la base de datos!" });
            }

            return Ok(result);
        }

        /// <summary>
        /// Acción que permite crear una nueva orden.
        /// </summary>
        /// <param name="request">Datos necesarios para crear la orden.</param>
        /// <returns>ID de la orden creada.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderId = await _orderLogic.CreateOrderAsync(request);

                return CreatedAtAction(
                    nameof(GetClientOrders),
                    new { clientId = request.CustId },
                    new { OrderId = orderId }
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error al procesar la solicitud.", Details = ex.Message });
            }
        }
    }
}
