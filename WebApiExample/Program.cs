using Application.Features.AmountToWords;
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
using Infrastructure.Features.FilmDatabase.Repository;
using Infrastructure.Features.RailVehicles.Repository;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Services.VerifyUser;

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

            const string connectionStringName = "DefaultConnection";
            var connectionString = builder.Configuration.GetConnectionString(connectionStringName) ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("Infrastructure")), ServiceLifetime.Transient);

            builder.Services.AddScoped<IVerifyUserService, VerifyUserService>();
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformationService>();
            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<ICurrentUserIdProvider, CurrentUserIdProvider>();
            builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

            builder.Services.AddScoped<ICurrentUtcTimeProvider, CurrentUtcTimeProvider>();
            builder.Services.AddScoped<Random>();
            builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();

            builder.Services.AddScoped<IPrimeNumbersService, PrimeNumbersService>();
            builder.Services.AddScoped<IDivisorsService, DivisorsService>();
            builder.Services.AddScoped<INumberToWordsCzechService, NumberToWordsCzechService>();
            builder.Services.AddScoped<ICurrencyCzechNameRepository, CurrencyCzechNameRepository>();
            builder.Services.AddScoped<IAmountInWordsCzechService, AmountInWordsCzechService>();
            builder.Services.AddScoped<IRandomSeriesEpisodeService, RandomSeriesEpisodeService>();
            builder.Services.AddScoped<IFilteredFilmsRepository, FilteredFilmsRepository>();

            builder.Services.AddScoped<IInsertOperation, InsertOperation>();
            builder.Services.AddScoped<IUpdateOperation, UpdateOperation>();
            builder.Services.AddScoped<ISoftDeleteOperation, SoftDeleteOperation>();
            builder.Services.AddScoped<IRestoreOperation, RestoreOperation>();
            builder.Services.AddScoped<IHardDeleteOperation, HardDeleteOperation>();

            builder.Services.AddScoped<IRailVehicleRepository<RailVehicleModelBase>, RailVehicleRepository>();
            builder.Services.AddScoped<IRailVehicleListRepository, RailVehicleListRepository>();
            builder.Services.AddScoped<IRailVehicleDeletedRepository<RailVehicleDeletedModel>, RailVehicleDeletedRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapIdentityApi<ApplicationUser>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
