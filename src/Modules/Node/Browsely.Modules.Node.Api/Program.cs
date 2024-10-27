using Browsely.Common.Infrastructure.Extensions;
using Browsely.Common.Infrastructure.Middleware;
using Browsely.Modules.Node.Application;
using Browsely.Modules.Node.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services
    .AddApplication([AssemblyReference.Assembly], builder.Configuration)
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseLogContext();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();

await app.RunAsync();
