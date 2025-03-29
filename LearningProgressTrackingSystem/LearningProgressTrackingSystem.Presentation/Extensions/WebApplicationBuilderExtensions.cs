using FluentValidation;
using FluentValidation.AspNetCore;
using LearningProgressTrackingSystem.Application.Contracts.Identity;
using LearningProgressTrackingSystem.Application.Features.Account.Commands.Register;
using LearningProgressTrackingSystem.Application.Features.Account.Queries.GetAccountLogin;
using LearningProgressTrackingSystem.Application.Features.Account.Queries.LogIn;
using LearningProgressTrackingSystem.Application.Features.Account.Validators;
using LearningProgressTrackingSystem.Application.Features.Student.Queries.GetAllStudentCourses;
using LearningProgressTrackingSystem.Application.Features.Student.Queries.GetStudentMainPageData;
using LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetAllTeacherCourses;
using LearningProgressTrackingSystem.Application.Features.Teacher.Queries.GetTeacherMainPageData;
using LearningProgressTrackingSystem.Application.Options;
using LearningProgressTrackingSystem.Application.Services.Identity;
using LearningProgressTrackingSystem.Data;
using LearningProgressTrackingSystem.Data.Repositories;
using LearningProgressTrackingSystem.Domain.Contracts;
using LearningProgressTrackingSystem.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

namespace LearningProgressTrackingSystem.Presentation.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddDbContext(this IHostApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("Local");
        
        builder.Services.AddDbContext<LearningProgressTrackingSystemContext>(options =>
            options.UseNpgsql(connectionString));
        
        return builder;
    }
    
    public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
        builder.Services.AddScoped<IStudentRepository, StudentRepository>();
        builder.Services.AddScoped<ICourseRepository, CourseRepository>();

        return builder;
    }
    
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtBearerService, JwtBearerService>();
        
        return builder;
    }
    
    public static IHostApplicationBuilder AddIntegrationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPasswordHasher<AccountEntity>, PasswordHasher<AccountEntity>>();
        builder.Services.AddHttpContextAccessor();
        
        return builder;
    }

    public static IHostApplicationBuilder AddMediatR(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
            configuration.RegisterServicesFromAssembly(typeof(LogInQuery).Assembly);
            configuration.RegisterServicesFromAssembly(typeof(GetAccountLoginQuery).Assembly);
            configuration.RegisterServicesFromAssembly(typeof(GetAllStudentCoursesQuery).Assembly);
            configuration.RegisterServicesFromAssembly(typeof(GetAllTeacherCoursesQuery).Assembly);
            configuration.RegisterServicesFromAssembly(typeof(GetStudentMainPageDataQuery).Assembly);
            configuration.RegisterServicesFromAssembly(typeof(GetTeacherMainPageDataQuery).Assembly);
        });

        return builder;
    }

    public static IHostApplicationBuilder AddFluentValidation(this IHostApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<LogInQueryValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();

        return builder;
    }

    public static IHostApplicationBuilder AddOptions(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
        builder.Services.Configure<CookieSettings>(builder.Configuration.GetSection("CookieOptions"));
        
        return builder;
    }

    public static IHostApplicationBuilder AddJwtBearerAuthentication(this IHostApplicationBuilder builder)
    {
        JwtOptions? jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

        if (jwtOptions == null)
        {
            throw new KeyNotFoundException("JwtOptions are not configured correctly.");
        }

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.ISSUER,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.AUDIENCE,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtOptions.GetKey()
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (string.IsNullOrEmpty(context.Token) &&
                            context.Request.Cookies.TryGetValue("Token", out string? token))
                        {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy("StudentOnly", policy => policy.RequireRole("Student"))
            .AddPolicy("TeacherOnly", policy => policy.RequireRole("Teacher"));
            
        return builder;
    }

    public static IHostApplicationBuilder AddResponseCompression(this IHostApplicationBuilder builder)
    {
        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        return builder;
    }

    public static IHostApplicationBuilder AddCors(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowMvcClient",
                policy =>
                {
                    policy.WithOrigins("https://localhost:7282/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        return builder;
    }

    public static IHostApplicationBuilder AddLocalization(this IHostApplicationBuilder builder)
    {
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        return builder;
    }
}