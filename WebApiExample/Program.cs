using Microsoft.EntityFrameworkCore;
using WebApiExample.Features.Divisors;
using WebApiExample.Features.FilmDatabase;
using WebApiExample.Features.NumberInWords;
using WebApiExample.Features.Primes;
using WebApiExample.Features.RailVehicles;
using WebApiExample.Features.RandomSeriesEpisode;
using WebApiExample.GeneralServices.RandomNumber;
using WebApiExample.GeneralServices.User;
using WebApiExample.GenericRepositories.SimpleModel;
using WebApiExample.GenericRepositories.SimpleModelWithUser;

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
            builder.Services.AddScoped<ISimpleModelWithUserRepository<RailVehicleModel>, SimpleModelWithUserRepository<RailVehicle, RailVehicleModel>>();

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
