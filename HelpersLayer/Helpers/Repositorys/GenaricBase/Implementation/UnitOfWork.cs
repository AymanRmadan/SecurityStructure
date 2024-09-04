using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;
using InfrastructureLayer.Context;
using InfrastructureLayer.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HelpersLayer.Helpers.Repositorys.GenaricBase.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;


        public IRepository<Test, int> TestRepository { get; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
            TestRepository = new Repository<Test, int>(dbContext);

        }
        public EntityEntry Attach(object entity)
        {
            return _dbContext.Attach(entity);
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
