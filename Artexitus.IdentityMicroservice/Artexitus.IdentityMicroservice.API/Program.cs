using Artexitus.IdentityMicroservice.API.Extensions;
using Artexitus.IdentityMicroservice.API.Middleware;
using Artexitus.IdentityMicroservice.Application.Extensions;
using Artexitus.IdentityMicroservice.Infrastructure.Extensions;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("tokensettings.json", optional: false, reloadOnChange: true);

builder.Services.AddAutomatedRequestValidation();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureConfigSections(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddAuth(builder.Configuration);

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

app.MapControllers();

app.Run();
