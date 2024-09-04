//using Madina.CrossCutting.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace HelpersLayer
{
    public static class HelpersLayerDependencies
    {
        public static IServiceCollection AddHelpersLayerDependenciesDependencies(this IServiceCollection services)
        {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));


            //#region Validation Registeration

            //foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            //{
            //    services.AddValidatorsFromAssembly(item);

            //}


            //#endregion


            return services;
        }
    }
}
