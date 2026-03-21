using Microsoft.Extensions.Logging;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Student;
using SchoolAppSol.Application.Interfaces.Student;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Application.Services.Student
{
    public sealed class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentValidator _studentValidator;
        private readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository,
                              IStudentValidator studentValidator,
                              ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _studentValidator = studentValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> CreateStudentAsync(StudentAddDto createDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting student creation process.");

            try
            {
                if (createDto == null)
                {
                    _logger.LogWarning("Student creation failed: DTO is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Student data is required.";
                    return serviceResult;
                }

                Domain.Entities.Student student = new Domain.Entities.Student
                {
                    FirstName = createDto.FirstName,
                    LastName = createDto.LastName,
                    EnrollmentDate = createDto.EnrollmentDate,
                    CreationUser = createDto.CreateUser,
                    CreationDate = createDto.CreationDate
                };

                await _studentValidator.ValidateForCreateAsync(student);
                _logger.LogInformation("Student validation successful for: {@student}", student);

                await _studentRepository.AddAsync(student);
                _logger.LogInformation("Student added to repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Student created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating student.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a student.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating the student.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteStudentAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting student deletion process for id: {id}", id);

            try
            {
                int userId = 1; // Assuming default system user for now 
                await _studentValidator.ValidateForDeleteAsync(id, userId);

                await _studentRepository.SoftDeleteAsync(id, userId);
                _logger.LogInformation("Student soft deleted successfully.");

                serviceResult.Success = true;
                serviceResult.Message = "Student deleted successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while deleting student.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while deleting student.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting student with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting the student.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<StudentModel>>> GetAllStudentsAsync()
        {
            ServiceResult<List<StudentModel>> result = new ServiceResult<List<StudentModel>>();

            try
            {
                var students = await _studentRepository.GetAllActiveAsync();
                result.Data = students.ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all students.");
                result.Success = false;
                result.Message = "Ocurrió un error al obtener los estudiantes.";
            }

            return result;
        }

        public async Task<ServiceResult<StudentModel>> GetStudentByIdAsync(int id)
        {
            ServiceResult<StudentModel> result = new ServiceResult<StudentModel>();

            try
            {
                if (id <= 0)
                {
                    result.Message = "El id del estudiante es inválido.";
                    result.Success = false;
                    return result;
                }

                var student = await _studentRepository.GetByIdAsync(id);

                if (student == null)
                {
                    result.Message = $"El estudiante con id:{id} no se encuentra registrado. ";
                    result.Success = false;
                    return result;
                }

                StudentModel studentModel = new StudentModel
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    EnrollmentDate = student.EnrollmentDate
                };

                result.Success = true;
                result.Data = studentModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting student by id.");
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo el estudiante.";
            }

            return result;
        }

        public async Task<ServiceResult<bool>> UpdateStudentAsync(int id, UpdateStudentDto updateDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting student update process for id: {id}", id);

            try
            {
                if (updateDto == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Student data is required.";
                    return serviceResult;
                }

                if (id != updateDto.StudentId)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "El id proporcionado no coincide con el del estudiante a actualizar.";
                    return serviceResult;
                }

                Domain.Entities.Student student = new Domain.Entities.Student
                {
                    StudentId = updateDto.StudentId,
                    FirstName = updateDto.FirstName,
                    LastName = updateDto.LastName,
                    EnrollmentDate = updateDto.EnrollmentDate,
                    UserMod = updateDto.UpdateUser,
                    ModifyDate = updateDto.UpdateDate
                };

                await _studentValidator.ValidateForUpdateAsync(student);
                _logger.LogInformation("Student validation successful for update: {@student}", student);

                await _studentRepository.UpdateAsync(student);
                _logger.LogInformation("Student updated in repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Student updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating student.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while updating student.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating student with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating the student.";
            }

            return serviceResult;
        }
    }
}
