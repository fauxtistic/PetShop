using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PetShop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        private IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        //Validates new pet objects
        public Pet NewPet(Pet pet)
        {
            string errorMessage = "";
            if (pet.Name.Length < 2)
            {
                errorMessage += "Name of pet must be at least two characters\n";
            }
            if (pet.BirthDate > DateTime.Now)
            {
                errorMessage += "Birth date of the pet cannot be in the future\n";
            }
            if (pet.SoldDate > pet.BirthDate)
            {
                errorMessage += "Last selling date of the pet cannot predate it's birth\n";
            }
            if (pet.Color.Length < 3)
            {
                errorMessage += "Name of color must be at least three characters\n";
            }
            if (pet.PreviousOwner.Length < 2)
            {
                errorMessage += "Name of previous owner must be at least two characters\n";
            }
            if (pet.Price < 0)
            {
                errorMessage += "Price of pet cannot be negative\n";
            }
            if (errorMessage.Length > 0)
            {
                throw new ArgumentException(errorMessage);
            }
            return pet;
        }
        public Pet CreatePet(Pet pet)
        {
            return _petRepository.CreatePet(pet);
        }

        public List<Pet> GetAllPets()
        {
            return _petRepository.ReadAllPets().ToList();
        }

        public List<Pet> GetAllPetsOfType(PetType type)
        {
            return _petRepository.ReadAllPets().Where(pet => pet.Type == type).ToList();
        }

        public List<Pet> GetAllPetsSortedByPrice(bool ascending)
        {
            if (ascending)
            {
                return _petRepository.ReadAllPets().OrderBy(pet => pet.Price).ToList();
            }
            else
            {
                return _petRepository.ReadAllPets().OrderByDescending(pet => pet.Price).ToList();
            }
            
        }

        public List<Pet> GetNumberOfMostExpensivePets(bool ascending, int number)
        {
            if (ascending)
            {
                return _petRepository.ReadAllPets().OrderBy(pet => pet.Price).Take(number).ToList();
            }
            else
            {
                return _petRepository.ReadAllPets().OrderByDescending(pet => pet.Price).Take(number).ToList();
            }
            
        }

        public Pet GetPetById(int id)
        {
            return GetAllPets().Find(pet => pet.PetId == id);
        }

        //should be changed
        public Pet EditPet(Pet editedPet)
        {
            var index = GetAllPets().FindIndex(pet => pet.PetId == editedPet.PetId);
            return _petRepository.EditPet(index, editedPet);
           
            //Pet petToChange = GetPetById(editedPet.PetId);
            //petToChange.Name = editedPet.Name;
            //petToChange.Type = editedPet.Type;
            //petToChange.BirthDate = editedPet.BirthDate;
            //petToChange.SoldDate = editedPet.SoldDate;
            //petToChange.Color = editedPet.Color;
            //petToChange.PreviousOwner = editedPet.PreviousOwner;
            //petToChange.Price = editedPet.Price;
            //return petToChange;
        }

        public Pet DeletePet(Pet pet, out bool success)
        {
            return _petRepository.DeletePet(pet, out success);
        }

    }
}
