using InfrastructureLayer.Entities.Base;
using System.Linq.Expressions;

namespace HelpersLayer.Helpers.Repositorys.GenaricBase.Interface
{
    public interface IRepository<TEntity, TKey>
        where TKey : struct
        where TEntity : Entity<TKey>
    {
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity?> GetByIdAsync(TKey id, params Expression<Func<TEntity, object?>>[] includes);
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object?>>[] includes);
        IQueryable<TEntity> GetAll(int skip, int take, params Expression<Func<TEntity, object?>>[] includes);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? criteria, int? skip, int? take, params Expression<Func<TEntity, object?>>[] includes);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? criteria = null, params Expression<Func<TEntity, object?>>[] includes);
        Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object?>>[] includes);
        Task InsertAsync(TEntity entity);
        Task InsertListAsync(IEnumerable<TEntity> entities);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? criteria = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? criteria = null);
        Task<TEntity?> FindAsync(params object[] id);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object?>>[] includes);
        Task<bool> IsExist(TKey Id);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> items);
        void Delete(TEntity item);


    }
}
