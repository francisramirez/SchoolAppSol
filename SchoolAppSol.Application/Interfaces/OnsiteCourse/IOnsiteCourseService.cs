using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.OnsiteCourse;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Application.Interfaces.OnsiteCourse
{
    public interface IOnsiteCourseService
    {
        Task<ServiceResult<List<OnsiteCourseModel>>> GetAllOnsiteCoursesAsync();
        Task<ServiceResult<OnsiteCourseModel>> GetOnsiteCourseByIdAsync(int id);
        Task<ServiceResult<List<OnsiteCourseModel>>> SearchByLocationAsync(string location);
        Task<ServiceResult<bool>> CreateOnsiteCourseAsync(OnsiteCourseAddDto createOnsiteCourseDto);
        Task<ServiceResult<bool>> UpdateOnsiteCourseAsync(int id, UpdateOnsiteCourseDto updateOnsiteCourseDto);
        Task<ServiceResult<bool>> DeleteOnsiteCourseAsync(int id);
    }
}
