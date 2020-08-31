using PetShop.Core;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PetShop.ConsoleApp
{
    public class OwnerMenu : Menu
    {        
        private IPetService _petService;
        private IOwnerService _ownerService;        

        public OwnerMenu(IPetService petService, IOwnerService ownerService)
        {
            CloseOption = 5;
            MenuOptions = new string[]
            {
                "Show all owners",
                "Create a new owner entry",
                "Edit an owner entry",
                "Delete an owner entry",
                "Back to main menu"
            };
            _petService = petService;
            _ownerService = ownerService;
        }

        public override void ProcessOption(int option)
        {
            switch(option)
            {
                case 1:
                    Console.WriteLine("Showing a list of all owners:");
                    ShowItems(_ownerService.GetAllOwners());
                    break;
                case 2:
                    CreateOwner();
                    break;
                case 3:
                    EditOwner();
                    break;
                case 4:
                    DeleteOwner();
                    break;
                case 5:
                    Console.WriteLine("Going back to main menu...");
                    break;
                default:
                    Console.WriteLine("Error, option not defined");
                    break;
            }
        }

        private void CreateOwner()
        {
            Console.WriteLine("You are creating a new owner entry");
            try
            {
                Owner createdOwner = _ownerService.CreateOwner(NewOwner());
                Console.WriteLine($"Created owner: {createdOwner}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
        }

        private void EditOwner()
        {
            Console.WriteLine("Enter the id of the owner entry you want to edit:");
            Owner foundOwner = GetOwnerById();
            if (foundOwner == null)
            {
                Console.WriteLine("There is no owner with this id!");
            }
            else
            {
                Console.WriteLine($"You are editing this entry: {foundOwner}");
                try
                {
                    Owner editedOwner = NewOwner();
                    editedOwner.OwnerId = foundOwner.OwnerId; //alternative pass id to NewOwner
                    Owner changedOwner = _ownerService.EditOwner(editedOwner);                    
                    Console.WriteLine($"Owner entry after being edited:\n{changedOwner}\n"); //add text about how entry previously looked
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private void DeleteOwner()
        {
            Console.WriteLine("Warning: pets associated with owner will also be deleted\nEnter the id of the owner entry you want to delete:");
            Owner foundOwner = GetOwnerById();
            if (foundOwner == null)
            {
                Console.WriteLine("There is no owner with this id!");
            }
            else
            {
                bool success;
                Owner deletedOwner = _ownerService.DeleteOwner(foundOwner, out success);
                Console.WriteLine($"The operation to remove:\n{deletedOwner}\n" + ((success) ? "\n...was successful!" : "\n...was NOT successful!"));
                List<Pet> pets = _petService.GetAllPets();
                foreach (var pet in pets)
                {
                    if (pet.PreviousOwner.OwnerId == deletedOwner.OwnerId)
                    {
                        bool removePetSuccess;
                        _petService.DeletePet(pet, out removePetSuccess);
                        Console.WriteLine($"{pet}" + ((removePetSuccess) ? " was successfully deleted!" : " was NOT deleted!"));
                    }                    

                }
            }
        }

        private Owner GetOwnerById()
        {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 1)
            {
                Console.WriteLine("You must enter a positive integer");
            }
            return _ownerService.GetOwnerById(id);
        }

        private Owner NewOwner()
        {
            var firstName = GetUserInput("Enter the first name of the owner").Trim();
            while (!new Regex(RegexConstants.FirstOrLastName).IsMatch(firstName))
            {
                firstName = GetUserInput("First name must consist of letters a-z\nTry entering a new first name:").Trim();
            }
            var lastName = GetUserInput("Enter the last name of the owner").Trim();
            while (!new Regex(RegexConstants.FirstOrLastName).IsMatch(lastName))
            {
                lastName = GetUserInput("Last name must consist of letters a-z\nTry entering a new last name:").Trim();
            }
            var address = GetUserInput("Enter the address of the owner").Trim();
            while (!new Regex(RegexConstants.RegexAddress).IsMatch(address))
            {
                address = GetUserInput("Address must be in format street, number, with additions allowed after\nTry entering a new address:").Trim();
            }
            var phoneNumber = GetUserInput("Enter the phone number of the owner:").Trim();
            while (!new Regex(RegexConstants.RegexPhoneNumber).IsMatch(phoneNumber))
            {
                phoneNumber = GetUserInput("Phone number is not in valid format\nTry entering a new phone number:").Trim();
            }
            var email = GetUserInput("Enter the email of the owner:").Trim();
            while (!new Regex(RegexConstants.RegexEmail).IsMatch(email))
            {
                email = GetUserInput("Email is not in valid format\nTry entering a new email").Trim();
            }
            return _ownerService.NewOwner(new Owner()
            { 
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email
            });

        }


    }
}
