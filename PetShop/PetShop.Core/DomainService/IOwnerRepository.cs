using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {
        public Owner CreateOwner(Owner ownerToCreate);
        public IEnumerable<Owner> GetAllOwners();
        public Owner EditOwner(int index, Owner editedOwner);
        public Owner DeleteOwner(Owner ownerToDelete, out bool success);
    }
}
