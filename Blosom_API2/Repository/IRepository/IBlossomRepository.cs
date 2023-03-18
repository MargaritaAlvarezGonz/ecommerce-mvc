using Blossom_API.Models;

namespace Blosom_API2.Repository.IRepository
{
    public interface IBlossomRepository : IRepository<Blossom>
    {
        Task<Blossom> Update(Blossom entity);
    }
}
