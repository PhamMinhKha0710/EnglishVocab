using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Infrastructure.DatabaseContext;
using EnglishVocab.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EnglishVocab.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database Context
            services.AddDbContext<EnglishVocabDatabaseContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("EnglishVocabConnection"),
                    b => b.MigrationsAssembly(typeof(EnglishVocabDatabaseContext).Assembly.FullName)));
            
            // Register Application Context Interface to Concrete Implementation
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EnglishVocabDatabaseContext>());

            // Repository Registrations
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDifficultyLevelRepository, DifficultyLevelRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IWordSetRepository, WordSetRepository>();
            services.AddScoped<IWordSetWordRepository, WordSetWordRepository>();
            services.AddScoped<IStudySessionRepository, StudySessionRepository>();
            services.AddScoped<IUserProgressRepository, UserProgressRepository>();
            services.AddScoped<IUserStatisticsRepository, UserStatisticsRepository>();

            return services;
        }
    }
} 