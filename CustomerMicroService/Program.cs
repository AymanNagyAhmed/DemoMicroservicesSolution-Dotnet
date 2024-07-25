using CustomerMicroService;
using JwtAuthenticationManager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();

/* Local Database Context Dependency Injection */
// var dbHost = "(localdb)\\local";
// var dbName = "customer_ms_db";
// var dbUser = "sa";
// var dbPassword = "@12345Admin";

/* Docker Database Context Dependency Injection */
string dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "customerdb";
string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "customer_db";
string dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
string dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? "123456789";
var dbConnectionString = $"Data Source={dbHost};Initial Catalog={dbName};User Id={dbUser};password={dbPassword};Encrypt=True;TrustServerCertificate=True";
// $"Data Source=(localdb)\local;Initial Catalog=customer_db;User Id=sa;password=@12345Admin;Encrypt=True;TrustServerCertificate=True";
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
