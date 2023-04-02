using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebLuto.Data;
using WebLuto.Repositories;
using WebLuto.Repositories.Interfaces;
using WebLuto.Security;
using WebLuto.Services;
using WebLuto.Services.Interfaces;

namespace WebLuto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services

            builder.Services.AddAutoMapper(typeof(Mapper.Mapper));
            builder.Services.AddControllers();
            builder.Services.AddCors();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion

            #region Scopes

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            #endregion

            #region Secret Key

            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = configurationBuilder.Build();

            var settings = new Settings(configuration);
            byte[] secretKey = Encoding.ASCII.GetBytes(settings.GetKeyValue("SecretKey"));

            #endregion

            #region JWT Authorization

            builder.Services.AddAuthentication(x =>
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
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion

            #region DataBase Connection

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<WLDBContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );

            #endregion

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}