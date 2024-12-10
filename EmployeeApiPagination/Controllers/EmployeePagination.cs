using Microsoft.AspNetCore.Mvc;
using EmployeeApiPagination.Models;
using EmployeeApiPagination.Services;

namespace EmployeeApiPagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DatabaseServices _databaseServices;

        public EmployeeController(DatabaseServices databaseServices)
        {
            _databaseServices = databaseServices;
        }

        // Get all employees without pagination or search
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Employees>> GetAllEmployees()
        {
            var employees = _databaseServices.GetAllEmployees(); // This method will fetch all employees
            return Ok(employees); // Return the list of all employees
        }

        // Get Paginated Employees with filtering and searching
        [HttpGet()]
        public ActionResult<IEnumerable<Employees>> GetEmployees(
            int pageNumber = 1,
            int pageSize = 10,
            string searchString = "")
        {
            var employees = _databaseServices.GetEmployees(pageNumber, pageSize,searchString);
            return Ok(employees);
        }

    }
}
