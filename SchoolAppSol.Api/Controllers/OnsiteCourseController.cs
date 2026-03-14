using Microsoft.AspNetCore.Mvc;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.OnsiteCourse;
using SchoolAppSol.Application.Interfaces.OnsiteCourse;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnsiteCourseController : ControllerBase
    {
        private readonly IOnsiteCourseService _onsiteCourseService;

        public OnsiteCourseController(IOnsiteCourseService onsiteCourseService)
        {
            _onsiteCourseService = onsiteCourseService;
        }

        [HttpGet("GetOnsiteCourses")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<OnsiteCourseModel>> result = await _onsiteCourseService.GetAllOnsiteCoursesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetOnsiteCourseById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<OnsiteCourseModel> result = await _onsiteCourseService.GetOnsiteCourseByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("SearchByLocation")]
        public async Task<IActionResult> SearchByLocation(string location)
        {
            ServiceResult<List<OnsiteCourseModel>> result = await _onsiteCourseService.SearchByLocationAsync(location);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveOnsiteCourse")]
        public async Task<IActionResult> Post(OnsiteCourseAddDto onsiteCourseAddDto)
        {
            ServiceResult<bool> result = await _onsiteCourseService.CreateOnsiteCourseAsync(onsiteCourseAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateOnsiteCourse")]
        public async Task<IActionResult> Post(UpdateOnsiteCourseDto updateOnsiteCourseDto)
        {
            ServiceResult<bool> result = await _onsiteCourseService.UpdateOnsiteCourseAsync(updateOnsiteCourseDto.Id, updateOnsiteCourseDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteOnsiteCourse")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _onsiteCourseService.DeleteOnsiteCourseAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
