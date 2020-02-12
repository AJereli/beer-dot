using System.Collections.Generic;
using System.Threading.Tasks;
using BeerDotApi.Beer.DTO;
using BeerDotApi.Database.Entities;

namespace BeerDotApi.Beer
{
    public interface IBeerService
    {
        public Task<bool> Add(BeerDto beer, long userId);
        public Task<bool> Add(BeerWithReviewDto beerWithReviewDto, long userId);

        public Task<IEnumerable<BeerEntity>> GetAll();

        public Task<BeerEntity> Get(long id);
    }
}