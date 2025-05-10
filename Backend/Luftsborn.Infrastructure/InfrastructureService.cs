using Luftsborn.Application.Contracts.Repositories;
using Luftsborn.Domain.Entities;
using Luftsborn.Infrastructure.Managers.RepositoriesManagers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftsborn.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("LuftsbornContext");
            services.AddDbContext<LuftsbornDbContext>(options =>options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);

            }).AddEntityFrameworkStores<LuftsbornDbContext>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "default";
                options.DefaultChallengeScheme = "default";
            })
                .AddJwtBearer("default", options => {
                    var secretKey = configuration.GetValue<string>("SecretKey");
                    var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
                    var key = new SymmetricSecurityKey(secretKeyInBytes);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key
                    };
                });


            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<ITagRepository, TagRepository>();

            return services;
        }
    }
}
