using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IO;
using System.Reflection;
using System;
using ChatApi.API.Interfaces;
using ChatApi.Domain.Entities.Models;
using ChatApi.Domain.Entities.Tenants;
using ChatApi.Domain.Entities.Settings;
using ChatApi.Infrastructure.Middlewares;
using ChatApi.Infrastructure.Context;
using ChatApi.Infrastructure.Repositories;
using ChatApi.Infrastructure.Services;
using ChatApi.Infrastructure.Interfaces;

namespace ChatApi
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.Configure<ApplicationSettings>(_configuration.GetSection("ApplicationSettings"));

            var settingsSection = _configuration.GetSection("ApplicationSettings");
            var settings = settingsSection.Get<ApplicationSettings>();

            var server = _configuration["DbServer"] ?? settings.Server;
            var port = _configuration["DbPort"] ?? settings.Port;
            var db = _configuration["Database"] ?? settings.Database;
            var tenant = _configuration["TenantDb"] ?? settings.TenantDb;
            var user = _configuration["DbUser"] ?? settings.UserId;
            var password = _configuration["Password"] ?? settings.PasswordDb;

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(
                    string.Format(settings.ConnectionString, server, port, db, user, password)
                );
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseNpgsql(
                    string.Format(settings.ConnectionString, server, port, tenant, user, password)
                );
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Chat Application",
                    Description = "An example chat application with ASP.NET Core Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Abel Gasque",
                        Email = "abelgasque20@gmail.com",
                        Url = new Uri("https://abelgasque.com")
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
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
                        new string[]{ }
                    }
                });
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<UserService>();
            services.AddScoped<IBaseController<UserModel>, UserService>();
            services.AddTransient<UserService>();

            services.AddScoped<TenantService>();
            services.AddScoped<IBaseController<TenantModel>, TenantService>();
            services.AddTransient<TenantService>();

            services.AddScoped<ChannelService>();
            services.AddScoped<IBaseController<ChannelModel>, ChannelService>();
            services.AddTransient<ChannelService>();

            services.AddScoped<BotService>();
            services.AddScoped<IBaseController<BotModel>, BotService>();
            services.AddTransient<BotService>();

            services.AddTransient<TokenService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");                
                app.UseHsts();
            }

            app.UseCors("AllowOrigin");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
        }
    }
}
