
namespace SchoolAppSol.Domain.Abstractions
{
    public interface IOnsiteCourseDomainRepository
    {
        Task<bool> ExistsForCourseAsync(int courseId, int? excludingOnsiteCourseId, CancellationToken ct = default);

    }
}
