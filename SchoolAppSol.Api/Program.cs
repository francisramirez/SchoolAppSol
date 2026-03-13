
using Microsoft.EntityFrameworkCore;
using SchoolAppSol.Application.Interfaces.Course;
using SchoolAppSol.Application.Services.Course;
using SchoolAppSol.Application.Interfaces.Department;
using SchoolAppSol.Application.Services.Department;
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
            builder.Services.AddScoped<ICourseValidator, CourseValidator>();
            builder.Services.AddScoped<IDepartmentValidator, DepartmentValidator>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            
            // Register infrastructure services
            builder.Services.AddInfrastructureServices();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
