//using Serilog;

//using MagicVilla_villaAPI.Logging;

using MagicVilla_villaAPI;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); 

        builder.Services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
        });

        // Add services to the container.

        //Log.Logger =  new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villLogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();

        //builder.Host.UseSerilog();

        builder.Services.AddControllers(option =>{
                //option.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //builder.Services.AddSingleton<ILogging, Logging>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
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