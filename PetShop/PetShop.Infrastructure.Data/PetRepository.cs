using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        private IEnumerable<Pet> _pets;
        private int _counter;

        public PetRepository()
        {
            _pets = FakeDB.pets; //consider removing FakeDB altogether
            _counter = FakeDB.counter;            
        }

        public Pet CreatePet(Pet pet)
        {
            pet.PetId = _counter++;
            ((List<Pet>)_pets).Add(pet);
            return pet;
        }

        public IEnumerable<Pet> ReadAllPets()
        {
            return _pets;
        }

        //should be changed
        public Pet EditPet(int index, Pet editedPet)
        {
            ((List<Pet>)_pets).RemoveAt(index);
            ((List<Pet>)_pets).Insert(index, editedPet);
            return ((List<Pet>)_pets)[index];
        }

        public Pet DeletePet(Pet pet, out bool success)
        {
            //_counter--;
            success = ((List<Pet>)_pets).Remove(pet);
            return pet;
        }

        
    }
}
