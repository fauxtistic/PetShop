using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class DataInitializer
    {
        private IPetRepository _petRepository;

        public DataInitializer(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public void InitData()
        {
            _petRepository.CreatePet(new Pet() 
            {
                Name = "Snoopy",
                Type = PetType.Dog,
                BirthDate = DateTime.Parse("01-01-2020"),
                SoldDate = DateTime.Parse("01-06-2020"),
                Color = "White",
                PreviousOwner = "John Johnson",
                Price = 2500
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Garfield",
                Type = PetType.Cat,
                BirthDate = DateTime.Parse("01-12-2019"),
                SoldDate = DateTime.Parse("01-05-2020"),
                Color = "Orange",
                PreviousOwner = "Jack Jackson",
                Price = 2000
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Clean Cat",
                Type = PetType.Cat,
                BirthDate = DateTime.Parse("01-12-2010"),
                SoldDate = DateTime.Parse("01-05-2012"),
                Color = "White/red",
                PreviousOwner = "Uncle Bob",
                Price = 3000
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Fowl",
                Type = PetType.Parrot,
                BirthDate = DateTime.Parse("01-12-2016"),
                SoldDate = DateTime.Parse("01-05-2017"),
                Color = "White",
                PreviousOwner = "Martin Fowler",
                Price = 5000
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Bugs Bunny",
                Type = PetType.Rabbit,
                BirthDate = DateTime.Parse("01-12-2018"),
                SoldDate = DateTime.Parse("01-05-2019"),
                Color = "White/grey",
                PreviousOwner = "Gang of Four",
                Price = 1500
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Aesop",
                Type = PetType.Turtle,
                BirthDate = DateTime.Parse("15-09-1830"),
                SoldDate = DateTime.Parse("15-09-1835"),
                Color = "Green",
                PreviousOwner = "Charles Darwin",
                Price = 3500
            });
        }
    }
}
