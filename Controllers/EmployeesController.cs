using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployeesService _employeesService;
        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            bool data = await _employeesService.CreateEmployee(employee);

            return Ok(data);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {

            bool data = await _employeesService.UpdateEmployee(id, employee);

            return Ok(data);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmplyee(int id)
        {

            bool data = await _employeesService.DeleleEmployee(id);

            return Ok(data);

        }
    }
}
