using IKEA.BLL.DTO_s;
using IKEA.BLL.DTO_s.Employee;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger<EmployeeController> logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices _employeeServices, ILogger<EmployeeController> _logger, IWebHostEnvironment environment)
        {
            employeeServices = _employeeServices;
            logger = _logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = employeeServices.GetAllEmployees();
            return View(employees);
        }
        #endregion
        #region Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = employeeServices.GetEmployeeById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }
        #endregion
        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(employeeDto);

                var result = employeeServices.CreateEmployee(employeeDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee is not created");
                return View(employeeDto);
            }
            catch (Exception ex)
            {
                // Log the exception using Kestrel logging
                logger.LogError(ex, ex.Message);

                // Set default error message
                var message = environment.IsDevelopment() ? ex.Message : "An error occurred during the creation process.";

                ModelState.AddModelError(string.Empty, message);
                return View(employeeDto);
            }

        }

        #endregion


    }
}
