using CustomerMicroService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/* Database Context Dependency Injection */
var dbHost = "(localdb)\\local";
var dbName = "customer_ms_db";
var dbUser = "sa";
var dbPassword = "@12345Admin";
var dbConnectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID={dbUser};Password={dbPassword}";
// $"Data Source={dbHost};Initial Catalog={dbName};Integrated Security=True";
// $"server={dbHost};port={dbPort};database={dbName};user={dbUser};password={dbPassword}";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(dbConnectionString));
/* ===================== */


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
