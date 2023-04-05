using Blosom_API2.Data;
using Blosom_API2.Models;
using Blosom_API2.Repository.IRepository;
using Blossom_API.Models;

namespace Blosom_API2.Repository
{
    public class NumberBlossomRepository : Repository<NumberBlossom>, INumberBlossomRepository
    {
        private readonly ApplicationDbContext _db;
        public NumberBlossomRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
            
        }
        public async Task<NumberBlossom> Update(NumberBlossom entity)
        {
            entity.DateUpdate = DateTime.Now;
            _db.NumberBlossoms.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public Task<NumberBlossom> Update(Blossom entity)
        {
            throw new NotImplementedException();
        }

        Task INumberBlossomRepository.Update(NumberBlossom model)
        {
            throw new NotImplementedException();
        }
    }
}
