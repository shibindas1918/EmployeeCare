using EmployeeCare.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            
        }
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            var employee = _employeeRepository.GetAllEmployee();
            return Ok(employee);    
        }
        [HttpPost]
        public ActionResult createEmployee(Employee employee)
        {
            _employeeRepository.createEmployee(employee);
            var allEmp = _employeeRepository.GetAllEmployee();
            var response = new
            {
                Message = "The employee is added to the system ",
                TotalEmployees = allEmp

            };
            return Ok(response);
        }
        [HttpPut]
        public ActionResult UpdateEmployee (Employee employee)
        {
             
            _employeeRepository.updateEmployee(employee);
            return Ok();
        }
        [HttpDelete]
        public ActionResult DeleteEmployee(int id)
        {
            
            _employeeRepository.DeleteEmployee(id);
            var employee = _employeeRepository.GetAllEmployee();
            var response = new
            {
                Message = "The employee was deleted successfully.",
                RemainingEmployees = employee
            };
            return Ok(response);
        }
    }
}
