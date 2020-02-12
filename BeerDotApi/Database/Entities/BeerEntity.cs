namespace BeerDotApi.Database.Entities
{
    public class BeerEntity: BaseEntity
    {
        public string Title { get; set; }
        public string Producer { get; set; }
        
        public UserEntity Creater { get; set; }
    }
}