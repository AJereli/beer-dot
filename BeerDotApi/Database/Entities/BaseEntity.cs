using System;

namespace BeerDotApi.Database.Entities
{
    public interface IEntity
    {
        long Id { get; set; }
    }
    
    public class BaseEntity: IEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}