using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.BLL.DTO_s;

namespace IKEA.BLL.Services.DepartmentServices
{
    public interface IDepartmentServices
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);

        int CreateDepartment(CreatedDepartmentDto department);
        int UpdateDepartment(UpdatedDepartmentDto department);

        bool DeleteDepartment(int id);
    }
}
