using Autofac;
using Autofac.Extensions.DependencyInjection;
using CRUD_Practice.Bussines.Interfaces;
using CRUD_Practice.Bussines.Services;
using CRUD_Practice.Infrastructure.Cliente;
using CRUD_Practice.Infrastructure.Data;
using CRUD_Practice.Infrastructure.Interfaces;
using CRUD_Practice.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Practice.IoCContainer
{
    public static class IoCServiceCollection
    {
        public static ContainerBuilder BuildContext(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Populate(services);
            return BuildContext(builder, configuration);
        }

        public static ContainerBuilder BuildContext(this ContainerBuilder builder, IConfiguration configuration)
        {
            
            RegisterRepositories(builder, configuration);
            RegisterServices(builder, configuration);
            return builder;
        }

        private static void RegisterRepositories(ContainerBuilder builder, IConfiguration configuration)
        {

            builder
                .RegisterType<DBRepositoryEF>().As<IDBRepositoryEF>().InstancePerLifetimeScope();

        }

        private static void RegisterServices(ContainerBuilder builder, IConfiguration configuration)
        {
            builder
                .RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        }


    }
}
