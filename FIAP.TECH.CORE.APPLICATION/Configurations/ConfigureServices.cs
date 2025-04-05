using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.APPLICATION.Services.Doctors;
using FIAP.TECH.CORE.APPLICATION.Services.Patients;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;

namespace FIAP.TECH.CORE.APPLICATION.Configurations;

public static class ConfigureServices
{
    public static void AddInjectionApplication(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Repositories
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();

        // Services
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IScheduleService, ScheduleService>();


        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });
    }
}
