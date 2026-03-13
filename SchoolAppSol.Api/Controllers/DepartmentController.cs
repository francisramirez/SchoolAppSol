using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Department;
using SchoolAppSol.Application.Interfaces.Department;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<DepartmentModel>> result = new ServiceResult<List<DepartmentModel>>();

            result = await _departmentService.GetAllDepartmentsAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetDepartmentById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<DepartmentModel> result = new ServiceResult<DepartmentModel>();

            result = await _departmentService.GetDepartmentByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveDepartment")]
        public async Task<IActionResult> Post(DepartmentAddDto departmentAddDto)
        {
            ServiceResult<bool> result = new ServiceResult<bool>();

            result = await _departmentService.CreateDepartmentAsync(departmentAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> Post(UpdateDepartmentDto updateDepartmentDto)
        {
            ServiceResult<bool> result = new ServiceResult<bool>();

            result = await _departmentService.UpdateDepartmentAsync(updateDepartmentDto.Id, updateDepartmentDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteDepartment")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = new ServiceResult<bool>();

            result = await _departmentService.DeleteDepartmentAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
