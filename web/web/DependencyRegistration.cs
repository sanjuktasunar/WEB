using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.Lifetime;
using Web.Repositories.Repositories.Administration;
using Web.Services.Services;

namespace web
{
    public class DependencyRegistration
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterType<IMenusRepository, MenusRepository>(new HierarchicalLifetimeManager())
                     .RegisterType<IMenusService, MenusService>(new HierarchicalLifetimeManager());
                     
        }
    }
}