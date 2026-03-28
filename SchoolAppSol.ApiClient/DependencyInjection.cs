using System;
using Microsoft.Extensions.DependencyInjection;
using SchoolAppSol.ApiClient.Interfaces;
using SchoolAppSol.ApiClient.Services;

namespace SchoolAppSol.ApiClient
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiClients(this IServiceCollection services, string baseUrl)
        {
            services.AddHttpClient<IDepartmentApiClient, DepartmentApiClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
