using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.OnlineCourse;
using SchoolAppSol.Application.Interfaces.OnlineCourse;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineCourseController : ControllerBase
    {
        private readonly IOnlineCourseService _onlineCourseService;

        public OnlineCourseController(IOnlineCourseService onlineCourseService)
        {
            _onlineCourseService = onlineCourseService;
        }

        [HttpGet("GetOnlineCourses")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<OnlineCourseModel>> result = await _onlineCourseService.GetAllOnlineCoursesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetOnlineCourseById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<OnlineCourseModel> result = await _onlineCourseService.GetOnlineCourseByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveOnlineCourse")]
        public async Task<IActionResult> Post(OnlineCourseAddDto onlineCourseAddDto)
        {
            ServiceResult<bool> result = await _onlineCourseService.CreateOnlineCourseAsync(onlineCourseAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateOnlineCourse")]
        public async Task<IActionResult> Post(UpdateOnlineCourseDto updateOnlineCourseDto)
        {
            ServiceResult<bool> result = await _onlineCourseService.UpdateOnlineCourseAsync(updateOnlineCourseDto.Id, updateOnlineCourseDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteOnlineCourse")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _onlineCourseService.DeleteOnlineCourseAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
