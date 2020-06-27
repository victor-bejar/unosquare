using System.ComponentModel.DataAnnotations;

using App.Model.Interface;

namespace App.Persistence.Model
{

    public class Product : IProduct
    {

        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int? AgeRestriction { get; set; }

        [Required]
        [StringLength(50)]
        public string Company { get; set; }

        [Required]
        public decimal Price { get; set; }

    }

}