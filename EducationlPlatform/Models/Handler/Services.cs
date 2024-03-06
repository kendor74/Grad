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


        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _db.ToListAsync();

            return entities;
        }

        public async Task<T> FindById(int id)
        {
            var entity = await _db.FindAsync(id);
            return entity;
        }

        public async Task<T> Create(T entity)
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
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
