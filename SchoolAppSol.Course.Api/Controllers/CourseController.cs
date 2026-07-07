using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Course;
using SchoolAppSol.Application.Interfaces.Course;

namespace SchoolAppSol.Course.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> Get()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> Create([FromBody] CourseAddDto courseAddDto)
        {
            ServiceResult<bool> result = new ServiceResult<bool>();

            if (courseAddDto == null)
            {
                result.Success = false;
                result.Message = "Course data is null.";
                return BadRequest(result);
            }

            result = await _courseService.CreateCourseAsync(courseAddDto);

            return Ok(result); // 200 //

        }
        [HttpPut("UpdateCourse/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto courseUpdateDto)
        {
            if (courseUpdateDto == null)
            {
                return BadRequest(new ServiceResult<bool>
                {
                    Success = false,
                    Message = "Course data is null."
                });
            }
            var result = await _courseService.UpdateCourseAsync(id, courseUpdateDto);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
    }
}
