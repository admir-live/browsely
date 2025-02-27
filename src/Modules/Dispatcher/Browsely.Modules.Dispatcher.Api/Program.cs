using Browsely.Common.Application;
using Browsely.Common.Infrastructure.Extensions;
using Browsely.Common.Infrastructure.Middleware;
using Browsely.Common.Presentation.Endpoints;
using Browsely.Modules.Dispatcher.Application;
using Browsely.Modules.Dispatcher.Infrastructure;
using Browsely.Modules.Dispatcher.Infrastructure.Database;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services
    .AddApplication([AssemblyReference.Assembly])
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigration<DispatcherDbContext>();
}

app.UseLogContext();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();
app.MapEndpoints();

await app.RunAsync();
