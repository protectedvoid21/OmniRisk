using OmniRiskAPI.Api;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Authorization;
using OmniRiskAPI.Persistence;
using OmniRiskAPI.Setup;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

builder.Services.AddAuthentication(builder.Configuration, builder.Environment.IsProduction());
builder.Services.AddTokenService();

var connectionString = builder.Configuration.GetConnectionString("DefaultDb") ??
                       builder.Configuration.GetConnectionString("Test");
builder.Services.AddSqlServer<OmniRiskDbContext>(connectionString);

builder.Services.AddCurrentUser();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedDatabase();

app.UseHttpsRedirection();
app.RegisterApiEndpoints();

app.Run();