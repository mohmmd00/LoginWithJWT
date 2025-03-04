
using Microsoft.EntityFrameworkCore;
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
            builder.Services.AddScoped<IUserRepository, UserRepository>(); // works with database and we dont need to make a new instance of these services every time we use them 
            builder.Services.AddScoped<ISessionRepository, SessionRepository>();



            builder.Services.AddTransient<IPasswordService, PasswordService>();
            builder.Services.AddTransient<IJwtService, JwtService>();



            builder.Services.AddScoped<AuthService>();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            app.UseAuthorization();
            app.UseAuthentication();


            app.MapControllers();

            app.Run();
        }
    }
}
