using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProductMicroService.Models
{
    [Table("products")]
    [Index(nameof(Code), IsUnique = true)]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Column("code")]
        [Required]
        public string Code { get; set; }

        [Column("price")]
        [Required]
        public decimal Price { get; set; }
    }
}
