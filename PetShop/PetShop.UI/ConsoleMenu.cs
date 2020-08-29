using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.ConsoleApp
{    

    public class ConsoleMenu : Menu
    {
        private IPetService _petService;
        private IOwnerService _ownerService;
        private PetMenu _petMenu;
        private OwnerMenu _ownerMenu;       

        public ConsoleMenu(IPetService petService, IOwnerService ownerService)
        {
            CloseOption = 3;
            MenuOptions = new string[] 
            {
                "Show pet menu",
                "Show owner menu",
                "Exit program"
            };
            _petService = petService;
            _ownerService = ownerService;
            _petMenu = new PetMenu(petService, ownerService);
            _ownerMenu = new OwnerMenu(petService, ownerService);
        }

        public override void ProcessOption(int option)
        {
            switch(option)
            {
                case 1:
                    _petMenu.ConsoleLoop();
                    break;
                case 2:
                    _ownerMenu.ConsoleLoop();
                    break;
                case 3:
                    Console.WriteLine("Exiting program, see you soon!");
                    break;
                default:
                    Console.WriteLine("Error, option not defined");
                    break;
            }
        }
    }
}
