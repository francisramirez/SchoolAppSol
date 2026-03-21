using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.CourseEnrollment;
using SchoolAppSol.Application.Interfaces.CourseEnrollment;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseEnrollmentController : ControllerBase
    {
        private readonly ICourseEnrollmentService _courseEnrollmentService;

        public CourseEnrollmentController(ICourseEnrollmentService courseEnrollmentService)
        {
            _courseEnrollmentService = courseEnrollmentService;
        }

        [HttpGet("GetCourseEnrollments")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<CourseEnrollmentModel>> result = await _courseEnrollmentService.GetAllCourseEnrollmentsAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCourseEnrollmentById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<CourseEnrollmentModel> result = await _courseEnrollmentService.GetCourseEnrollmentByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetCourseEnrollmentsByStudentId")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            ServiceResult<List<CourseEnrollmentModel>> result = await _courseEnrollmentService.GetCourseEnrollmentsByStudentIdAsync(studentId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveCourseEnrollment")]
        public async Task<IActionResult> Post(CourseEnrollmentAddDto courseEnrollmentAddDto)
        {
            ServiceResult<bool> result = await _courseEnrollmentService.CreateCourseEnrollmentAsync(courseEnrollmentAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateCourseEnrollment")]
        public async Task<IActionResult> Post(UpdateCourseEnrollmentDto updateCourseEnrollmentDto)
        {
            ServiceResult<bool> result = await _courseEnrollmentService.UpdateCourseEnrollmentAsync(updateCourseEnrollmentDto.EnrollmentId, updateCourseEnrollmentDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteCourseEnrollment")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _courseEnrollmentService.DeleteCourseEnrollmentAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
