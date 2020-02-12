using BeerDotApi.Database;
using BeerDotApi.Database.Entities;
using BeerDotApi.RepositoryBase;

namespace BeerDotApi.Beer.DTO
{
    public class BeerRepository: BaseRepository<BeerEntity, BeerContext>
    {
        private readonly BeerContext _context;

        public BeerRepository(BeerContext context) : base(context)
        {
            _context = context;
        }
        
        
        
        
    }
}