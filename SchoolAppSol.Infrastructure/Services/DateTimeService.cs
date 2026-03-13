using SchoolAppSol.Application.Interfaces.Base;

namespace SchoolAppSol.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
