using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Infrastructure.DatabaseContext;
using EnglishVocab.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using System;

namespace EnglishVocab.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký DbContext cho Infrastructure với tối ưu hiệu suất
            services.AddDbContext<EnglishVocabDatabaseContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("EnglishVocabConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(EnglishVocabDatabaseContext).Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                        sqlOptions.CommandTimeout(30);
                    });

                // Tối ưu hiệu suất EF Core
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                
                // Bật tính năng batch để giảm số lượng truy vấn SQL
                options.EnableSensitiveDataLogging(false);
            });

            // Đăng ký IApplicationDbContext
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EnglishVocabDatabaseContext>());

            // Đăng ký các repository
            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<IWordSetRepository, WordSetRepository>();
            services.AddScoped<IStudySessionRepository, StudySessionRepository>();
            services.AddScoped<IUserProgressRepository, UserProgressRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IUserStatisticsRepository, UserStatisticsRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDifficultyLevelRepository, DifficultyLevelRepository>();

            // Đăng ký AutoMapper profile
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
} 