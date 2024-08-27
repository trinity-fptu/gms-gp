using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WebAPI.Middlewares;
using WebAPI.Services;


namespace WebAPI
{
    public static class DependencyInjection
    {
        public static void AddWebAPIService(this IServiceCollection services, WebApplicationBuilder builder)
        {
            #region JWT Config
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            #endregion

            #region Controller configs
            services
               .AddControllers()
               .AddJsonOptions(c => c.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            #endregion

            #region Swagger config
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });
            #endregion

            #region CORS Policy config
            services.AddCors();
            #endregion


            services.AddQuartz(q =>
            {
                // Use a Scoped container to create jobs. I'll default to a new job every time.
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Create a "key" for the job
                var jobKey = new JobKey("CronJob");

                // Register the job with the DI container
                q.AddJob<CronJob>(opts => opts.WithIdentity(jobKey));

                // Create a trigger for the job
                q.AddTrigger(opts => opts
                    .ForJob(jobKey) // link to the Cronjob
                    .WithIdentity("Job trigger") // give the trigger a unique name
                    .WithCronSchedule("0 1 0 * * ?")); // run at 12:01 AM every day
            });

                // Add the QuartzHostedService
                services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);

            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddHealthChecks();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddHttpContextAccessor();
        }
    }
}