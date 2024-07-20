using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CustomerMicroService.Models
{
    [Table("customers", Schema = "dbo")]
    [Index("Email",IsUnique = true)]
    [Index("Mobile", IsUnique = true)]
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
        [Phone]
        public string Mobile { get; set; }
        [Column("email")]
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        [MinLength(3)]
        public string Email { get; set; }
    }
}
