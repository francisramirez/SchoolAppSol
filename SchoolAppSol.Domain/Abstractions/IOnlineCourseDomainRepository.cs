namespace SchoolAppSol.Domain.Abstractions
{
    public interface IOnlineCourseDomainRepository
    {
        Task<bool> ExistsForCourseAsync(int courseId, int? excludingOnlineCourseId, CancellationToken ct = default);
    }
}
