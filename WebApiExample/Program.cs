using Application.Features.AmountToWords;
using Application.Features.Auth.Repository;
using Application.Features.Divisors;
using Application.Features.FilmDatabase;
using Application.Features.NumberToWords;
using Application.Features.PrimeNumbers;
using Application.Features.RailVehicles.Model;
using Application.Features.RailVehicles.Repository;
using Application.Features.RandomSeriesEpisode;
using Application.Services;
using Asp.Versioning;
using Infrastructure;
using Infrastructure.DatabaseOperations.HardDelete;
using Infrastructure.DatabaseOperations.Insert;
using Infrastructure.DatabaseOperations.Restore;
using Infrastructure.DatabaseOperations.SoftDelete;
using Infrastructure.DatabaseOperations.Update;
using Infrastructure.Features.AmountToWords.Repository;
using Infrastructure.Features.Auth.Repository;
using Infrastructure.Features.FilmDatabase.Repository;
using Infrastructure.Features.RailVehicles.Repository;
using Infrastructure.Services;
using Infrastructure.Services.CurrentUser;
using Infrastructure.Services.Jwt;
using Infrastructure.Services.PasswordHash;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApiExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = ApiVersion.Default;
                    options.ReportApiVersions = true;
                }).AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'V";
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Services.AddAutoMapper(typeof(Program), typeof(AutoMapperProfile));

            string env = builder.Environment.EnvironmentName;
            string connectionStringName = $"{env}Database";
            string connectionString = builder.Configuration.GetConnectionString(connectionStringName) ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("Infrastructure")), ServiceLifetime.Transient);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration["Token"] ?? throw new KeyNotFoundException("Token not found."))),
                        ValidateIssuerSigningKey = true
                    };
                });
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<ICurrentUtcTimeProvider, CurrentUtcTimeProvider>();
            builder.Services.AddScoped<ICurrentUserIdProvider, CurrentUserIdProvider>();
            builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            builder.Services.AddSingleton<IPasswordHashService, PasswordHashService>();
            builder.Services.AddSingleton<IJwtService, JwtService>();
            builder.Services.AddScoped<Random>();
            builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();
            builder.Services.AddScoped<IUserRolesProvider, UserRolesProvider>();

            builder.Services.AddScoped<IInsertOperation, InsertOperation>();
            builder.Services.AddScoped<IUpdateOperation, UpdateOperation>();
            builder.Services.AddScoped<ISoftDeleteOperation, SoftDeleteOperation>();
            builder.Services.AddScoped<IRestoreOperation, RestoreOperation>();
            builder.Services.AddScoped<IHardDeleteOperation, HardDeleteOperation>();

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IPrimeNumbersService, PrimeNumbersService>();
            builder.Services.AddScoped<IDivisorsService, DivisorsService>();
            builder.Services.AddScoped<INumberToWordsCzechService, NumberToWordsCzechService>();
            builder.Services.AddScoped<ICurrencyCzechNameRepository, CurrencyCzechNameRepository>();
            builder.Services.AddScoped<IAmountInWordsCzechService, AmountInWordsCzechService>();
            builder.Services.AddScoped<IRandomSeriesEpisodeService, RandomSeriesEpisodeService>();
            builder.Services.AddScoped<IFilteredFilmsRepository, FilteredFilmsRepository>();

            builder.Services.AddScoped<IElectrificationTypeRepository<ElectrificationTypeModel, ElectrificationTypeListModel>, ElectrificationTypeRepository>();
            builder.Services.AddScoped<IRailVehicleRepository<RailVehicleModelBase>, RailVehicleRepository>();
            builder.Services.AddScoped<IRailVehicleListRepository, RailVehicleListRepository>();
            builder.Services.AddScoped<IRailVehicleDeletedRepository<RailVehicleDeletedModel>, RailVehicleDeletedRepository>();
            builder.Services.AddScoped<IRailVehicleNameRepository, RailVehicleNameRepository>();
            builder.Services.AddScoped<ITrainRepository<TrainInputModel, TrainOutputModel>, TrainRepository>();
            builder.Services.AddScoped<ITrainListRepository<TrainListModel>, TrainListRepository>();
            builder.Services.AddScoped<ITrainDeletedRepository<TrainDeletedModel>, TrainDeletedRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
