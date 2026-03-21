using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.CourseEnrollment;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Application.Interfaces.CourseEnrollment
{
    public interface ICourseEnrollmentService
    {
        Task<ServiceResult<List<CourseEnrollmentModel>>> GetAllCourseEnrollmentsAsync();
        Task<ServiceResult<CourseEnrollmentModel>> GetCourseEnrollmentByIdAsync(int id);
        Task<ServiceResult<List<CourseEnrollmentModel>>> GetCourseEnrollmentsByStudentIdAsync(int studentId);
        Task<ServiceResult<bool>> CreateCourseEnrollmentAsync(CourseEnrollmentAddDto createDto);
        Task<ServiceResult<bool>> UpdateCourseEnrollmentAsync(int id, UpdateCourseEnrollmentDto updateDto);
        Task<ServiceResult<bool>> DeleteCourseEnrollmentAsync(int id);
    }
}
