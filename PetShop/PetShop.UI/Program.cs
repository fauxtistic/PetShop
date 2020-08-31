using Microsoft.Extensions.DependencyInjection;
using PetShop.ConsoleApp;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Impl;
using PetShop.Core.DomainService;
using PetShop.Infrastructure.Data;
using System;

namespace PetShop.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IOwnerRepository, OwnerRepository>();
            serviceCollection.AddScoped<IOwnerService, OwnerService>();
            serviceCollection.AddScoped<IMenu, ConsoleMenu>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var consoleMenu = serviceProvider.GetRequiredService<IMenu>();
            var petRepository = serviceProvider.GetRequiredService<IPetRepository>();
            var ownerRepository = serviceProvider.GetRequiredService<IOwnerRepository>();

            var data = new DataInitializer(petRepository, ownerRepository);
            data.InitData();          

            consoleMenu.ConsoleLoop();
        }
    }
}
