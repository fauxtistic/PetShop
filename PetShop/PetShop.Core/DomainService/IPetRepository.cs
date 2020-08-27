using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        public Pet CreatePet(Pet pet);
        public IEnumerable<Pet> ReadAllPets();
        public Pet EditPet(int index, Pet editedPet);
        public Pet DeletePet(Pet pet, out bool success);
    }
}
