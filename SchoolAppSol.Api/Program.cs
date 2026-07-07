
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolAppSol.Application.Interfaces.Course;
using SchoolAppSol.Application.Interfaces.Department;
using SchoolAppSol.Application.Services.Course;
using SchoolAppSol.Application.Services.Department;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Infrastructure;
using SchoolAppSol.Persitence.Context;
using SchoolAppSol.Persitence.Repositories;
using System.Text;

namespace SchoolAppSol.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolDb")));

            // Register infrastructure services
           // builder.Services.AddInfrastructureServices();

            builder.Services.AddScoped<IDepartmentDomainRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentValidator, DepartmentValidator>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseDomainRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseValidator, CourseValidator>();
            builder.Services.AddScoped<ICourseService, CourseService>();


            //builder.Services.AddHttpClient();
            //builder.Services.AddScoped<IDepartmentApiClient, DepartmentApiClient>();


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
