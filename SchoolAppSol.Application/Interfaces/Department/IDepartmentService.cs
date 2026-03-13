using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Department;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Application.Interfaces.Department
{
    public interface IDepartmentService
    {
        Task<ServiceResult<List<DepartmentModel>>> GetAllDepartmentsAsync();
        Task<ServiceResult<DepartmentModel>> GetDepartmentByIdAsync(int id);
        Task<ServiceResult<bool>> CreateDepartmentAsync(DepartmentAddDto createDepartmentDto);
        Task<ServiceResult<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto);
        Task<ServiceResult<bool>> DeleteDepartmentAsync(int id);
    }
}
