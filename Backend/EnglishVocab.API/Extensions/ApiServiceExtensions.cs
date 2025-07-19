using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using EnglishVocab.API.Middleware;
using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;

namespace EnglishVocab.API.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
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
                // Xử lý tham chiếu vòng tròn
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            // Đăng ký API Explorer
            services.AddEndpointsApiExplorer();

            // Đăng ký Swagger với tối ưu hiệu suất
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnglishVocab API", Version = "v1" });
                
                // Cấu hình xác thực JWT cho Swagger - chấp nhận token trực tiếp
                c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    Description = "Nhập token JWT trực tiếp không cần thêm Bearer. Hệ thống sẽ tự nhận diện token.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // Vẫn giữ scheme bearer cho swagger UI
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
                                Id = "JWT"
                            }
                        },
                        new List<string>()
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
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            
            // Tối ưu hiệu suất API
            services.Configure<MvcOptions>(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Trường này là bắt buộc.");
            });

            // Add health checks
            services.AddHealthChecks();

            return services;
        }

        public static IApplicationBuilder UseApiServices(this IApplicationBuilder app)
        {
            // Use custom exception handling middleware
            app.UseMiddleware<ExceptionMiddleware>();

            // Use Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishVocab API V1");
                c.RoutePrefix = "swagger";
            });

            return app;
        }
    }
} 