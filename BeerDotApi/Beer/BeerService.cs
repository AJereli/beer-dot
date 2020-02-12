using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeerDotApi.Beer.DTO;
using BeerDotApi.Database;
using BeerDotApi.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BeerDotApi.Beer
{
    public class BeerService: IBeerService
    {
        private readonly BeerContext _beerContext;
        private readonly ILogger<BeerService> _logger;
        private readonly BeerRepository _beerRepository;
        
        public BeerService(BeerContext beerContext, ILogger<BeerService> logger, BeerRepository beerRepository)
        {
            _logger = logger;
            _beerContext = beerContext;
            _beerRepository = beerRepository;
        }
        
        public async Task<bool> Add(BeerDto beer, long userId)
        {
            try
            {
                var user = await _beerContext.
                    User.
                    SingleOrDefaultAsync(u => u.Id == userId);
                
                var beerModel = new BeerEntity {
                        Producer = beer.Producer, 
                        Title = beer.Title,
                        Creater = user
                };
                
                var created = await _beerContext.Beer.AddAsync(beerModel);
                var resultCount = await _beerContext.SaveChangesAsync();
                return resultCount != 0;
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> Add(BeerWithReviewDto beerWithReviewDto, long userId)
        {
            try
            {
                var beer = beerWithReviewDto.Beer;
                var review = beerWithReviewDto.Review;
                
                var user = await _beerContext.
                    User.
                    SingleAsync(u => u.Id == userId);
                
                var createdBeer = await _beerContext.Beer.AddAsync(new BeerEntity {
                    Producer = beer.Producer, 
                    Title = beer.Title,
                    Creater = user
                });
                
                var reviewModel = new BeerReviewEntity
                {
                    Beer = createdBeer.Entity,
                    Price = review.Price,
                    Mark = review.Mark,
                    Description = review.Description,
                    IsDiscount = review.IsDiscount
                };

                var createdReview = await _beerContext.Review.AddAsync(reviewModel);
                
                return await _beerContext.SaveChangesAsync() != 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<BeerEntity>> GetAll()
        {
            return await _beerContext.Beer.AsQueryable().ToListAsync();
        }

        public async Task<BeerEntity> Get(long id)
        {
            // new BeerContext().
            return await _beerContext.Beer.SingleOrDefaultAsync(b => b.Id == id);
        }
    }
}