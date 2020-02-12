using System.ComponentModel.DataAnnotations;

namespace BeerDotApi.Beer.DTO
{
    public class BeerDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Producer { get; set; }
    }
}