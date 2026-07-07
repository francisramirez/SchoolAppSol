using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Dtos.Department;
using SchoolAppSol.Application.Interfaces.Department;

namespace SchoolAppSol.Department.Api.Controllers
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
            var result = await _departmentService.GetAllDepartmentsAsync();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _departmentService.GetDepartmentByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> Post(DepartmentAddDto departmentAddDto)
        {
            var result = await _departmentService.CreateDepartmentAsync(departmentAddDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> Put(int id, UpdateDepartmentDto updateDepartment)
        {
            var result = await _departmentService.UpdateDepartmentAsync(id, updateDepartment);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _departmentService.DeleteDepartmentAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
  
}
