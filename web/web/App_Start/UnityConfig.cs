using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Entity;
using Web.Repositories.Interface;
using Web.Repositories.Repositories.Account;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Utitlities;
using Web.Services.Services;
using Web.Services.Services.Account;

namespace web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IDapperManager, DapperManager>();
            container.RegisterType<IDatabaseManager, DatabaseManager>();

            //base repo
            container.RegisterType<IBaseRepo<Unit>, BaseRepo<Unit>>();
            container.RegisterType<IBaseRepo<Product>, BaseRepo<Product>>();
            container.RegisterType<IBaseRepo<ProductPrice>, BaseRepo<ProductPrice>>();
            container.RegisterType<IBaseRepo<ProductImage>, BaseRepo<ProductImage>>();


            //utilities
            container.RegisterType<IMessageClass, MessageClass>();

            //Repositories
            container.RegisterType<IMenusRepository, MenusRepository>();
            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IBaseInterface, BaseInterface>();
            container.RegisterType<IStaffsRepository, StaffsRepository>();
            container.RegisterType<IPhotoStorageRepository, PhotoStorageRepository>();
            container.RegisterType<IAdministrationRepository, AdministrationRepository>();
            container.RegisterType<IUnitRepository, UnitRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();


            //services
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IMenusService, MenusService>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IStaffsService, StaffsService>();
            container.RegisterType<IAdministrationService, AdministrationService>();
            container.RegisterType<IUnitService, UnitService>();
            container.RegisterType<IProductService, ProductService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}