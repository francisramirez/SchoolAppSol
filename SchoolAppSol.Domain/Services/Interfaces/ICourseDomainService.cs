

namespace SchoolAppSol.Domain.Services.Interfaces
{
    public interface ICourseDomainService
    {
        Task<bool> CanDeleteCourseAsync(int courseId);
        Task<bool> ExistsByTitleAsync(string courseTitle);
    }
}
