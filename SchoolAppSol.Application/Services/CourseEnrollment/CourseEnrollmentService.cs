using Microsoft.Extensions.Logging;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.CourseEnrollment;
using SchoolAppSol.Application.Interfaces.CourseEnrollment;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Entities;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Application.Services.CourseEnrollment
{
    public sealed class CourseEnrollmentService : ICourseEnrollmentService
    {
        private readonly ICourseEnrollmentRepository _enrollmentRepository;
        private readonly ICourseEnrollmentValidator _enrollmentValidator;
        private readonly ILogger<CourseEnrollmentService> _logger;

        public CourseEnrollmentService(ICourseEnrollmentRepository enrollmentRepository,
                                       ICourseEnrollmentValidator enrollmentValidator,
                                       ILogger<CourseEnrollmentService> logger)
        {
            _enrollmentRepository = enrollmentRepository;
            _enrollmentValidator = enrollmentValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> CreateCourseEnrollmentAsync(CourseEnrollmentAddDto createDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting course enrollment creation process.");

            try
            {
                if (createDto == null)
                {
                    _logger.LogWarning("Course enrollment creation failed: DTO is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Enrollment data is required.";
                    return serviceResult;
                }

                Domain.Entities.CourseEnrollment enrollment = new Domain.Entities.CourseEnrollment
                {
                    CourseId = createDto.CourseId,
                    StudentId = createDto.StudentId,
                    EnrollmentDate = createDto.EnrollmentDate,
                    EnrollmentStatusId = createDto.EnrollmentStatusId,
                    CreationUser = createDto.CreateUser,
                    CreationDate = createDto.CreationDate
                };

                await _enrollmentValidator.ValidateForCreateAsync(enrollment);
                _logger.LogInformation("Enrollment validation successful for: {@enrollment}", enrollment);

                await _enrollmentRepository.AddAsync(enrollment);
                _logger.LogInformation("Enrollment added to repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Course enrollment created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating enrollment.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a course enrollment.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating an enrollment.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteCourseEnrollmentAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting course enrollment deletion process for id: {id}", id);

            try
            {
                int userId = 1; // Assuming default system user for now 
                await _enrollmentValidator.ValidateForDeleteAsync(id, userId);

                await _enrollmentRepository.SoftDeleteAsync(id, userId);
                _logger.LogInformation("Course enrollment soft deleted successfully.");

                serviceResult.Success = true;
                serviceResult.Message = "Course enrollment deleted successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while deleting enrollment.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while deleting enrollment.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting enrollment with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting the enrollment.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<CourseEnrollmentModel>>> GetAllCourseEnrollmentsAsync()
        {
            ServiceResult<List<CourseEnrollmentModel>> result = new ServiceResult<List<CourseEnrollmentModel>>();

            try
            {
                var enrollments = await _enrollmentRepository.GetAllActiveAsync();
                result.Data = enrollments.ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all enrollments.");
                result.Success = false;
                result.Message = "Ocurrió un error al obtener las inscripciones.";
            }

            return result;
        }

        public async Task<ServiceResult<CourseEnrollmentModel>> GetCourseEnrollmentByIdAsync(int id)
        {
            ServiceResult<CourseEnrollmentModel> result = new ServiceResult<CourseEnrollmentModel>();

            try
            {
                if (id <= 0)
                {
                    result.Message = "El id de inscripción es inválido.";
                    result.Success = false;
                    return result;
                }

                var enrollment = await _enrollmentRepository.GetByIdWithDetailsAsync(id);

                if (enrollment == null)
                {
                    result.Message = $"La inscripción con id:{id} no se encuentra registrada. ";
                    result.Success = false;
                    return result;
                }

                result.Success = true;
                result.Data = enrollment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting enrollment by id.");
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo la inscripción.";
            }

            return result;
        }

        public async Task<ServiceResult<List<CourseEnrollmentModel>>> GetCourseEnrollmentsByStudentIdAsync(int studentId)
        {
            ServiceResult<List<CourseEnrollmentModel>> result = new ServiceResult<List<CourseEnrollmentModel>>();

            try
            {
                if (studentId <= 0)
                {
                    result.Message = "El id del estudiante es inválido.";
                    result.Success = false;
                    return result;
                }

                var enrollments = await _enrollmentRepository.GetByStudentIdAsync(studentId);
                result.Data = enrollments.ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting enrollments by student id.");
                result.Success = false;
                result.Message = "Ocurrió un error buscando las inscripciones del estudiante.";
            }

            return result;
        }

        public async Task<ServiceResult<bool>> UpdateCourseEnrollmentAsync(int id, UpdateCourseEnrollmentDto updateDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting course enrollment update process for id: {id}", id);

            try
            {
                if (updateDto == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Enrollment data is required.";
                    return serviceResult;
                }

                if (id != updateDto.EnrollmentId)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "El id proporcionado no coincide con la inscripción a actualizar.";
                    return serviceResult;
                }

                Domain.Entities.CourseEnrollment enrollment = new Domain.Entities.CourseEnrollment
                {
                    EnrollmentId = updateDto.EnrollmentId,
                    CourseId = updateDto.CourseId,
                    StudentId = updateDto.StudentId,
                    EnrollmentDate = updateDto.EnrollmentDate,
                    EnrollmentStatusId = updateDto.EnrollmentStatusId,
                    UserMod = updateDto.UpdateUser,
                    ModifyDate = updateDto.UpdateDate
                };

                await _enrollmentValidator.ValidateForUpdateAsync(enrollment);
                _logger.LogInformation("Enrollment validation successful for update: {@enrollment}", enrollment);

                await _enrollmentRepository.UpdateAsync(enrollment);
                _logger.LogInformation("Enrollment updated in repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Course enrollment updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating enrollment.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while updating enrollment.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating enrollment with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating the enrollment.";
            }

            return serviceResult;
        }
    }
}
