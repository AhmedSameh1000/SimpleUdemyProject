using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UdemyProject.Contract.Helpers;
using UdemyProject.Contract.RepositoryContracts;
using UdemyProject.Infrastructure.DbContext;
using X.PagedList;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> GetAllAsNoTracking(string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsNoTracking().AsQueryable();
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }

            return await Query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsTracking(string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsQueryable();
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }

            return await Query.ToListAsync();
        }

        public async Task<IPagedList<T>> GetAllAsTracking(RequestParams requestParams, string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>();
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }

            return await Query.ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsQueryable();
            Query = Query.Where(filter);
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }

            return await Query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsNoTracking(Expression<Func<T, bool>> filter, string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsQueryable();
            Query = Query.Where(filter);
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }
            return await Query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsTracking(Expression<Func<T, bool>> filter, string[] InclueProperties = null)
        {
            IQueryable<T> Query = _dbContext.Set<T>().AsQueryable();
            Query = Query.Where(filter);
            if (InclueProperties != null)
            {
                foreach (var includeProperty in InclueProperties)
                {
                    Query = Query.Include(includeProperty.Trim());
                }
            }
            return await Query.ToListAsync();
        }

        public void Remove(T Entity)
        {
            _dbContext.Set<T>().Remove(Entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task<bool> SaveChanges()
        {
            var RowsEfected = await _dbContext.SaveChangesAsync();
            return RowsEfected > 0 ? true : false;
        }

        public async Task<int> GetCount()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public void RemoveRange(IEnumerable<T> Entities)
        {
            _dbContext.Set<T>().RemoveRange(Entities);
        }
    }
}