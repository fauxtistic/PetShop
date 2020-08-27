using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        public Pet NewPet(Pet pet);
        public Pet CreatePet(Pet pet);
        public List<Pet> GetAllPets();
        public List<Pet> GetAllPetsOfType(PetType type);
        public List<Pet> GetAllPetsSortedByPrice(bool ascending);
        public List<Pet> GetNumberOfMostExpensivePets(bool ascending, int number);
        public Pet GetPetById(int id);
        public Pet EditPet(Pet editedPet);
        public Pet DeletePet(Pet pet, out bool success);
    }
}
