using Microsoft.Extensions.DependencyInjection;
using SchoolAppSol.Application.Interfaces.Base;
using SchoolAppSol.Application.Interfaces.Auth;
using SchoolAppSol.Infrastructure.Services;

using SchoolAppSol.Application.Interfaces.Course;
using SchoolAppSol.Application.Services.Course;
using SchoolAppSol.Application.Interfaces.Department;
using SchoolAppSol.Application.Services.Department;
using SchoolAppSol.Application.Interfaces.OnlineCourse;
using SchoolAppSol.Application.Services.OnlineCourse;
using SchoolAppSol.Application.Interfaces.OnsiteCourse;
using SchoolAppSol.Application.Services.OnsiteCourse;
using SchoolAppSol.Application.Interfaces.CourseEnrollment;
using SchoolAppSol.Application.Services.CourseEnrollment;
using SchoolAppSol.Application.Interfaces.Student;
using SchoolAppSol.Application.Services.Student;
using SchoolAppSol.Application.Services.Auth;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Repositories;

namespace SchoolAppSol.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<ITokenService, TokenService>();

        services.AddCourseDependency();
        services.AddDepartmentDependency();
        services.AddOnlineCourseDependency();
        services.AddOnsiteCourseDependency();
        services.AddCourseEnrollmentDependency();
        services.AddStudentDependency();
        services.AddAuthDependency();

        return services;
    }

    public static IServiceCollection AddCourseDependency(this IServiceCollection services)
    {
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseDomainRepository, CourseRepository>();
        services.AddScoped<ICourseValidator, CourseValidator>();            
        services.AddScoped<ICourseService, CourseService>();
        return services;
    }

    public static IServiceCollection AddDepartmentDependency(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentDomainRepository, DepartmentRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDepartmentValidator, DepartmentValidator>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        return services;
    }

    public static IServiceCollection AddOnlineCourseDependency(this IServiceCollection services)
    {
        services.AddScoped<IOnlineCourseRepository, OnlineCourseRepository>();
        services.AddScoped<IOnlineCourseDomainRepository, OnlineCourseRepository>();
        services.AddScoped<IOnlineCourseValidator, OnlineCourseValidator>();
        services.AddScoped<IOnlineCourseService, OnlineCourseService>();
        return services;
    }

    public static IServiceCollection AddOnsiteCourseDependency(this IServiceCollection services)
    {
        services.AddScoped<IOnsiteCourseRepository, OnsiteCourseRepository>();
        services.AddScoped<IOnsiteCourseDomainRepository, OnsiteCourseRepository>();
        services.AddScoped<IOnsiteCourseValidator, OnsiteCourseValidator>();
        services.AddScoped<IOnsiteCourseService, OnsiteCourseService>();
        return services;
    }

    public static IServiceCollection AddCourseEnrollmentDependency(this IServiceCollection services)
    {
        services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
        services.AddScoped<ICourseEnrollmentDomainRepository, CourseEnrollmentRepository>();
        services.AddScoped<ICourseEnrollmentValidator, CourseEnrollmentValidator>();
        services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();
        return services;
    }

    public static IServiceCollection AddStudentDependency(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentDomainRepository, StudentRepository>();
        services.AddScoped<IStudentValidator, StudentValidator>();
        services.AddScoped<IStudentService, StudentService>();
        return services;
    }

    public static IServiceCollection AddAuthDependency(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserDomainRepository, UserRepository>();
        services.AddScoped<IUserValidator, UserValidator>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
