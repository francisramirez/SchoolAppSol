
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Course;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Application.Interfaces.Course
{
    public interface ICourseService
    {
        Task<ServiceResult<List<CourseModel>>> GetAllCoursesAsync();
        Task<ServiceResult<CourseModel>> GetCourseByIdAsync(int id);
        Task<ServiceResult<bool>> CreateCourseAsync(CourseAddDto createCourseDto);
        Task<ServiceResult<bool>> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto);
        Task<ServiceResult<bool>> DeleteCourseAsync(int id);
    }
}
