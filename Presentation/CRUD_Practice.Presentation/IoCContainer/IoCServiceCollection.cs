using Autofac;
using Autofac.Extensions.DependencyInjection;
using CRUD_Practice.Bussines.Interfaces;
using CRUD_Practice.Bussines.Services;
using CRUD_Practice.Infrastructure.Cliente;
using CRUD_Practice.Infrastructure.Interfaces;
using CRUD_Practice.Infrastructure.Repositories;

namespace CRUD_Practice.IoCContainer
{
    public static class IoCServiceCollection
    {
        public static ContainerBuilder BuildContext(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            return BuildContext(builder, configuration);
        }

        public static ContainerBuilder BuildContext(this ContainerBuilder builder, IConfiguration configuration)
        {
            RegisterClients(builder, configuration);
            RegisterRepositories(builder, configuration);
            RegisterServices(builder, configuration);
            return builder;
        }

        private static void RegisterClients(ContainerBuilder builder, IConfiguration configuration)
        {
            builder
                .Register((context, parameters) =>
                {
                    var connectionString = configuration["ConnectionString"];
                    return new DBClient(connectionString);
                })
           .Named<IDBClient>("DBClient")
           .SingleInstance();

        }

        private static void RegisterRepositories(ContainerBuilder builder, IConfiguration configuration)
        {

            builder
                .Register((context, parameters) =>
                {
                    var dbClient = context.ResolveNamed<IDBClient>("DBClient");
                    return new DBRepository(dbClient);
                })
                .As<IDBRepository>()
                .SingleInstance();

        }

        private static void RegisterServices(ContainerBuilder builder, IConfiguration configuration)
        {
            builder
                .Register((context, parameters) => new UserService(
                    context.Resolve<IDBRepository>()
                    ))
                .As<IUserService>()
                .SingleInstance();
        }


    }
}
