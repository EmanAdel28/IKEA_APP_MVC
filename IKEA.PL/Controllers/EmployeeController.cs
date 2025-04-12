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
        public IActionResult Index(string Search)
        {
            var employees = employeeServices.GetAllEmployees(Search);
            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(employeeDto);

                var result = employeeServices.CreateEmployee(employeeDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {
                // Log the exception using Kestrel logging
                logger.LogError(ex, ex.Message);

                // Set default error message
                var message = environment.IsDevelopment() ? ex.Message : "An error occurred during the creation process.";

            }
            ModelState.AddModelError(string.Empty, "Employee is not created");
            return View(employeeDto);

    

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

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null)
                return BadRequest();
            var employee = employeeServices.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            var MappedEmployee = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType
            };
            return View(MappedEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(UpdatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return View(employeeDto);
            var message = string.Empty;
            try
            {
                var Result = employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Department is Not Update";

            }
            catch (Exception ex)
            {
                // Log the exception using Kestrel logging
                logger.LogError(ex, ex.Message);

                // Set default error message
                message = environment.IsDevelopment() ? ex.Message : "An error occurred during the creation process.";


            }
            ModelState.AddModelError(string.Empty, message);
            return View(employeeDto);



        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? itemId)
        {
            if (itemId is null)
                return BadRequest();

            var Employee = employeeServices.GetEmployeeById(itemId.Value);

            if (Employee is null)
                return NotFound();

            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int itemId)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = employeeServices.DeleteEmployee(itemId);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                Message = "Employee is Not Deleted";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error has been ocurred during delete the employee";
            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { id = itemId });


        }

        #endregion




    }
}
