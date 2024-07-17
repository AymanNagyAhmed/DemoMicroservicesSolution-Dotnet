using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService.Models
{
    [Table("customers", Schema = "dbo")] // Corrected syntax for specifying schema
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Column("mobile")]
        [Required]
        [Phone] // Corrected attribute casing
        public string Mobile { get; set; }
        [Column("email")]
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [MinLength(3)]
        // [Index(IsUnique = true)]
        public string Email { get; set; }
    }

    // Extension method to configure the unique index for Email
    public static class ModelBuilderExtensions
    {
        public static void ConfigureCustomerEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
    }
}
