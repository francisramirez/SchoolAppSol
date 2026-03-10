using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Course;
using SchoolAppSol.Application.Interfaces.Course;
using SchoolAppSol.Domain.Models;


namespace SchoolAppSol.Api.Controllers
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
        // GET: api/<CourseController>
        [HttpGet("GetCourses")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<CourseModel>> result = new ServiceResult<List<CourseModel>>();


            result = await _courseService.GetAllCoursesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("GetCourseById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<CourseModel> result = new ServiceResult<CourseModel>();

            result = await _courseService.GetCourseByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // POST api/<CourseController>
        [HttpPost("SaveCourse")]
        public async Task<IActionResult> Post(CourseAddDto courseAddDto)
        {

            ServiceResult<bool> result = new ServiceResult<bool>();

            result = await _courseService.CreateCourseAsync(courseAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateCourse")]
        public async Task<IActionResult> Post(UpdateCourseDto updateCourseDto)
        {
            ServiceResult<bool> result = new ServiceResult<bool>();

            result = await _courseService.UpdateCourseAsync(updateCourseDto.Id,updateCourseDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

       

    }
}

