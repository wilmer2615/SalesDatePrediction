using Logic.EmployeeLogic;
using Microsoft.AspNetCore.Mvc;
using Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesDatePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeeLogic _employeeLogic;

        public EmployeesController(IEmployeeLogic employeeLogic)
        {
            this._employeeLogic = employeeLogic;
        }


        /// <summary>
        /// Accion que permite listar los empleados.
        /// </summary>
        /// <returns>istado de empleados.</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _employeeLogic.GetEmployeesAsync();
            if (result == null || !result.Any())
                return NotFound(new ErrorResponse { Message = "No hay empleados registrados en la base de datos!" });

            return Ok(result);
        }
    }
}
