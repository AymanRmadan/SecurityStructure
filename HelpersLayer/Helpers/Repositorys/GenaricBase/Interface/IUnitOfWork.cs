using InfrastructureLayer.Entities;

namespace HelpersLayer.Helpers.Repositorys.GenaricBase.Interface
{
    public interface IUnitOfWork
    {

        IRepository<Test, int> TestRepository { get; }
        Task<int> CommitAsync();
        void Dispose();
    }
}
