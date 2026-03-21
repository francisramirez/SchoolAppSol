
using Microsoft.EntityFrameworkCore;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Context;
using SchoolAppSol.Persitence.Repositories;
using SchoolAppSol.Infrastructure;

namespace SchoolAppSol.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDb")));

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseDomainRepository, CourseRepository>();
            builder.Services.AddScoped<IDepartmentDomainRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IOnsiteCourseRepository, OnsiteCourseRepository>();
            builder.Services.AddScoped<IOnsiteCourseDomainRepository, OnsiteCourseRepository>();
            builder.Services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
            builder.Services.AddScoped<ICourseEnrollmentDomainRepository, CourseEnrollmentRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IStudentDomainRepository, StudentRepository>();

            builder.Services.AddScoped<ICourseValidator, CourseValidator>();            
            builder.Services.AddScoped<IDepartmentValidator, DepartmentValidator>();
            builder.Services.AddScoped<IOnlineCourseValidator, OnlineCourseValidator>();
            builder.Services.AddScoped<IOnsiteCourseValidator, OnsiteCourseValidator>();
            builder.Services.AddScoped<ICourseEnrollmentValidator, CourseEnrollmentValidator>();
            builder.Services.AddScoped<IStudentValidator, StudentValidator>();

            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IOnlineCourseService, OnlineCourseService>();
            builder.Services.AddScoped<IOnsiteCourseService, OnsiteCourseService>();
            builder.Services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            
            builder.Services.AddScoped<IOnsiteCourseRepository, OnsiteCourseRepository>();
            builder.Services.AddScoped<IOnsiteCourseDomainRepository, OnsiteCourseRepository>();

            builder.Services.AddScoped<IOnlineCourseRepository, OnlineCourseRepository>();
            builder.Services.AddScoped<IOnlineCourseDomainRepository, OnlineCourseRepository>();

            // Register infrastructure services
            builder.Services.AddInfrastructureServices();

            // Configure JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = false, 
                        // IMPORTANT: For production, you must validate issuer, audience and set a secure IssuerSigningKey
                        // Example:
                        // ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        // ValidAudience = builder.Configuration["Jwt:Audience"],
                        // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                    };
                });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
