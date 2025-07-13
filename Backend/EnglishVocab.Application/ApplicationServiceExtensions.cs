using EnglishVocab.Application.Common.Behaviors;
using EnglishVocab.Application.Common.Interfaces;
using EnglishVocab.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using AutoMapper;

namespace EnglishVocab.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Add Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            // Add Pipeline Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register Services
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<IStudyService, StudyService>();
            services.AddScoped<ISpacedRepetitionService, SpacedRepetitionService>();
            services.AddScoped<IDataTableService, DataTableService>();

            return services;
        }
    }
}