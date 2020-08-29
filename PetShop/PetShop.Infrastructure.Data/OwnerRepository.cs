using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        private IEnumerable<Owner> _owners;
        private int _counter;

        public OwnerRepository()
        {
            _owners = FakeDB.owners;
            _counter = FakeDB.ownerCounter;
        }

        public Owner CreateOwner(Owner ownerToCreate)
        {
            ownerToCreate.OwnerId = _counter++;
            ((List<Owner>)_owners).Add(ownerToCreate);
            return ownerToCreate;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _owners;
        }
        public Owner EditOwner(int index, Owner editedOwner)
        {
            ((List<Owner>)_owners)[index] = editedOwner;
            return editedOwner;
        }
        public Owner DeleteOwner(Owner ownerToDelete, out bool success)
        {
            success = ((List<Owner>)_owners).Remove(ownerToDelete);
            return ownerToDelete;
        }
    }
}
