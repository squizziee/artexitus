using Artexitus.IdentityMicroservice.API.Extensions;
using Artexitus.IdentityMicroservice.API.Filters;
using Artexitus.IdentityMicroservice.API.Middleware;
using Artexitus.IdentityMicroservice.API.Misc;
using Artexitus.IdentityMicroservice.Application.Extensions;
using Artexitus.IdentityMicroservice.Infrastructure.Extensions;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Hangfire;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Configuration.AddJsonFile("tokensettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("credentials.json", optional: false, reloadOnChange: true);

builder.Services.AddValidators();
builder.Services.AddProblemDetails();
builder.Services.AddInfrastructureConfigSections(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddBackgroundJobs(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDatabaseContext>();
    dbContext.Database.Migrate();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard("/hf-dash", new DashboardOptions
{
    Authorization = [new DashboardFilter()]
});

app.MapControllers();

RecurringJobInstantiator.Init();

app.Run();
