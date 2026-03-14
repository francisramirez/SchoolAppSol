using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.OnlineCourse;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Application.Interfaces.OnlineCourse
{
    public interface IOnlineCourseService
    {
        Task<ServiceResult<List<OnlineCourseModel>>> GetAllOnlineCoursesAsync();
        Task<ServiceResult<OnlineCourseModel>> GetOnlineCourseByIdAsync(int id);
        Task<ServiceResult<bool>> CreateOnlineCourseAsync(OnlineCourseAddDto createOnlineCourseDto);
        Task<ServiceResult<bool>> UpdateOnlineCourseAsync(int id, UpdateOnlineCourseDto updateOnlineCourseDto);
        Task<ServiceResult<bool>> DeleteOnlineCourseAsync(int id);
    }
}
