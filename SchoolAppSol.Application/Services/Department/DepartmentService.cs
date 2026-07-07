using Microsoft.Extensions.Logging;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Department;
using SchoolAppSol.Application.Interfaces.Department;
using SchoolAppSol.Domain.Common;
using SchoolAppSol.Domain.Models;
using SchoolAppSol.Domain.Repository;
using SchoolAppSol.Domain.Validators.Interfaces;
using SchoolAppSol.Persitence.Exceptions;

namespace SchoolAppSol.Application.Services.Department
{
    public sealed class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentValidator _departmentValidator;
        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService(IDepartmentRepository departmentRepository,
                                 IDepartmentValidator departmentValidator,
                                 ILogger<DepartmentService> logger)
        {
            _departmentRepository = departmentRepository;
            _departmentValidator = departmentValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> CreateDepartmentAsync(DepartmentAddDto createDepartmentDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting department creation process.");

            try
            {
                if (createDepartmentDto == null)
                {
                    _logger.LogWarning("Department creation failed: DepartmentAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Department data is required.";
                    return serviceResult;
                }

                Domain.Entities.Department department = new Domain.Entities.Department
                {
                    Name = createDepartmentDto.Name,
                    Budget = createDepartmentDto.Budget,
                    StartDate = createDepartmentDto.StartDate,
                    Administrator = createDepartmentDto.Administrator,
                    CreationUser = createDepartmentDto.CreateUser,
                    CreationDate = createDepartmentDto.CreationDate
                };

                await _departmentValidator.ValidateForCreateAsync(department);
                _logger.LogInformation("Department validation successful for: {@department}", department);

                await _departmentRepository.AddAsync(department);
                _logger.LogInformation("Department added to repository successfully.");

                var departmentAdded = department.DepartmentId;

                serviceResult.Success = true;
                serviceResult.Message = "Department created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a department.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a department.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a department.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteDepartmentAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting department deletion process for id: {id}", id);

            try
            {
                int userId = 1; // Assuming default system user for now 
                await _departmentValidator.ValidateForDeleteAsync(id, userId);

                await _departmentRepository.SoftDeleteAsync(id, userId);
                _logger.LogInformation("Department soft deleted successfully.");

                serviceResult.Success = true;
                serviceResult.Message = "Department deleted successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while deleting department.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while deleting department.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting department with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting the department.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<DepartmentModel>>> GetAllDepartmentsAsync()
        {
            ServiceResult<List<DepartmentModel>> result = new ServiceResult<List<DepartmentModel>>();

            try
            {
                var departments = await _departmentRepository.GetAllActiveAsync();
                result.Data = departments.ToList();
                result.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all departments.");
                result.Success = false;
                result.Message = "Ocurrió un error al obtener los departamentos.";
            }

            return result;
        }

        public async Task<ServiceResult<DepartmentModel>> GetDepartmentByIdAsync(int id)
        {
            ServiceResult<DepartmentModel> result = new ServiceResult<DepartmentModel>();

            try
            {
                if (id <= 0)
                {
                    result.Message = "El id del departamento es inválido.";
                    result.Success = false;
                    return result;
                }

                Domain.Entities.Department? department = await _departmentRepository.GetByIdAsync(id);

                if (department == null)
                {
                    result.Message = $"El departamento con id:{id} no se encuentra registrado. ";
                    result.Success = false;
                    return result;
                }

                DepartmentModel departmentModel = new DepartmentModel()
                {
                    DepartmentId = department.DepartmentId,
                    Name = department.Name,
                    Budget = department.Budget,
                    StartDate = department.StartDate
                };

                result.Success = true;
                result.Data = departmentModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting department by id.");
                result.Success = false;
                result.Message = "Ocurrió un error obteniendo el departamento.";
            }

            return result;
        }

        public async Task<ServiceResult<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            _logger.LogInformation("Starting department update process for id: {id}", id);

            try
            {
                if (updateDepartmentDto == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Department data is required.";
                    return serviceResult;
                }

                if (id != updateDepartmentDto.Id)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "El id proporcionado no coincide con el del departamento a actualizar.";
                    return serviceResult;
                }

                Domain.Entities.Department department = new Domain.Entities.Department
                {
                    DepartmentId = updateDepartmentDto.Id,
                    Name = updateDepartmentDto.Name,
                    Budget = updateDepartmentDto.Budget,
                    StartDate = updateDepartmentDto.StartDate,
                    Administrator = updateDepartmentDto.Administrator,
                    UserMod = updateDepartmentDto.UpdateUser,
                    ModifyDate = updateDepartmentDto.UpdateDate
                };

                await _departmentValidator.ValidateForUpdateAsync(department);
                _logger.LogInformation("Department validation successful for update: {@department}", department);

                await _departmentRepository.UpdateAsync(department);
                _logger.LogInformation("Department updated in repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Department updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating department.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
            }
            catch (PersistenceException pex)
            {
                _logger.LogWarning(pex, "Persistence validation failed while updating department.");
                serviceResult.Success = false;
                serviceResult.Message = pex.Message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating department with id: {id}", id);
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating the department.";
            }

            return serviceResult;
        }
    }
}
