
namespace SchoolAppSol.Domain.Services.Interfaces
{
    public interface IDepartmentDomainService
    {
        Task<bool> CanDeleteDepartmentAsync(int departmentId);
        Task<bool> ExistsByNameAsyng(string departmentName);
    }
}
