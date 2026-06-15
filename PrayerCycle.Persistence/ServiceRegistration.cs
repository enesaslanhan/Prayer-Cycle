using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrayerCycle.Application.Common.Abstractions;
using PrayerCycle.Application.Families;
using PrayerCycle.Application.FamilyMembers;
using PrayerCycle.Application.PrayerLogs;
using PrayerCycle.Application.Users;
using PrayerCycle.Persistence.Context;
using PrayerCycle.Persistence.Families;
using PrayerCycle.Persistence.FamilyMembers;
using PrayerCycle.Persistence.PrayerLogs;
using PrayerCycle.Persistence.Users;

namespace PrayerCycle.Persistence;

public static class ServiceRegistration
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

        services.AddDbContext<PrayerCycleDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();
        services.AddScoped<IFamilyReadRepository, FamilyReadRepository>();
        services.AddScoped<IFamilyWriteRepository, FamilyWriteRepository>();
        services.AddScoped<IFamilyMemberReadRepository, FamilyMemberReadRepository>();
        services.AddScoped<IFamilyMemberWriteRepository, FamilyMemberWriteRepository>();
        services.AddScoped<IPrayerLogReadRepository, PrayerLogReadRepository>();
        services.AddScoped<IPrayerLogWriteRepository, PrayerLogWriteRepository>();

        return services;
    }
}
