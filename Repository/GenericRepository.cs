using Hotel_Listings.Data;
using Hotel_Listings.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel_Listings.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext databaseContext;
        private readonly DbSet<T> db;

        public GenericRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            db = databaseContext.Set<T>();
        }
        public async Task Delete(int id)
        {
            var entity = await db.FindAsync(id);
            db.Remove(entity);
        }

        public async void DeleteRange(IEnumerable<T> entities)
        {
            db.RemoveRange(entities);

        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = db;
            if (includes != null)
            {
                foreach (var includedproperty in includes)
                {
                    query = query.Include(includedproperty);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = db;
            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includedproperty in includes)
                {
                    query = query.Include(includedproperty);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        //public async Task<IPagedList<T>> GetPagedList(RequestParam requestParam, List<string> includes = null)
        //{
        //    IQueryable<T> query = db;


        //    if (includes != null)
        //    {
        //        foreach (var includedproperty in includes)
        //        {
        //            query = query.Include(includedproperty);
        //        }
        //    }

        //    return await query.AsNoTracking().ToPagedListAsync(requestParam.PageNumber, requestParam.PageSize);
        //}

        public async Task Insert(T Entity)
        {
            await db.AddAsync(Entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            db.Attach(entity);
            databaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
