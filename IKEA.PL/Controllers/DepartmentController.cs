using IKEA.BLL.DTO_s;
using IKEA.BLL.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartmentServices _departmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Department = departmentServices.GetAllDepartments();
            return View(Department);
        }
        #endregion

        #region Details
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departmentServices.GetDepartmentById(id.Value);

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
        [ValidateAntiForgeryToken]

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

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null)
                return BadRequest();
            var department = departmentServices.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate,
            };
            return View(MappedDepartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(UpdatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return View(departmentDto);
            var message = string.Empty;
            try
            {
                var Result = departmentServices.UpdateDepartment(departmentDto);
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
            return View(departmentDto);



        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? DeptId)
        {
            if (DeptId is null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentById(DeptId.Value);

            if (Department is null)
                return NotFound();

            return View(Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int DeptId)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(DeptId);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                Message = "Department is Not Deleted";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                Message = environment.IsDevelopment() ? ex.Message : "An Error has been ocurred during delete the department";
            }
            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { id = DeptId });


        }

        #endregion

    }
}
