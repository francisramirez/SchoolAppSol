

namespace SchoolAppSol.Domain.Abstractions
{
    public interface ICourseDomainRepository
    {
        Task<bool> ExistsActiveAsync(int courseId, CancellationToken ct = default);
        Task<bool> TitleExistsInDepartmentAsync(string title, 
                                                int departmentId, 
                                                int? excludingCourseId, 
                                                CancellationToken ct = default);

    }
}
