using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace EnglishVocab.API.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Đăng ký Controllers với tối ưu hiệu suất
            services.AddControllers(options =>
            {
                // Cấu hình caching
                options.CacheProfiles.Add("Default30",
                    new CacheProfile
                    {
                        Duration = 30,
                        Location = ResponseCacheLocation.Any
                    });
            })
            .AddJsonOptions(options =>
            {
                // Tối ưu hiệu suất JSON serialization
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = false;
            });

            // Đăng ký API Explorer
            services.AddEndpointsApiExplorer();

            // Đăng ký Swagger với tối ưu hiệu suất
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "EnglishVocab API", 
                    Version = "v1",
                    Description = "API cho ứng dụng học từ vựng tiếng Anh",
                    Contact = new OpenApiContact
                    {
                        Name = "EnglishVocab Team",
                        Email = "contact@englishvocab.com",
                    }
                });
                
                // Cấu hình Swagger để sử dụng JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                // Thêm XML Comments cho Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
                
                // Tối ưu hiệu suất Swagger
                c.UseInlineDefinitionsForEnums();
                c.CustomSchemaIds(type => type.FullName);
            });

            // Đăng ký CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            
            // Tối ưu hiệu suất API
            services.Configure<MvcOptions>(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Trường này là bắt buộc.");
            });

            return services;
        }
    }
} 