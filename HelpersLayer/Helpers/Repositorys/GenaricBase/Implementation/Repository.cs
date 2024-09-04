using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;
using InfrastructureLayer.Context;
using InfrastructureLayer.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HelpersLayer.Helpers.Repositorys.GenaricBase.Implementation
{
    public class Repository<TEntity, Key> : IRepository<TEntity, Key>
       where Key : struct
       where TEntity : Entity<Key>
    {
        #region Variables
        protected readonly ApplicationDbContext _dbContext;

        protected readonly DbSet<TEntity> _table;
        #endregion

        #region CTOR
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity>();
        }

        #endregion

        #region Async Function

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object?>>[] includes)
        {
            var query = GetWhere(criteria, includes);
            return await query.FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object?>>[] includes)
        {
            var query = Includes(_table, includes);

            return query;
        }
        public IQueryable<TEntity> GetAll(int skip, int take, params Expression<Func<TEntity, object?>>[] includes)
        {
            var query = Includes(_table, includes);

            query = query.Skip(skip).Take(take);

            return query;
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? criteria, int? skip, int? take, params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _table;

            if (criteria != null)
                query = query.Where(criteria);

            query = Includes(query, includes);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return query;
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>>? criteria = null, params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _table;

            if (criteria != null)
                query = query.Where(criteria);

            query = Includes(query, includes);

            return query;
        }
        public Task<TEntity?> GetByIdAsync(Key id)
        {
            return _table.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        public Task<TEntity?> GetByIdAsync(Key id, params Expression<Func<TEntity, object?>>[] includes)
        {
            var query = Includes(_table, includes);
            return query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
        public Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object?>>[] includes)
        {
            return GetWhere(criteria, includes).FirstOrDefaultAsync();
        }
        public async Task InsertAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }
        public async Task InsertListAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? criteria = null)
        {
            return criteria == null ? await _table.AnyAsync() : await _table.AnyAsync(criteria);
        }
        public async Task<bool> IsExist(Key Id)
        {
            return await _table.AnyAsync(e => e.Id.Equals(Id));
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? criteria = null)
        {
            return criteria == null ? await _table.CountAsync() : await _table.CountAsync(criteria);
        }


        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);

        }
        public void UpdateRange(IEnumerable<TEntity> items)
        {
            _dbContext.UpdateRange(items);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
        #endregion

        #region Private functions

        private IQueryable<TEntity> Includes(IQueryable<TEntity> query, params Expression<Func<TEntity, object?>>[] includes)
        {
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query;
        }
        private IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object?>>[] includes)
        {
            var query = Includes(_table, includes);

            return query.Where(criteria);
        }

        public async Task<TEntity?> FindAsync(params object[] id)
        {
            return await _table.FindAsync(id);
        }



        #endregion
    }

}
