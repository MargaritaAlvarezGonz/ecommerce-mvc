using Blosom_API2.Data;
using Blosom_API2.Repository.IRepository;
using Blossom_API.Models;

namespace Blosom_API2.Repository
{
    public class BlossomRepository : Repository<Blossom>, IBlossomRepository
    {
        private readonly ApplicationDbContext _db;
        public BlossomRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
            
        }
        public async Task<Blossom> Update(Blossom entity)
        {
            entity.DateUpdated = DateTime.Now;
            _db.Blossoms.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
