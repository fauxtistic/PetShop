using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.ConsoleApp
{    

    public class ConsoleMenu : IConsoleMenu
    {
        private const int closeOption = 8;
        private IPetService _petService;
        private readonly string[] _menuOptions =
        {
            "Show all pets",
            "Show all pets of specified type",
            "Show all pets by price",
            "Show five most expensive pets",
            "Create a new pet entry",
            "Delete a pet entry",
            "Edit a pet entry",
            "Exit program"
        };

        public ConsoleMenu(IPetService petService)
        {
            _petService = petService;
        }

        public void ConsoleLoop()
        {
            bool running = true;
            while (running)
            {
                ShowOptions();
                var option = ChooseOption();
                ProcessOption(option);
                if (option != closeOption)
                {
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                }
                else
                {
                    running = false;
                }
            }
        }

        private void ShowOptions()
        {
            Console.Clear();
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {_menuOptions[i]}");
            }
            
        }

        private int ChooseOption()
        {
            int option;
            Console.WriteLine("\nSelect option by typing number:");
            while (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > closeOption)
            {
                Console.WriteLine("You must type a number from the ones listed above");
            }
            return option;
        }

        private void ProcessOption(int option)
        {
            switch(option)
            {
                case 1:
                    Console.WriteLine("Showing all pets:\n");
                    ShowPets(_petService.GetAllPets());
                    break;
                case 2:
                    ShowPetsOfSpecifiedType();                    
                    break;
                case 3:
                    Console.WriteLine("Showing all pets by price, in descending order:\n");
                    ShowPets(_petService.GetAllPetsSortedByPrice(false));
                    break;
                case 4:
                    Console.WriteLine("Showing five most expensive pets:\n");
                    ShowPets(_petService.GetNumberOfMostExpensivePets(false, 5));
                    break;
                case 5:
                    CreatePet(); 
                    break;
                case 6:
                    DeletePet();
                    break;
                case 7:
                    EditPet();
                    break;
                case 8:
                    Console.WriteLine("Exiting program, see you soon!");
                    break;
                default:
                    Console.WriteLine("Error, option not defined");
                    break;
            }
        }

        private void CreatePet()
        {
            Console.WriteLine("You are creating a new pet entry");
            try
            {
                Pet createdPet = _petService.CreatePet(NewPet());
                Console.WriteLine($"Created pet: {createdPet}"); //maybe add out bool to show if successful
            }    
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
        }       

        private double GetPrice()
        {
            Console.WriteLine("Enter the price of the pet:");
            double price;
            while(!Double.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("You must enter the price in numbers. Decimal points are allowed");
            }
            return price;
        }


        private PetType GetPetType()
        {
            var petTypeOverview = "The pet can be any of these: ";
            foreach (PetType type in Enum.GetValues(typeof(PetType)))
            {
                petTypeOverview = petTypeOverview + type + ", ";
            }
            petTypeOverview = petTypeOverview.Remove(petTypeOverview.Length - 2);
            Console.WriteLine(petTypeOverview);
            var typeAsText = GetUserInput("Enter the type of pet");

            foreach (PetType type in Enum.GetValues(typeof(PetType)))
            {
                if (type.ToString().ToLower().Equals(typeAsText.ToLower()))
                {
                    return type;
                }
            }
            Console.WriteLine("No such pet type is defined");
            return GetPetType();
        }

        private DateTime GetDateTime(string command)
        {
            Console.WriteLine(command);
            DateTime date;
            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Wrong format");
                Console.WriteLine(command);
            }
            return date;            
        }

        private void ShowPetsOfSpecifiedType()
        {
            PetType type = GetPetType();
            List<Pet> petsOfType = _petService.GetAllPetsOfType(type);
            if (petsOfType.Count == 0)
            {
                Console.WriteLine($"There are no pets of type {type}");
            }
            else
            {
                Console.WriteLine($"Showing pets of type {type}:\n");
                ShowPets(petsOfType);
            }            
        }

        private void ShowPets(List<Pet> pets) 
        {
            foreach (var pet in pets)
            {
                Console.WriteLine($"{pet}\n");
            }
        }

        private void EditPet()
        {
            Console.WriteLine("Enter the id of the pet entry you want to edit:");
            Pet foundPet = GetPetById();
            if (foundPet == null)
            {
                Console.WriteLine("There is no pet with this id!");
            }
            else
            {
                Console.WriteLine($"You are editing this entry: {foundPet}");
                try
                {
                    Pet editedPet = NewPet();
                    editedPet.PetId = foundPet.PetId; //this can be better
                    Pet changedPet = _petService.EditPet(editedPet);
                    Console.WriteLine($"Previous information about pet:\n{foundPet}\n\nPet entry after being edited:\n{changedPet}\n");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                }                
                
            }
        }

        private void DeletePet()
        {
            Console.WriteLine("Enter the id of the pet entry you want to delete:");
            Pet foundPet = GetPetById();
            if (foundPet == null)
            {
                Console.WriteLine("There is no pet with this id!");
            }
            else
            {
                bool success;
                Pet deletedPet = _petService.DeletePet(foundPet, out success);
                Console.WriteLine($"The operation to remove:\n{deletedPet}\n" + ((success) ? "\n...was successful!" : "\n...was NOT successful!"));
            }
        }

        private Pet GetPetById()
        {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 1)
            {
                Console.WriteLine("You must enter a positive integer");
            }
            return _petService.GetPetById(id);
        }

        private Pet NewPet()
        {
            var name = GetUserInput("Enter the name of the pet:");
            while (name.Length < 2)
            {
                name = GetUserInput("Name of the pet must be longer than two characters. Try entering a new name:");
            }
            var type = GetPetType();
            var birthDate = GetDateTime("Enter the birth date of the pet in format dd-mm-yyyy:");
            while (birthDate > DateTime.Now)
            {
                Console.WriteLine("Birth date of the pet cannot be in the future");
                birthDate = GetDateTime("Enter the birth date of the pet in format dd-mm-yyyy:");
            }
            var soldDate = GetDateTime("Enter the last selling date of the pet in format dd-mm-yyyy:");
            while (soldDate > birthDate)
            {
                Console.WriteLine($"Last selling date of the pet cannot predate it's birthDate {birthDate.ToShortDateString()}");
                soldDate = GetDateTime("Enter the last selling date of the pet in format dd-mm-yyyy:");
            }
            var color = GetUserInput("Enter the color(s) of the pet:");
            while (color.Length < 3)
            {
                color = GetUserInput("Color of the pet must be longer than two characters. Try entering a new color");
            }
            var previousOwner = GetUserInput("Enter the previous owner of the pet:");
            while (previousOwner.Length < 2)
            {
                previousOwner = GetUserInput("Name of previous owner must be longer than two characters. Try entering a new name:");
            }
            var price = GetPrice();  
            while (price < 0)
            {
                Console.WriteLine("The price cannot be negative");
                price = GetPrice();
            }
            return _petService.NewPet(new Pet() {
                    Name = name,
                    Type = type,
                    BirthDate = birthDate,
                    SoldDate = soldDate,
                    Color = color,
                    PreviousOwner = previousOwner,
                    Price = price
                });               
                 
        }

        private string GetUserInput(string command)
        {
            Console.WriteLine(command);
            return Console.ReadLine();
        }
    }
}
