using EnglishVocab.API.Extensions;
using EnglishVocab.Application;
using EnglishVocab.Identity;
using EnglishVocab.Identity.Data;
using EnglishVocab.Identity.Data.Dbcontext;
using EnglishVocab.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using EnglishVocab.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add serilog with detailed configuration
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    .WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));

// Tối ưu hóa cấu hình
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/json" });
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

// Thêm CORS trực tiếp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Thêm caching
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

// Đăng ký các services từ các layers
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Tối ưu hóa EF Core

var app = builder.Build();

// Thêm middleware để log tất cả các requests
app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
});

// Thêm middleware xử lý ngoại lệ
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<NotFoundMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnglishVocab API v1"));
    app.UseDeveloperExceptionPage();
}
else
{
    // Chỉ sử dụng HTTPS Redirection trong môi trường production
    app.UseHsts();
    // Use custom exception handling middleware
    app.UseApiServices();
}

// Luôn bật HTTPS Redirection để đảm bảo kết nối an toàn
app.UseHttpsRedirection();

// Sử dụng CORS trước UseRouting
app.UseCors("AllowAll");

// Sử dụng middleware nén phản hồi
app.UseResponseCompression();
app.UseResponseCaching();

// Thêm header cache control
app.Use(async (context, next) =>
{
    context.Response.GetTypedHeaders().CacheControl = 
        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        {
            Public = true,
            MaxAge = TimeSpan.FromSeconds(10)
        };
    await next();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Không cần gọi seed data bởi vì đã được cấu hình qua HasData trong các lớp Configuration

// Log khi ứng dụng khởi động
Log.Information("API đã khởi động tại: {time}", DateTime.Now);

app.Run();