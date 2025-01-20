using Logic.ShipperLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperLogic _shipperLogic;

        public ShippersController(IShipperLogic shipperLogic)
        {
            this._shipperLogic = shipperLogic;
        }

        /// <summary>
        /// Accion que permite listar los transportistas.
        /// </summary>
        /// <returns>Lista de transportistas.</returns>
        [HttpGet()]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShippers()
        {
            var result = await _shipperLogic.GetShippersAsync();
            if (result == null || !result.Any())
                return NotFound(new ErrorResponse { Message = "No hay transportistas registrados en la base de datos!" });

            return Ok(result);
        }
    }
}
