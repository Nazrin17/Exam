using ExamTask.Core.DAL.Repository.Interfaces;
using ExamTask.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExamTask.Core.DAL.Repository.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class,new()
    {
        private readonly ExamDbContext _context;

        public BaseRepository(ExamDbContext context)
        {
            _context = context;
        }

        public async Task Create(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
            _context.SaveChanges(); 
            
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
           IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return exp == null ? await query.ToListAsync():await query.Where(exp).ToListAsync();
        }

        public  async Task<List<TEntity>> GetAllAsync(params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetAsynyc(Expression<Func<TEntity, bool>> exp = null, params string[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return await query.Where(exp).FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
    }
}
