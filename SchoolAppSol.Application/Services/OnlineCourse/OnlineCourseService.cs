using Microsoft.Extensions.Logging;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.OnlineCourse;
using SchoolAppSol.Application.Interfaces.OnlineCourse;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Application.Services.OnlineCourse
{
    public sealed class OnlineCourseService : IOnlineCourseService
    {
        private readonly IOnlineCourseRepository _onlineCourseRepository;
        private readonly IOnlineCourseValidator _onlineCourseValidator;
        private readonly ILogger<OnlineCourseService> _logger;

        public OnlineCourseService(IOnlineCourseRepository onlineCourseRepository,
                                   IOnlineCourseValidator onlineCourseValidator,
                                   ILogger<OnlineCourseService> logger)
        {
            _onlineCourseRepository = onlineCourseRepository;
            _onlineCourseValidator = onlineCourseValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> CreateOnlineCourseAsync(OnlineCourseAddDto createOnlineCourseDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting online course creation process.");

            try
            {
                if (createOnlineCourseDto == null)
                {
                    _logger.LogWarning("Online course creation failed: OnlineCourseAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Online course data is required.";
                    return serviceResult;
                }

                Domain.Entities.OnlineCourse onlineCourse = new Domain.Entities.OnlineCourse
                {
                    CourseId = createOnlineCourseDto.CourseId,
                    Url = createOnlineCourseDto.Url
                };

                await _onlineCourseValidator.ValidateForCreateAsync(onlineCourse);
                _logger.LogInformation("Online course validation successful for: {@onlineCourse}", onlineCourse);

                await _onlineCourseRepository.AddAsync(onlineCourse);
                _logger.LogInformation("Online course added to repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Online course created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating an online course.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an online course.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating an online course.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteOnlineCourseAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting online course deletion process for id: {id}", id);

            try
            {
                int userId = 1; // Assuming default system user for now 
                await _onlineCourseValidator.ValidateForDeleteAsync(id, userId);

                await _onlineCourseRepository.SoftDeleteAsync(id, userId);
                _logger.LogInformation("Online course deleted successfully.");

                serviceResult.Success = true;
                serviceResult.Message = "Online course deleted successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while deleting online course.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while deleting online course.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting online course with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting the online course.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<OnlineCourseModel>>> GetAllOnlineCoursesAsync()
        {
            ServiceResult<List<OnlineCourseModel>> result = new ServiceResult<List<OnlineCourseModel>>();

            try
            {
                var courses = await _onlineCourseRepository.GetAllAsync();
                var models = courses.Select(c => new OnlineCourseModel
                {
                    CourseId = c.CourseId,
                    Url = c.Url
                }).ToList();

                result.Data = models;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all online courses.");
                result.Success = false;
                result.Message = "Ocurrió un error al obtener los cursos en línea.";
            }

            return result;
        }

        public async Task<ServiceResult<OnlineCourseModel>> GetOnlineCourseByIdAsync(int id)
        {
            ServiceResult<OnlineCourseModel> result = new ServiceResult<OnlineCourseModel>();

            try
            {
                if (id <= 0)
                {
                    result.Message = "El id del curso es inválido.";
                    result.Success = false;
                    return result;
                }

                var onlineCourseModel = await _onlineCourseRepository.GetByCourseIdAsync(id);

                if (onlineCourseModel == null)
                {
                    result.Message = $"El curso en línea con id:{id} no se encuentra registrado. ";
                    result.Success = false;
                    return result;
                }

                result.Success = true;
                result.Data = onlineCourseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting online course by id.");
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo el curso en línea.";
            }

            return result;
        }

        public async Task<ServiceResult<bool>> UpdateOnlineCourseAsync(int id, UpdateOnlineCourseDto updateOnlineCourseDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting online course update process for id: {id}", id);

            try
            {
                if (updateOnlineCourseDto == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Online course data is required.";
                    return serviceResult;
                }

                if (id != updateOnlineCourseDto.Id)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "El id proporcionado no coincide con el del curso en línea a actualizar.";
                    return serviceResult;
                }

                Domain.Entities.OnlineCourse onlineCourse = new Domain.Entities.OnlineCourse
                {
                    Id = updateOnlineCourseDto.Id,
                    CourseId = updateOnlineCourseDto.CourseId,
                    Url = updateOnlineCourseDto.Url
                };

                await _onlineCourseValidator.ValidateForUpdateAsync(onlineCourse);
                _logger.LogInformation("Online course validation successful for update: {@onlineCourse}", onlineCourse);

                await _onlineCourseRepository.UpdateAsync(onlineCourse);
                _logger.LogInformation("Online course updated in repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Online course updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating online course.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while updating online course.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating online course with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating the online course.";
            }

            return serviceResult;
        }
    }
}
