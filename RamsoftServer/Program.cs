using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using RamsoftServer.Infrastructure;
using RamsoftServer.Infrastructure.Repositories;

var allowSpecificOrigins = "_allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000");
                      });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IColumnRepository, ColumnRepository>();

var configuration = builder.Configuration; // allows both to access and to set up the config

var connection = configuration["SqliteConnection:SqliteConnectionString"];
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(connection)
);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
