using System.Collections.Generic;

namespace BeerDotApi.Database.Entities
{
    public class UserEntity: BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        public List<BeerReviewEntity> Reviews { get; set; }
    }
}