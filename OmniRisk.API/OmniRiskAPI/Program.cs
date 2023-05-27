using OmniRiskAPI.Api;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Persistence;
using OmniRiskAPI.Services;
using OmniRiskAPI.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var connectionString = builder.Configuration.GetConnectionString("DefaultDb") ??
//                       builder.Configuration.GetConnectionString("Test");
//builder.Services.AddSqlServer<OmniRiskDbContext>(connectionString);


builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<TwitterService, TwitterService>();
builder.Services.AddScoped<GptService, GptService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.SeedDatabase();

app.UseHttpsRedirection();
app.RegisterApiEndpoints();

app.Run();