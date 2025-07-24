using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
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
            DotNetEnv.Env.Load();
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("/app/keys"))
                .SetApplicationName("ChatApi");

            services.Configure<ApplicationSettings>(_configuration.GetSection("ApplicationSettings"));

            var settingsSection = _configuration.GetSection("ApplicationSettings");
            var settings = settingsSection.Get<ApplicationSettings>();

            var server = Environment.GetEnvironmentVariable("DbServer") ?? settings.Server;
            var port = Environment.GetEnvironmentVariable("DbPort") ?? settings.Port;
            var db = Environment.GetEnvironmentVariable("Database") ?? settings.Database;
            var tenant = Environment.GetEnvironmentVariable("DbTenant") ?? settings.TenantDb;
            var user = Environment.GetEnvironmentVariable("DbUser") ?? settings.UserId;
            var password = Environment.GetEnvironmentVariable("DbPassword") ?? settings.PasswordDb;

            var connectionStringApp = string.Format(settings.ConnectionString, server, port, db, user, password);
            var connectionStringTenant = string.Format(settings.ConnectionString, server, port, tenant, user, password);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionStringApp);
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
            });

            services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseNpgsql(connectionStringTenant);
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

            services.AddScoped(typeof(IRepository<>), typeof(AppRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(TenantRepository<>));

            services.AddScoped<UserService>();
            services.AddScoped<IRepository<UserModel>, AppRepository<UserModel>>();
            services.AddScoped<IBaseController<UserModel>, UserService>();
            services.AddTransient<UserService>();

            services.AddScoped<TenantService>();
            services.AddScoped<IRepository<TenantModel>, AppRepository<TenantModel>>();
            services.AddScoped<IBaseController<TenantModel>, TenantService>();
            services.AddTransient<TenantService>();

            services.AddScoped<ChannelService>();
            services.AddScoped<IRepository<ChannelModel>, AppRepository<ChannelModel>>();
            services.AddScoped<IBaseController<ChannelModel>, ChannelService>();
            services.AddTransient<ChannelService>();

            services.AddScoped<BotService>();
            services.AddScoped<IRepository<BotModel>, TenantRepository<BotModel>>();
            services.AddScoped<IBaseController<BotModel>, BotService>();
            services.AddTransient<BotService>();

            services.AddScoped<ChatUserMessageService>();
            services.AddScoped<IRepository<ChatUserMessageModel>, TenantRepository<ChatUserMessageModel>>();
            services.AddScoped<IBaseController<ChatUserMessageModel>, ChatUserMessageService>();
            services.AddTransient<ChatUserMessageService>();

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
