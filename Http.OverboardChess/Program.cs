using Aplication.OverboardChess;
using Aplication.OverboardChess.Abstractions;
using Aplication.OverboardChess.Abstractions.Repositories;
using Aplication.OverboardChess.Abstractions.Repositories.InvitationRepositories;
using Aplication.OverboardChess.Abstractions.Repositories.MeetingRepositories;
using Aplication.OverboardChess.Providers;
using Http.OverboardChess.Providers;
using Infrastructure.OverboardChess.Database;
using Infrastructure.OverboardChess.Database.MongoDbRepositories;
using Infrastructure.OverboardChess.Providers;
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
builder.Services.AddScoped<IInvitationRepository, InvitationMongoRepository>();
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IEmailProvider, EmailProvider>();
builder.Services.AddScoped<ISecurityTokenProvider, SecurityTokenProvider>();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<MongoDbSettings>(config =>
{
    var appSettingsSection = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>() ?? new MongoDbSettings();
    config.ConnectionString = Environment.GetEnvironmentVariable("MONGO_SETTINGS_CONNECTION_STRING") ?? appSettingsSection.ConnectionString;
    config.DatabaseName = appSettingsSection.DatabaseName;
});

builder.Services.Configure<EmailProviderSettings>(config =>
{
    config.Email = "overboardchess@gmail.com";
    config.Password = Environment.GetEnvironmentVariable("EMAIL_PROVIDER_SETTINGS_PASSWORD") ?? "";
    config.Username = "OverboardChess";
    config.Server = "smtp.gmail.com";
    config.Port = 587;
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
    //app.UseCors(configue =>
    //{
    //    configue.AllowAnyOrigin();
    //    configue.AllowAnyMethod();
    //    configue.AllowAnyHeader();
    //    configue.AllowCredentials();
    //});
}

app.UseExceptionHandler("/exception");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
