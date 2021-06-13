using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Entity;
using Web.Repositories.Interface;
using Web.Repositories.Repositories;
using Web.Repositories.Repositories.Account;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Repositories.Customer;
using Web.Repositories.Repositories.Members;
using Web.Repositories.Utitlities;
using Web.Services.Interface;
using Web.Services.Services;
using Web.Services.Services.Account;
using Web.Services.Services.Administration;
using Web.Services.Services.Customer;
using Web.Services.Services.Members;

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
            container.RegisterType<IBaseRepo<Role>, BaseRepo<Role>>();
            container.RegisterType<IBaseRepo<Designation>, BaseRepo<Designation>>();
            container.RegisterType<IBaseRepo<Department>, BaseRepo<Department>>();
            container.RegisterType<IBaseRepo<Supplier>, BaseRepo<Supplier>>();
            container.RegisterType<IBaseRepo<OrganizationInfo>, BaseRepo<OrganizationInfo>>();
            container.RegisterType<IBaseRepo<CustomerQuery>, BaseRepo<CustomerQuery>>();
            container.RegisterType<IBaseRepo<AccountHead>, BaseRepo<AccountHead>>();
            container.RegisterType<IBaseRepo<FiscalYear>, BaseRepo<FiscalYear>>();


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
            container.RegisterType<IDesignationRepository, DesignationRepository>();
            container.RegisterType<IRoleRepository, RoleRepository>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IOrganizationInfoRepository, OrganizationInfoRepository>();
            container.RegisterType<ICustomerQueryRepository, CustomerQueryRepository>();
            container.RegisterType<IAccountHeadRepository, AccountHeadRepository>();
            container.RegisterType<ISupplierRepository, SupplierRepository>();
            container.RegisterType<IFiscalYearRepository, FiscalYearRepository>();
            container.RegisterType<IDataRepository, DataRepository>();
            container.RegisterType<IMemberRepository, MemberRepository>();


            //services
            container.RegisterType<IImageService, ImageService>();
            container.RegisterType<IMenusService, MenusService>();
            container.RegisterType<IUsersService, UsersService>();
            container.RegisterType<IStaffsService, StaffsService>();
            container.RegisterType<IAdministrationService, AdministrationService>();
            container.RegisterType<IUnitService, UnitService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IDesignationService, DesignationService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<IOrganizationInfoService, OrganizationInfoService>();
            container.RegisterType<ICustomerQueryService, CustomerQueryService>();
            container.RegisterType<IAccountHeadService, AccountHeadService>();
            container.RegisterType<ISupplierService, SupplierService>();
            container.RegisterType<IFiscalYearService, FiscalYearService>();
            container.RegisterType<IDateService, DateService>();
            container.RegisterType<IDataService, DataService>();
            container.RegisterType<IMemberService, MemberService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}