using Contracts.Interfaces;
using Domain;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Console.Composition
{
    public static class CompositionRoot
    {
        private static IServiceProvider ServiceProvider;

        static CompositionRoot()
        {
            ServiceProvider = Host.CreateDefaultBuilder()
                .ConfigureServices(LoadBindings)
                .Build()
                .Services;
        }

        private static void LoadBindings(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddSingleton<IProcessor, Processor>();
            services.AddSingleton<ICustomerParser, CustomerParser>();
            services.AddSingleton<IDatabaseRepository, DatabaseRepository>();
        }

        public static T Get<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
