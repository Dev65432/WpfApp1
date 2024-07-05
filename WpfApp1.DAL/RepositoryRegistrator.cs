using WpfApp1.DAL.Entityes;

using Microsoft.Extensions.DependencyInjection;

namespace WpfApp1.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
           .AddTransient<IRepository<Deal>, DealsRepository>()           
            .AddTransient<IRepository<Picture>, PicturesRepository>()



        ;
    }
}
