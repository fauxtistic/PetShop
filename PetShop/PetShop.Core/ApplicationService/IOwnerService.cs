using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IOwnerService
    {
        public Owner NewOwner(Owner newOwner);
        public Owner CreateOwner(Owner ownerToCreate);
        public List<Owner> GetAllOwners();
        public Owner GetOwnerById(int id);
        public Owner EditOwner(Owner editedOwner);
        public Owner DeleteOwner(Owner ownerToDelete, out bool success);
    }
}
