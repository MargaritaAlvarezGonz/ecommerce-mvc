using System.Linq.Expressions;

namespace Blosom_API2.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Post (T entity);
        Task<List<T>> GetAll(Expression<Func<T,bool>>? filtro =null);
        Task<T> Get(Expression<Func<T, bool>>? filtro = null, bool tracked=true);
        Task Delete(T entity);

        Task Save();
    }
}
