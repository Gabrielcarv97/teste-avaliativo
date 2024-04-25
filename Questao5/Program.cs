using System.Data;
using MediatR;
using Questao5.Infrastructure.Sqlite;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.OpenApi.Models;
using Questao5.Application.Handlers;
using Questao5.Infrastructure.Repositories;
using Questao5.Infrastructure.Repositories.Interfaces;
using Questao5.Infrastructure.Services;
using Questao5.Infrastructure.Services.Interfaces;
using Questao5.Infrastructure.Repositories.Scripts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(config=> config.RegisterServicesFromAssemblyContaining<GetBalanceHandler>());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDbConnection>((sp) =>
{
    var connectionString = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite");
    return new SqliteConnection(connectionString);
});
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddSingleton<IMovementRepository, MovementRepository>();
builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton<IBalanceService, BalanceService>();
builder.Services.AddSingleton<IMovementService, MovementService>(); 
builder.Services.AddSingleton<IIdempotencyRepository, IdempotencyRepository>();
builder.Services.AddSingleton<IIdempotencyManageService, IdempotencyManageService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API - Account", Version = "v1" });
});


var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

app.Run();