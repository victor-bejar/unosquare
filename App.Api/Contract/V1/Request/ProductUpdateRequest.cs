using System.ComponentModel.DataAnnotations;

namespace App.Api.Contract.V1.Request
{

    public class ProductUpdateRequest
    {
        
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [Range(0, 100)]
        public int? AgeRestriction { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Company { get; set; }

        [Range(1, 1000)]
        public decimal Price { get; set; }

    }

}