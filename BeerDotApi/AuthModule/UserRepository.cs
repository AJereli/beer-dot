using System.Threading.Tasks;
using BeerDotApi.Database;
using BeerDotApi.Database.Entities;
using BeerDotApi.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace BeerDotApi.AuthModule
{
    public class UserRepository: BaseRepository<UserEntity, BeerContext>
    {
        private readonly BeerContext _context;
        public UserRepository(BeerContext context) : base(context)
        {
            _context = context;
        }

        public Task<UserEntity> Get(string email)
        {
            return _context.User.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}