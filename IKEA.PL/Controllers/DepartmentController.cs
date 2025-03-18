using IKEA.BLL.DTO_s;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices ,ILogger<DepartmentController> _logger,IWebHostEnvironment environment )
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Department = departmentServices.GetAllDepartments();
            return View(Department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
                return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(departmentDto);

                var result = departmentServices.CreateDepartment(departmentDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Department is not created");
                return View(departmentDto);
            }
            catch (Exception ex)
            {
                // Log the exception using Kestrel logging
                logger.LogError(ex, ex.Message);

                // Set default error message
                var message = environment.IsDevelopment() ? ex.Message : "An error occurred during the creation process.";

                ModelState.AddModelError(string.Empty, message);
                return View(departmentDto);
            }

        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentServices.GetDepartmentById(id.Value);

            if(department is null)
                return NotFound();

            return View(department);
        }

    }
}
