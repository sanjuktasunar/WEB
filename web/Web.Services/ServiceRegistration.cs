using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Web.Services
{
    public class ServiceRegistration
    {
        public static void Configure(IUnityContainer container)
        {
            //container.RegisterType<IEmailService, EmailService>(new HierarchicalLifetimeManager());
        }
    }
}
