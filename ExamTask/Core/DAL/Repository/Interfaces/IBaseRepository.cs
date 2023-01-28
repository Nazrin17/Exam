using ExamTask.Models;
using System.Linq.Expressions;

namespace ExamTask.Core.DAL.Repository.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task<List<TEntity>> GetAllAsync( params string[] includes);
        Task<TEntity> GetAsynyc(Expression<Func<TEntity, bool>> exp = null, params string[] includes);
        Task Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
