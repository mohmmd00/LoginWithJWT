using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TA.Application.Services;
using TA.Domain.Interfaces;
using TA.Infrastructure.Sqlite;
using TA.Infrastructure.Sqlite.Repository;

namespace TA.Presentation.Webapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AuthContext>(options => options.UseSqlite("Data Source=..\\TA.Infrastructure.Sqlite\\app.db"));

            // Add services to the container.
            builder.Services.AddScoped<IUserRepository, UserRepository>(); //manage connections to data base with transient we have to make multiple connections to database and that have security and performance problems
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();



            builder.Services.AddTransient<IPasswordService, PasswordService>();
            builder.Services.AddTransient<IJwtService, JwtService>();



            builder.Services.AddScoped<AuthService>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,           
                    ValidateLifetime = true,           
                    ValidateIssuerSigningKey = true,  
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
                };
            });


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMiddleware<SessionValidationMiddleware>();
            app.UseAuthorization();



            app.MapControllers();

            app.Run();
        }
    }
}
