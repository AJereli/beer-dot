namespace BeerDotApi.Database.Entities
{
    public class BeerReviewEntity: BaseEntity
    {
        public decimal Price { get; set; }
        public bool IsDiscount { get; set; }
        public string Description { get; set; }
        public int Mark { get; set; }
        
        public UserEntity User { get; set; }
        public BeerEntity Beer { get; set; }
    }
}