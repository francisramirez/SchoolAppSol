namespace SchoolAppSol.Domain.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // PasswordHash is intentionally excluded from the model to prevent accidental exposure
    }
}
