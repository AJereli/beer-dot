using System;
using System.ComponentModel.DataAnnotations;

namespace BeerDotApi.Beer.DTO
{
    public class ReviewDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        public bool IsDiscount { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0, 10)]
        public int Mark { get; set; }
    }
}