using Microsoft.EntityFrameworkCore;
using OmniRiskAPI.Api;
using OmniRiskAPI.Persistence;
using OmniRiskAPI.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultDb") ??
                       builder.Configuration.GetConnectionString("Test");
builder.Services.AddSqlServer<OmniRiskDbContext>(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedDatabase();

app.UseHttpsRedirection();
app.RegisterApiEndpoints();

app.Run();