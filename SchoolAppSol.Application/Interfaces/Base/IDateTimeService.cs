namespace SchoolAppSol.Application.Interfaces.Base;

public interface IDateTimeService
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
