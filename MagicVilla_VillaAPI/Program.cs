using MagicVilla_VillaAPI.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Log.Logger= new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("log/villaLogs.txt",rollingInterval:RollingInterval.Year).CreateLogger();

//builder.Host.UseSerilog(); //dont use built in logger instead use Serilogger

builder.Services.AddControllers().AddNewtonsoftJson();//.AddXmlDataContractSerializerFormatters() in order to add XML output format support 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging, LoggingV2>(); //addSingleton, addScoped , addTransient 
//With dependency injection we can just change Logging to LoggingV2 and change the whole implementation 

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
