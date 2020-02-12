using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeerDotApi.Beer.DTO
{
    public class BeerWithReviewDto
    {
        [Required]
        public BeerDto Beer { get; set; }
        [Required]
        public ReviewDto Review { get; set; }
    }
}