using AutoMapper;
using IKEA.BLL.DTO_s;
using IKEA.BLL.Services.DepartmentServices;
using IKEA.DAL.Models.Departments;
using IKEA.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using NuGet.Protocol.Plugins;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentServices departmentServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;
        //private readonly IMapper mapper;

        public DepartmentController(IDepartmentServices _departmentServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departmentServices = _departmentServices;
            logger = _logger;
            this.environment = environment;
            //this.mapper = mapper;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
         //string name= ViewData["name"] as string;
         //   name = "eman";
         //       ViewData["Department"] = departmentServices.GetAllDepartments() ;
         

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

        public IActionResult Create(DepartmentVM departmentVM)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(departmentVM);
                var departmentDto = new CreatedDepartmentDto()
                {
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    CreationDate = departmentVM.CreationDate,
                };
                var result = departmentServices.CreateDepartment(departmentDto);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Department is not created");
                return View(departmentVM);
            }
            catch (Exception ex)
            {
                // Log the exception using Kestrel logging
                logger.LogError(ex, ex.Message);

                // Set default error message
                var message = environment.IsDevelopment() ? ex.Message : "An error occurred during the creation process.";

                ModelState.AddModelError(string.Empty, message);
                return View(departmentVM);
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

            var MappedDepartment = new DepartmentVM()
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

        public IActionResult Edit(DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var message = string.Empty;
            try
            {
                var departmentDto = new UpdatedDepartmentDto()
                {
                    Id = departmentVM.Id,
                    Name = departmentVM.Name,
                    Code = departmentVM.Code,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,

                };
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
            return View(departmentVM);



        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? itemId)
        {
            if (itemId is null)
                return BadRequest();

            var Department = departmentServices.GetDepartmentById(itemId.Value);

            if (Department is null)
                return NotFound();

            return View(Department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int itemId)
        {
            var Message = string.Empty;
            try
            {
                var IsDeleted = departmentServices.DeleteDepartment(itemId);
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
            return RedirectToAction(nameof(Delete), new { id = itemId });


        }

        #endregion

    }
}
