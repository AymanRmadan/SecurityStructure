using HelpersLayer.Helpers.Repositorys.GenaricBase.Implementation;
using HelpersLayer.Helpers.Repositorys.Repositories.Interface;
using InfrastructureLayer.Context;
using InfrastructureLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelpersLayer.Helpers.Repositorys.Repositories.Implementaion
{
    public class TestRepository : Repository<Test, int>, ITestRepository
    {
        private readonly DbSet<Test> _test;
        public TestRepository(ApplicationDbContext context) : base(context)
        {
            _test = _dbContext.Set<Test>();
        }
    }
}
