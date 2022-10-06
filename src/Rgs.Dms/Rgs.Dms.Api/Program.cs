using Rgs.Dms.Api.Infrastructure.BackgroundTask;
using Rgs.Dms.Api.Infrastructure.Configuration;
using Rgs.Dms.Api.Infrastructure.Middlewares.Extentions;
var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddApiVersioning();

builder.Services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
builder.Services.AddHostedService<BackgroundQueueHostedService>();


builder.Services.AddDmsAuthentication();
builder.Services.Configure<HostOptions>(
        opts => opts.ShutdownTimeout = TimeSpan.FromSeconds(20));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.AddCorrelationIdToResponseMiddleware();
app.AddCorrelationIdToLogContextMiddleware();

app.Run();


