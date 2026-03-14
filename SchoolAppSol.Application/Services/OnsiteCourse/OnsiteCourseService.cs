using Microsoft.Extensions.Logging;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.OnsiteCourse;
using SchoolAppSol.Application.Interfaces.OnsiteCourse;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Application.Services.OnsiteCourse
{
    public sealed class OnsiteCourseService : IOnsiteCourseService
    {
        private readonly IOnsiteCourseRepository _onsiteCourseRepository;
        private readonly IOnsiteCourseValidator _onsiteCourseValidator;
        private readonly ILogger<OnsiteCourseService> _logger;

        public OnsiteCourseService(IOnsiteCourseRepository onsiteCourseRepository,
                                   IOnsiteCourseValidator onsiteCourseValidator,
                                   ILogger<OnsiteCourseService> logger)
        {
            _onsiteCourseRepository = onsiteCourseRepository;
            _onsiteCourseValidator = onsiteCourseValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> CreateOnsiteCourseAsync(OnsiteCourseAddDto createOnsiteCourseDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting onsite course creation process.");

            try
            {
                if (createOnsiteCourseDto == null)
                {
                    _logger.LogWarning("Onsite course creation failed: OnsiteCourseAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Onsite course data is required.";
                    return serviceResult;
                }

                Domain.Entities.OnsiteCourse onsiteCourse = new Domain.Entities.OnsiteCourse
                {
                    CourseId = createOnsiteCourseDto.CourseId,
                    Location = createOnsiteCourseDto.Location,
                    Days = createOnsiteCourseDto.Days,
                    Time = createOnsiteCourseDto.Time
                };

                await _onsiteCourseValidator.ValidateForCreateAsync(onsiteCourse);
                _logger.LogInformation("Onsite course validation successful for: {@onsiteCourse}", onsiteCourse);

                await _onsiteCourseRepository.AddAsync(onsiteCourse);
                _logger.LogInformation("Onsite course added to repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Onsite course created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating an onsite course.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an onsite course.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating an onsite course.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteOnsiteCourseAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting onsite course deletion process for id: {id}", id);

            try
            {
                int userId = 1; // Assuming default system user for now 
                await _onsiteCourseValidator.ValidateForDeleteAsync(id, userId);

                await _onsiteCourseRepository.SoftDeleteAsync(id, userId);
                _logger.LogInformation("Onsite course deleted successfully.");

                serviceResult.Success = true;
                serviceResult.Message = "Onsite course deleted successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while deleting onsite course.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while deleting onsite course.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting onsite course with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting the onsite course.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<OnsiteCourseModel>>> GetAllOnsiteCoursesAsync()
        {
            ServiceResult<List<OnsiteCourseModel>> result = new ServiceResult<List<OnsiteCourseModel>>();

            try
            {
                var courses = await _onsiteCourseRepository.GetAllAsync();
                var models = courses.Select(c => new OnsiteCourseModel
                {
                    CourseId = c.CourseId,
                    Location = c.Location,
                    Days = c.Days,
                    Time = c.Time
                }).ToList();

                result.Data = models;
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all onsite courses.");
                result.Success = false;
                result.Message = "Ocurrió un error al obtener los cursos presenciales.";
            }

            return result;
        }

        public async Task<ServiceResult<OnsiteCourseModel>> GetOnsiteCourseByIdAsync(int id)
        {
            ServiceResult<OnsiteCourseModel> result = new ServiceResult<OnsiteCourseModel>();

            try
            {
                if (id <= 0)
                {
                    result.Message = "El id del curso es inválido.";
                    result.Success = false;
                    return result;
                }

                var onsiteCourseModel = await _onsiteCourseRepository.GetByCourseIdAsync(id);

                if (onsiteCourseModel == null)
                {
                    result.Message = $"El curso presencial con id:{id} no se encuentra registrado. ";
                    result.Success = false;
                    return result;
                }

                result.Success = true;
                result.Data = onsiteCourseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting onsite course by id.");
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo el curso presencial.";
            }

            return result;
        }

        public async Task<ServiceResult<List<OnsiteCourseModel>>> SearchByLocationAsync(string location)
        {
            ServiceResult<List<OnsiteCourseModel>> result = new ServiceResult<List<OnsiteCourseModel>>();

            try
            {
                var courses = await _onsiteCourseRepository.SearchByLocationAsync(location);
                result.Data = courses.ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching onsite courses by location.");
                result.Success = false;
                result.Message = "Ocurrió un error buscando cursos presenciales por ubicación.";
            }

            return result;
        }

        public async Task<ServiceResult<bool>> UpdateOnsiteCourseAsync(int id, UpdateOnsiteCourseDto updateOnsiteCourseDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting onsite course update process for id: {id}", id);

            try
            {
                if (updateOnsiteCourseDto == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Onsite course data is required.";
                    return serviceResult;
                }

                if (id != updateOnsiteCourseDto.Id)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "El id proporcionado no coincide con el del curso presencial a actualizar.";
                    return serviceResult;
                }

                Domain.Entities.OnsiteCourse onsiteCourse = new Domain.Entities.OnsiteCourse
                {
                    Id = updateOnsiteCourseDto.Id,
                    CourseId = updateOnsiteCourseDto.CourseId,
                    Location = updateOnsiteCourseDto.Location,
                    Days = updateOnsiteCourseDto.Days,
                    Time = updateOnsiteCourseDto.Time
                };

                await _onsiteCourseValidator.ValidateForUpdateAsync(onsiteCourse);
                _logger.LogInformation("Onsite course validation successful for update: {@onsiteCourse}", onsiteCourse);

                await _onsiteCourseRepository.UpdateAsync(onsiteCourse);
                _logger.LogInformation("Onsite course updated in repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Onsite course updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating onsite course.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while updating onsite course.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating onsite course with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating the onsite course.";
            }

            return serviceResult;
        }
    }
}
