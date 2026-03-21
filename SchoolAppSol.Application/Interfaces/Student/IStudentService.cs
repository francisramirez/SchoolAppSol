using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Student;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.Application.Interfaces.Student
{
    public interface IStudentService
    {
        Task<ServiceResult<List<StudentModel>>> GetAllStudentsAsync();
        Task<ServiceResult<StudentModel>> GetStudentByIdAsync(int id);
        Task<ServiceResult<bool>> CreateStudentAsync(StudentAddDto createDto);
        Task<ServiceResult<bool>> UpdateStudentAsync(int id, UpdateStudentDto updateDto);
        Task<ServiceResult<bool>> DeleteStudentAsync(int id);
    }
}
