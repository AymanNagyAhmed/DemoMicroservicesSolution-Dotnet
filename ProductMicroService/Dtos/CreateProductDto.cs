using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProductMicroService.Dtos
{
    [Index(nameof(Code), IsUnique = true)]
    public class CreateProductDto
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}