using Aplication.OverboardChess;
using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Http.OverboardChess.Providers;
using Infrastructure.OverboardChess.Database;
using Infrastructure.OverboardChess.Database.MongoDbRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Configuration;
using Utilities.OverboardChess.TokenProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(configue =>
{
    configue.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    configue.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});
builder.Services.AddScoped<ICurrentIdentity, CurrentIdentityHttp>();
builder.Services.AddSingleton<MongoDatabase>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IMeetingRepository, MeetingMongoRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<MongoDbSettings>(config =>
{
    config.ConnectionString = Environment.GetEnvironmentVariable("MONGO_SETTINGS_CONNECTION_STRING") 
        ?? builder.Configuration.GetSection("MongoDbSettings")["ConnectionString"] 
        ?? "";
    config.DatabaseName = builder.Configuration.GetSection("MongoDbSettings")["DatabaseName"] ?? "";
});

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(ApplicationAssembly.Assembly));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
    options.DefaultScheme = "Bearer";
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = ValidationParameters.Get(Key.GetSymetricSecurityKey("oaisdjasoidaslkdmnaskjdbaskdbasukdjasdsa")).Value;
});

//builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/exception");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
