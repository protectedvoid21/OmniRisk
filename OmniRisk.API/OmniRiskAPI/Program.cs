using Microsoft.AspNetCore.Identity;
using OmniRiskAPI.Api;
using OmniRiskAPI.Authentication;
using OmniRiskAPI.Authorization;
using OmniRiskAPI.Persistence;
using OmniRiskAPI.Services;
using OmniRiskAPI.Setup;
using Swashbuckle.AspNetCore.SwaggerGen;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy => {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SwaggerGeneratorOptions>(o => o.InferSecuritySchemes = true);

builder.Services.AddAuthentication(builder.Configuration, builder.Environment.IsProduction());
builder.Services.AddTokenService();

var connectionString = builder.Configuration.GetConnectionString("DefaultDb") ??
                       builder.Configuration.GetConnectionString("Test");

builder.Services.AddSqlServer<OmniRiskDbContext>(connectionString);
builder.Services.AddCurrentUser();

//builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>()
//    .AddEntityFrameworkStores<OmniRiskDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddScoped<TwitterService, TwitterService>();
builder.Services.AddScoped<GptService, GptService>();

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedDatabase();

app.UseHttpsRedirection();
app.RegisterApiEndpoints();

app.Run();