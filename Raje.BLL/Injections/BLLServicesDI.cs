using Raje.BLL.ConfigAutoMapper;
using Raje.DL.Services.BLL.Base;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Raje.BLL.Injections
{
    public class BLLServicesDI
    {
        public static void Config(IServiceCollection servicesContainer)
        {
            servicesContainer.AddSingleton(ConfigMapper.Configure());
            servicesContainer.AddAutoMapper(typeof(IDependencyInjectionService).Assembly);

            // Get all types in BLL assembly (dll)
            IEnumerable<Type> allTypes = typeof(BLLServicesDI)
                .GetTypeInfo()
                .Assembly
                .GetTypes();

            // Get all implemented types derived from interfaces of type IDependencyInjectionService
            IEnumerable<Type> services = allTypes.Where(type =>
                type.GetInterfaces().Contains(typeof(IDependencyInjectionService)) &&
                !type.IsAbstract &&
                // Get only implemented types
                !type.IsInterface
            );

            // For each service register in Dependency Injection container
            foreach (Type service in services)
            {
                // Get all interfaces of the service
                IEnumerable<Type> interfaces = service.GetInterfaces();

                // Iterate each interface of implemented type
                foreach (var item in interfaces)
                {
                    servicesContainer.AddTransient(item, service);
                }
            }
        }
    }
}
