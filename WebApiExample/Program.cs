using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using WebApiExample.Features.AmountInWords.V1;
using WebApiExample.Features.Divisors.V1;
using WebApiExample.Features.FilmDatabase.V1;
using WebApiExample.Features.Primes.V1;
using WebApiExample.Features.RailVehicles.V1;
using WebApiExample.Features.RandomSeriesEpisode.V1;
using WebApiExample.GenericRepositories.SimpleModel;
using WebApiExample.GenericRepositories.SimpleModelWithUser;
using WebApiExample.SharedServices.NumberInWords;
using WebApiExample.SharedServices.RandomNumber;
using WebApiExample.SharedServices.RestoreItem;
using WebApiExample.SharedServices.User;

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

            var connectionString = builder.Configuration.GetConnectionString(Constants.ConnectionStringName) ?? throw new InvalidOperationException($"Connection string '{Constants.ConnectionStringName}' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("WebApiExample")), ServiceLifetime.Transient);

            builder.Services.AddAuthorization();

            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();

            builder.Services.AddScoped<IPrimesService, PrimesService>();
            builder.Services.AddScoped<IDivisorsService, DivisorsService>();
            builder.Services.AddScoped<INumberInWordsCzechService, NumberInWordsCzechService>();
            builder.Services.AddScoped<ICurrencyCzechNameRepository, CurrencyCzechNameRepository>();
            builder.Services.AddScoped<IAmountInWordsCzechService, AmountInWordsCzechService>();
            builder.Services.AddScoped<IRandomSeriesEpisodeService, RandomSeriesEpisodeService>();
            builder.Services.AddScoped<ISimpleModelRepository<FilmModel>, SimpleModelRepository<Film, FilmModel>>();
            builder.Services.AddScoped<IFilteredFilmsRepository, FilteredFilmsRepository>();
            builder.Services.AddScoped<ISimpleModelWithUserRepository<RailVehicleModel>, SimpleModelWithUserRepository<RailVehicle, RailVehicleModel>>();
            builder.Services.AddScoped<IRestoreItemService, RestoreItemService<RailVehicle>>();

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
