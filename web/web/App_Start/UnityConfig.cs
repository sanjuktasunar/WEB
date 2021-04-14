using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Web.Database;
using Web.Repositories.Repositories.Administration;
using Web.Services.Services;

namespace web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IDapperManager, DapperManager>();
            container.RegisterType<IDatabaseManager, DatabaseManager>();

            //Repositories
            container.RegisterType<IMenusRepository, MenusRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();


            //services
            container.RegisterType<IMenusService, MenusService>();
            container.RegisterType<IUsersService, UsersService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}