using Application.OverboardChess.Repositories;
using Infrastructure.OverboardChess.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<MongoDatabase>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
builder.Services.Configure<MongoDbSettings>(config =>
{
    config.ConnectionString = Environment.GetEnvironmentVariable("MONGO_SETTINGS_CONNECTION_STRING") 
        ?? builder.Configuration.GetSection("MongoDbSettings")["ConnectionString"] 
        ?? "";
    config.DatabaseName = builder.Configuration.GetSection("MongoDbSettings")["DatabaseName"] ?? "";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
