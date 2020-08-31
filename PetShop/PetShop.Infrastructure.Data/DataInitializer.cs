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
        private IOwnerRepository _ownerRepository;

        public DataInitializer(IPetRepository petRepository, IOwnerRepository ownerRepository)
        {
            _petRepository = petRepository;
            _ownerRepository = ownerRepository;
        }

        public void InitData()
        {
            Owner owner1 = _ownerRepository.CreateOwner(new Owner() 
            {
                FirstName = "John",
                LastName = "Johnson",
                Address = "Mock Street 1",
                PhoneNumber = "000000000",
                Email = "johnj@mockmail.com"
            });

            Owner owner2 = _ownerRepository.CreateOwner(new Owner()
            {
                FirstName = "Jack",
                LastName = "Jackson",
                Address = "Mock Street 2",
                PhoneNumber = "000000000",
                Email = "jackj@mockmail.com"
            });

            Owner owner3 = _ownerRepository.CreateOwner(new Owner()
            {
                FirstName = "Uncle",
                LastName = "Bob",
                Address = "Mock Street 3",
                PhoneNumber = "000000000",
                Email = "uncleb@mockmail.com"
            });

            Owner owner4 = _ownerRepository.CreateOwner(new Owner()
            {
                FirstName = "Martin",
                LastName = "Fowler",
                Address = "Mock Street 4",
                PhoneNumber = "000000000",
                Email = "martinf@mockmail.com"
            });

            Owner owner5 = _ownerRepository.CreateOwner(new Owner()
            {
                FirstName = "Kent",
                LastName = "Beck",
                Address = "Mock Street 5",
                PhoneNumber = "000000000",
                Email = "kentb@mockmail.com"
            });

            Owner owner6 = _ownerRepository.CreateOwner(new Owner()
            {
                FirstName = "Charles",
                LastName = "Darwin",
                Address = "Mock Street 6",
                PhoneNumber = "000000000",
                Email = "charlesd@mockmail.com"
            });

            _petRepository.CreatePet(new Pet() 
            {
                Name = "Snoopy",
                Type = PetType.Dog,
                BirthDate = DateTime.Parse("01-01-2020"),
                SoldDate = DateTime.Parse("01-06-2020"),
                Color = "White",
                PreviousOwner = owner1,
                Price = 2500
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Garfield",
                Type = PetType.Cat,
                BirthDate = DateTime.Parse("01-12-2019"),
                SoldDate = DateTime.Parse("01-05-2020"),
                Color = "Orange",
                PreviousOwner = owner2,
                Price = 2000
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Clean Cat",
                Type = PetType.Cat,
                BirthDate = DateTime.Parse("01-12-2010"),
                SoldDate = DateTime.Parse("01-05-2012"),
                Color = "White/red",
                PreviousOwner = owner3,
                Price = 3000
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Fowl",
                Type = PetType.Parrot,
                BirthDate = DateTime.Parse("01-12-2016"),
                SoldDate = DateTime.Parse("01-05-2017"),
                Color = "White",
                PreviousOwner = owner4,
                Price = 5000
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Bugs Bunny",
                Type = PetType.Rabbit,
                BirthDate = DateTime.Parse("01-12-2018"),
                SoldDate = DateTime.Parse("01-05-2019"),
                Color = "White/grey",
                PreviousOwner = owner5,
                Price = 1500
            });

            _petRepository.CreatePet(new Pet()
            {
                Name = "Aesop",
                Type = PetType.Turtle,
                BirthDate = DateTime.Parse("15-09-1830"),
                SoldDate = DateTime.Parse("15-09-1835"),
                Color = "Green",
                PreviousOwner = owner6,
                Price = 3500
            });
        }
    }
}
