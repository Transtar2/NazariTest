using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using NazariTest.Application.AutoFac;
using NazariTest.Application.Interfaces;
using NazariTest.Persistence.Data;

namespace NazariTest.Persistence.AutoFac
{
    public static class AutofacConfigurationExtensions
    {
        public static void AddAutofacDependencyServices(this ContainerBuilder containerBuilder)
        {
            var currentAssembly = Assembly.Load("NazariTest.Persistence");
            var coreAssembly = Assembly.Load("NazariTest.Application");
            containerBuilder
                .RegisterAssemblyTypes(new[] { currentAssembly, coreAssembly })
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            containerBuilder
                .RegisterAssemblyTypes(new[] { currentAssembly, coreAssembly })
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
            containerBuilder
                .RegisterAssemblyTypes(new[] { currentAssembly, coreAssembly })
                .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
                .SingleInstance();
            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
        }
    }
}
