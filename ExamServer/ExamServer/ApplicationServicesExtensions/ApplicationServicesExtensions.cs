using System.Reflection;
using System.Text;
using Data;
using Domain.Entities;
using ExamServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ExamServer.ApplicationServicesExtensions;

public static class ApplicationServicesExtensions
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
    
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        return services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly));
    }
    
    public static IServiceCollection AddApplicationDb(this IServiceCollection services,
        string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.LogTo(Console.WriteLine);
            options.UseNpgsql(connectionString);
        });
        
        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        return services;
    }

    public static IServiceCollection AddJWTAuthorization(this IServiceCollection services, IConfigurationManager config)
    {
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = config["JwtSettings:Issuer"],
                    ValidAudience = config["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //ValidateLifetime = true, //Todo надо ли это и нужно ли делать refresh для этого?
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddAuthorizationBuilder();
        
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        
        return services;
    }
}