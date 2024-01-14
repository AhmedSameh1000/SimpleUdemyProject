using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using UdemyProject.Application.ServicesImplementation.AuthServicesImplementation;
using UdemyProject.Application.ServicesImplementation.CourseCategoriesServicesimplementation;
using UdemyProject.Application.ServicesImplementation.Courselangugeimplementation;
using UdemyProject.Application.ServicesImplementation.CourseLectureServiceImplementation;
using UdemyProject.Application.ServicesImplementation.CourseSectionServiceImplementation;
using UdemyProject.Application.ServicesImplementation.CourseServicesimplementation;
using UdemyProject.Contracts.RepositoryContracts;
using UdemyProject.Contracts.ServicesContracts;

namespace UdemyProject.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
                c.EnableAnnotations();

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                          {
                              new OpenApiSecurityScheme
                              {
                                  Reference = new OpenApiReference
                                  {
                                      Type = ReferenceType.SecurityScheme,
                                      Id = JwtBearerDefaults.AuthenticationScheme
                                  }
                              },
                              Array.Empty<string>()
                          }
                     });
            });

            #endregion Swagger

            #region Localization

            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "";
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-EG")
                };

                options.DefaultRequestCulture = new RequestCulture("ar-EG");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            #endregion Localization

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<ICourseLangugeService, CourseLangugeService>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseSectionService, CourseSectionService>();
            services.AddScoped<ICourseLectureService, CourseLectureService>();
            services.AddMediatR(md => md.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}