using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RsCase.Employee.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Bütün çalışanları getiren endpoint
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthConstants.RoleUser)]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_employeeService.GetAllEmployee());
        }

        [Authorize(AuthConstants.RoleUser)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Result<EmployeeDto> employeeResult = _employeeService.GetEmployeeById(id);
            if(employeeResult.IsSuccess)
                return Ok(employeeResult);

            return NotFound();
        }

        [Authorize(AuthConstants.RoleAdmin)]
        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeeDto newEmployee)
        {
            var result = _employeeService.AddEmployee(newEmployee);
            if(result.IsSuccess)
                return StatusCode(201, result);

            return StatusCode(204, result);
        }

        [Authorize(AuthConstants.RoleAdmin)]
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(string id, [FromBody] EmployeeDto updateEmployee)
        {
            var result = _employeeService.UpdateEmployee(id, updateEmployee);
            if(result.IsSuccess)
                return StatusCode(201, result);

            return StatusCode(204, result);
        }

        [Authorize(AuthConstants.RoleAdmin)]
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(string id)
        {
            var result = _employeeService.DeleteEmployee(id);
            if(result.IsSuccess)
                return StatusCode(200, result);
            
            return StatusCode(204,result);
        }

    }
}


