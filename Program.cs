using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using TournamentGame.Handlers;
using TournamentGame.Models;

namespace TournamentGame;

class Program
{
    public Wizard wizard = new("TEST", 10, 10, 10, "Air");
    public string CHARACTER_STORAGE = @"wizards.json";

    static string GetInput(string prompt)
    {
        string finalInput = "";
        Console.WriteLine(prompt);
        while (string.IsNullOrEmpty(finalInput))
        {
            string userInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(userInput))
            {
                finalInput = userInput;
            }
            else
            {
                Console.WriteLine("Invalid Input, please try again");
                continue;
            }
        }
        return finalInput;
    }

    static void Main(string[] args)
    {
        // Steps
        /*
        Steps:
        * INPUT: name
        * create wizard object for this new wizard
        * store new wizard in json file
        */
        Console.WriteLine("Welcome to the Tournament!");
        string isEnrolled = GetInput("Are you already enrolled? Yes or no?");
        string name = "";
        string element = "";
        WizardHandler wizardStorage = new();
        switch (isEnrolled.ToLower())
        {
            case "yes":
                name = GetInput("Wonderful, could you please tell me your name?");
                break;
            // ask for name
            // check through JSON for the name
            case "no":
                name = GetInput("Not a problem, could you please tell me your name?");
                element = GetInput("What element would you like to choose?");
                break;
            // ask for name
            // create new wizard, store in JSON
            default:
                break;
        }
        Wizard wizard = wizardStorage.GetWizard(name, element);
        Console.WriteLine(
            $"Welcome to the Tournament, {wizard.Name} ({wizard.Element}) it's wonderful to have you here"
        );
        // next day logic
        bool tourneyWon = false;
        bool dayEnded = false;
        while (!dayEnded) { 
            
        }
        if (tourneyWon) {
            Console.WriteLine($"Congratulations Wizard {wizard.Name}! You have proven that {wizard.Element} is the strongest of them all!");
        }
    }
}
