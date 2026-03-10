using Microsoft.Extensions.Logging;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Course;
using SchoolAppSol.Application.Interfaces.Course;
using SchoolAppSol.Domain.Abstractions;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Application.Services.Course
{
    public sealed class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseValidator _courseValidator;
        private readonly ICourseDomainRepository _courseDomainRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository,
                             ICourseDomainRepository courseDomainRepository,
                             ICourseValidator courseValidator,
                             ILogger<CourseService> logger)
        {
            _courseRepository = courseRepository;
            _courseDomainRepository = courseDomainRepository;
            _logger = logger;
        }
        public async Task<ServiceResult<bool>> CreateCourseAsync(CourseAddDto createCourseDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();


            _logger.LogInformation("Starting course creation process.");


            try
            {
                if (createCourseDto == null)
                {
                    _logger.LogWarning("Course creation failed: CourseAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Course data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }

                Domain.Entities.Course course = new Domain.Entities.Course
                {
                    Title = createCourseDto.Title,
                    Credits = createCourseDto.Credits,
                    DepartmentId = createCourseDto.DepartmentId,
                    CourseId = createCourseDto.CreateUser,
                    CreationDate = createCourseDto.CreationDate
                };

                _logger.LogInformation("Course validation successful for: {@course}", course);

                // Save the course using repository
                await _courseRepository.AddAsync(course);
                _logger.LogInformation("Course added to repository successfully: {@course}", course);
                serviceResult.Success = true;
                serviceResult.Message = "Course created successfully.";
                serviceResult.Data = true;

            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a course.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while creating a course.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a course.";
                serviceResult.Data = false;

            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteCourseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<CourseModel>>> GetAllCoursesAsync()
        {
            ServiceResult<List<CourseModel>> result = new ServiceResult<List<CourseModel>>();

            try
            {
                var courses = await _courseRepository.GetCoursesAsync();

                result.Data = courses.ToList();
                result.Success = true;
            }
            catch (DomainException dex)
            {
                _logger.LogError(dex.Message, dex.ToString());
                result.Success = false;
                result.Message = dex.Message;
                return result;
            }
            catch (PersistenceException pex)
            {
                _logger.LogError(pex.Message, pex.ToString());
                result.Success = false;
                result.Message = pex.Message;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.ToString());
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }

            return result;
        }

        public async Task<ServiceResult<CourseModel>> GetCourseByIdAsync(int id)
        {
            ServiceResult<CourseModel> result = new ServiceResult<CourseModel>();

            try
            {
                if (id <= 0)
                {
                    result.Message = "El id del curso es inváido.";
                    result.Success = false;
                    return result;
                }

                Domain.Entities.Course? course = await _courseRepository.GetByIdAsync(id);

                if (course == null) 
                {
                    result.Message = $"El curso con el id:{id} no se encuentra registrado. ";
                    result.Success = false;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Ocurrió un error obteniendo el curso.";
                result.Success = false;
                _logger.LogError(result.Message, ex.ToString());
            }

            return result;
        }

        public Task<ServiceResult<bool>> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
        {
            throw new NotImplementedException();
        }
    }
}
