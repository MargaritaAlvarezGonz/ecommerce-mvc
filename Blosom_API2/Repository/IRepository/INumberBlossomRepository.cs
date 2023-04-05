using Blosom_API2.Models;
using Blossom_API.Models;

namespace Blosom_API2.Repository.IRepository
{
    public interface INumberBlossomRepository : IRepository<NumberBlossom>
    {
        Task<NumberBlossom> Update(Blossom entity);
        Task Update(NumberBlossom model);
    }
}
