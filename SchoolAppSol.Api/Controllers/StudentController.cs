using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Student;
using SchoolAppSol.Application.Interfaces.Student;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetStudents")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<StudentModel>> result = await _studentService.GetAllStudentsAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetStudentById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<StudentModel> result = await _studentService.GetStudentByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveStudent")]
        public async Task<IActionResult> Post(StudentAddDto studentAddDto)
        {
            ServiceResult<bool> result = await _studentService.CreateStudentAsync(studentAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> Post(UpdateStudentDto updateStudentDto)
        {
            ServiceResult<bool> result = await _studentService.UpdateStudentAsync(updateStudentDto.StudentId, updateStudentDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteStudent")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _studentService.DeleteStudentAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
