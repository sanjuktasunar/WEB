using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Repositories.Interface;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Utitlities;
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

            //utilities
            container.RegisterType<IMessageClass, MessageClass>();

            //Repositories
            container.RegisterType<IMenusRepository, MenusRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IBaseInterface, BaseInterface>();
            container.RegisterType<IStaffsRepository, StaffsRepository>();
            container.RegisterType<IPhotoStorageRepository, PhotoStorageRepository>();
            container.RegisterType<IAdministrationRepository, AdministrationRepository>();


            //services
            container.RegisterType<IMenusService, MenusService>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IStaffsService, StaffsService>();
            container.RegisterType<IAdministrationService, AdministrationService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}