using SchoolAppSol.Domain.Base;

namespace SchoolAppSol.Domain.Entities
{
    public class User : AuditableEntity<int>
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
