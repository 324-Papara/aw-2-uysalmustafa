using Para.Base.Entity;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Para.Data.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task Save();
    Task<TEntity?> GetById(long Id);
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
    Task Delete(long Id);
    Task<List<TEntity>> GetAll();
    Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> Include(params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> WhereWithIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

}