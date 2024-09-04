using HelpersLayer.Helpers.Repositorys.GenaricBase.Implementation;
using HelpersLayer.Helpers.Repositorys.GenaricBase.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace HelpersLayer.Helpers.Repositorys
{
    public static class RepositoriesDependencies
    {

        public static IServiceCollection AddRepositoriesDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
