using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PetShop.Core.ApplicationService.Impl
{
    public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }
        public Owner NewOwner(Owner newOwner)
        {
            string errorMessage = "";

            if (!new Regex("^[a - zA - Z] + (([\\'\\,\\.\\-][a-zA-Z])?[a-zA-Z]*)*$").IsMatch(newOwner.FirstName))
            {
                errorMessage += "First name must be a single name (use - to separate) consisting of letters\n";
            }
            if (!new Regex("^[a - zA - Z] + (([\\'\\,\\.\\-][a-zA-Z])?[a-zA-Z]*)*$").IsMatch(newOwner.LastName))
            {
                errorMessage += "Last name must be a single name (use - to separate) consisting of letters\n";
            }
            if (!new Regex("^((.){1,}(\\d){1,}(.){0,})$").IsMatch(newOwner.Address))
            {
                errorMessage += "Address must be in format street, number, with additions allowed after\n";
            }
            if (!new Regex("^([\\+][0-9]{1,3}([ \\.\\-])?)?([\\(]{1}[0-9]{3}[\\)])?([0-9A-Z \\.\\-]{1,32})((x|ext|extension)?[0-9]{1,4}?)$").IsMatch(newOwner.PhoneNumber))
            {
                errorMessage += "Phone number is not in valid format\n";
            }
            if (!new Regex("[\\w-]+@([\\w-]+\\.)+[\\w-]+").IsMatch(newOwner.Address))
            {
                errorMessage += "Email address is not in valid format\n";
            }

            if (errorMessage.Length > 0)
            {
                throw new ArgumentException(errorMessage);
            }

            return newOwner;
        }

        public Owner CreateOwner(Owner ownerToCreate)
        {
            return _ownerRepository.CreateOwner(ownerToCreate);
        }

        public List<Owner> GetAllOwners()
        {
            return _ownerRepository.GetAllOwners().ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return GetAllOwners().Find(owner => owner.OwnerId == id);
        }
        public Owner EditOwner(Owner editedOwner)
        {
            var index = GetAllOwners().FindIndex(owner => owner.OwnerId == editedOwner.OwnerId);
            return _ownerRepository.EditOwner(index, editedOwner);
        }

        public Owner DeleteOwner(Owner ownerToDelete, out bool success)
        {
            return _ownerRepository.DeleteOwner(ownerToDelete, out success);
        }
    }
}
