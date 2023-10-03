namespace Infrastructure.Repositories.Persistence.Common
{
    using Application.Contracts.Repositories.Base;
    using Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System.Linq.Expressions;
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicado, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AnyAsync(predicado, cancellationToken);
        }
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicado, CancellationToken cancellationToken)
        {

            return await _context.Set<T>().Where(predicado).ToListAsync(cancellationToken);
        }
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicado)
        {
            return await _context.Set<T>().Where(predicado).ToListAsync();
        }
        /*
         Permite hacer la consulta a solo 2 entidades
         */
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicado = null,
                                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                    string? includeString = null,
                                                    bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString!);
            if (predicado != null) query = query.Where(predicado);
            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicado = null,
                                                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                       List<Expression<Func<T, object>>>? includes = null,
                                                       bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicado != null) query = query.Where(predicado);
            if (orderBy != null) return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicado = null,
                                                List<Expression<Func<T, object>>>? includes = null,
                                                bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicado != null) query = query.Where(predicado);
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }
        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void UpdateRange(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }


        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public void DeleteRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public void DeleteAll()
        {
            var entities = _context.Set<T>().ToList();
            DeleteRange(entities);
        }
    }
}
