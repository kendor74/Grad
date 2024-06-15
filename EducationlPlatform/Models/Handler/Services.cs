using Microsoft.EntityFrameworkCore;

namespace EducationlPlatform.Models.Handler
{
    public class Services<T> where T : class
    {
        private readonly IdentityUserDbContext _context;
        private readonly DbSet<T> _db;

        public Services(IdentityUserDbContext context)
        {
            _context = context;
            _db = context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>> includeProperties = null)
        {
            IQueryable<T> query = _db;

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            return query.ToList();
        }

        public async Task<T> FindById(int id, Func<IQueryable<T>, IQueryable<T>> includeProperties = null)
        {
            IQueryable<T> query = _db;

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            return query.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<T> Create(T entity, params string[] navigationProperties)
        {
            await _db.AddAsync(entity);
            foreach (var navigationProperty in navigationProperties)
            {
                _context.Entry(entity).Reference(navigationProperty).Load();
            }

            await _context.SaveChangesAsync();

            // Reload the entity with included navigation properties
            _context.Entry(entity).Reload();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await FindById(id);

            if (entity != null)
            {
                _db.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<T> Update(int id, T entity)
        {
            T existing = await FindById(id);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }

            return entity;
        }

        public async Task<bool> IsExist(int id)
        {
            var entity = await FindById(id);
            return (entity != null) ? true : false;
        }

        public async Task<int> Count()
        {
            return await _db.CountAsync();
        }


        public IEnumerable<T> Search(Func<T, bool> filter)
        {
            return _db.Where(filter);
        }


    }
}
