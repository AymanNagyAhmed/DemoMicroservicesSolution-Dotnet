
using ProductMicroService;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/* Local Database Context Dependency Injection */
var dbHost = "localhost";
var dbName = "product_ms_db";
var dbPort = "3306";
var dbUser = "admin";
var dbPassword = "@12345Admin";

/* Docker Database Context Dependency Injection */
// string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "productdb";
// string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "product_db";
// string dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
// string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
// string dbPassword = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD") ?? "123456789";

var connectionString = $"server={dbHost};port={dbPort};database={dbName};user={dbUser};password={dbPassword}";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

/* =============================================================== */

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// add cors header
builder.Services.AddCors();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
