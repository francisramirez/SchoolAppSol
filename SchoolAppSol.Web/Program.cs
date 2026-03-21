using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Application.Interfaces.Course;
using SchoolAppSol.Application.Interfaces.Department;
using SchoolAppSol.Application.Interfaces.OnlineCourse;
using SchoolAppSol.Application.Interfaces.OnsiteCourse;
using SchoolAppSol.Application.Services.Course;
using SchoolAppSol.Application.Services.Department;
using SchoolAppSol.Application.Services.OnlineCourse;
using SchoolAppSol.Application.Services.OnsiteCourse;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Context;
using SchoolAppSol.Persitence.Repositories;

namespace SchoolAppSol.Web
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
            builder.Services.AddScoped<ICourseValidator, CourseValidator>();
            builder.Services.AddScoped<IDepartmentValidator, DepartmentValidator>();
            builder.Services.AddScoped<IOnlineCourseValidator, OnlineCourseValidator>();
            builder.Services.AddScoped<IOnsiteCourseValidator, OnsiteCourseValidator>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IOnlineCourseService, OnlineCourseService>();
            builder.Services.AddScoped<IOnsiteCourseService, OnsiteCourseService>();
            builder.Services.AddScoped<IOnsiteCourseRepository, OnsiteCourseRepository>();
            builder.Services.AddScoped<IOnsiteCourseDomainRepository, OnsiteCourseRepository>();
            builder.Services.AddScoped<IOnlineCourseRepository, OnlineCourseRepository>();
            builder.Services.AddScoped<IOnlineCourseDomainRepository, OnlineCourseRepository>();


            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
