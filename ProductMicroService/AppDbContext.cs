using Microsoft.EntityFrameworkCore;
using ProductMicroService.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ProductMicroService
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // try
            // {
            //     var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //     if (databaseCreator != null)
            //     {
            //         if (!databaseCreator.CanConnect())
            //         {
            //             databaseCreator.Create();
            //         }
            //         if (!databaseCreator.HasTables())
            //         {
            //             databaseCreator.CreateTables();
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
        }

    }
}
