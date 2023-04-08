using Blosom_API2.Data;
using Blosom_API2.Models.Specifications;
using Blosom_API2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blosom_API2.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db) 
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Delete(T entity)
        {
           dbSet.Remove(entity);
            await Save();
        }

        public async Task<T> Get(Expression<Func<T, bool>> filtro = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public Task<T> Get(Expression<Func<T, bool>> filtro = null, bool tracked = true)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<T>> GetAll(Expression<Func<T, bool>> filtro = null, string includeProperties = null)
        //{
        //    IQueryable<T> query = dbSet;
        //    if (filtro != null)
        //    {
        //        query = query.Where(filtro);
        //    }
        //    if (includeProperties != null)
        //    {
        //        foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(includeProp);
        //        }
        //    }
        //    return await query.ToListAsync();
        //}

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filtro = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public PagedList<T> GetAllPaginated(Parameters parameters, Expression<Func<T, bool>> filtro = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return PagedList<T>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public async Task Post(T entity)
        {
            await dbSet.AddAsync(entity);
            await Save();
        }

        public async Task Save()
        {
           await _db.SaveChangesAsync();
        }
    }
}
