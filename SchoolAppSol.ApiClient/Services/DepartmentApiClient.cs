using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SchoolAppSol.ApiClient.Interfaces;
using SchoolAppSol.Application.Base;
using SchoolAppSol.Application.Dtos.Department;
using SchoolAppSol.Domain.Models;

namespace SchoolAppSol.ApiClient.Services
{
    public class DepartmentApiClient : IDepartmentApiClient
    {
        private readonly HttpClient _httpClient;

        public DepartmentApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResult<List<DepartmentModel>>> GetAllDepartmentsAsync()
        {
            var response = await _httpClient.GetAsync("api/Department/GetDepartments");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult<List<DepartmentModel>>>();
                return result ?? new ServiceResult<List<DepartmentModel>> { Success = false, Message = "Deserialization failed." };
            }
            return new ServiceResult<List<DepartmentModel>> { Success = false, Message = $"HTTP Error: {response.StatusCode}" };
        }

        public async Task<ServiceResult<DepartmentModel>> GetDepartmentByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Department/GetDepartmentById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult<DepartmentModel>>();
                return result ?? new ServiceResult<DepartmentModel> { Success = false, Message = "Deserialization failed." };
            }
            return new ServiceResult<DepartmentModel> { Success = false, Message = $"HTTP Error: {response.StatusCode}" };
        }

        public async Task<ServiceResult<bool>> CreateDepartmentAsync(DepartmentAddDto createDepartmentDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Department/SaveDepartment", createDepartmentDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult<bool>>();
                return result ?? new ServiceResult<bool> { Success = false, Message = "Deserialization failed." };
            }
            return new ServiceResult<bool> { Success = false, Message = $"HTTP Error: {response.StatusCode}" };
        }

        public async Task<ServiceResult<bool>> UpdateDepartmentAsync(int id, UpdateDepartmentDto updateDepartmentDto)
        {
            var dtoWithId = updateDepartmentDto with { Id = id };
            var response = await _httpClient.PostAsJsonAsync("api/Department/UpdateDepartment", dtoWithId);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult<bool>>();
                return result ?? new ServiceResult<bool> { Success = false, Message = "Deserialization failed." };
            }
            return new ServiceResult<bool> { Success = false, Message = $"HTTP Error: {response.StatusCode}" };
        }

        public async Task<ServiceResult<bool>> DeleteDepartmentAsync(int id)
        {
            var response = await _httpClient.PostAsync($"api/Department/DeleteDepartment?id={id}", null);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ServiceResult<bool>>();
                return result ?? new ServiceResult<bool> { Success = false, Message = "Deserialization failed." };
            }
            return new ServiceResult<bool> { Success = false, Message = $"HTTP Error: {response.StatusCode}" };
        }
    }
}
