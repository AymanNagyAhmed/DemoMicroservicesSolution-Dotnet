using JwtAuthenticationManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
/* Add Authentication */
builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

await app.UseOcelot();

/* Add Authentication */
app.UseAuthentication();
app.UseAuthorization();

app.Run();