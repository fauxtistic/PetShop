using PetShop.Core.ApplicationService;
using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }

        private void EditOwner()
        {
            throw new NotImplementedException();
        }

        private void DeleteOwner()
        {
            throw new NotImplementedException();
        }

    }
}
