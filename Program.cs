using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using TournamentGame.Handlers;
using TournamentGame.Models;
using static TournamentGame.Utils;

namespace TournamentGame;

class Program
{
    static int CalculateDamage(int wisdom, PlayerWizard playerWizard, EnemyWizard enemyWizard, string attackingWizard)
    {
        bool buffActive = false;
        if (attackingWizard == "PLAYER")
        {
            buffActive = playerWizard.CheckIfBuffActive(enemyWizard.Element);
        }
        else if (attackingWizard == "ENEMY")
        {
            buffActive = enemyWizard.CheckIfBuffActive(playerWizard.Element);
        }
        int damage;
        if (buffActive)
        {
            damage = (int)Math.Floor(wisdom * 1.5);
        }
        else
        {
            damage = wisdom;
        }
        return damage;
    }
    static string GetInput(string prompt, bool askForConfirmation = false, string confirmPrompt = "Are you sure? Yes or No")
    {
        static string _getInput(string prompt)
        {
            string finalInput = "";
            Console.WriteLine(prompt);
            while (string.IsNullOrEmpty(finalInput))
            {
                string userInput = Console.ReadLine();
                if (userInput == null)
                {
                    continue;
                }
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
        if (!askForConfirmation) return _getInput(prompt);

        static string _getConfirmation(string prompt, string confirmPrompt)
        {
            string input = _getInput(prompt);
            string confirmedInput = _getInput(confirmPrompt).ToLower();
            switch (confirmedInput)
            {
                case "yes":
                    return input;
                case "no":
                    return _getConfirmation(prompt, confirmPrompt);
                default:
                    return _getConfirmation(prompt, confirmPrompt);

            }
        }
        return _getConfirmation(prompt, confirmPrompt);
    }

    static void Main(string[] args)
    {   
        Console.Title = "The Wise One Tournament";
        Console.WriteLine("Welcome to the Tournament!");
        string isEnrolled = GetInput("Are you already enrolled? Yes or no?").ToLower();
        string name = "";
        string element = "";
        WizardHandler wizardStorage = new();
        wizardStorage.CheckForWizardStore(); // Creates the wizards.json file if it doesnt exist already
        switch (isEnrolled.ToLower())
        {
            case "yes":
                name = GetInput("Wonderful, could you please tell me your name?");
                break;
            // ask for name
            // check through JSON for the name
            case "no":
                bool wizardCreated = false;
                while (wizardCreated == false)
                {

                    name = GetInput("Not a problem, could you please tell me your name?", true);
                    if (!wizardStorage.CheckForWizard(name))
                    {
                        element = GetInput("What element would you like to choose?", true);
                        wizardCreated = true;
                    }
                    else
                    {
                        Console.WriteLine($"{name} is already enrolled.");
                        string useExistingWizard = GetInput($"Do you wish to play as {name}? Yes or no", true).ToLower();
                        switch (useExistingWizard)
                        {
                            case "yes":
                                wizardCreated = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            // ask for name
            // create new wizard, store in JSON
            default:
                break;
        }
        if (name == "")
        {
            Console.WriteLine("Please re-run the program as your name is missing.");
            Environment.Exit(0);
        }
        PlayerWizard wizard = wizardStorage.GetWizard(name, element);
        Console.WriteLine($"Welcome to the Tournament, {CreateElementText(wizard.Element,$"{name}")} of the {CreateElementText($"{wizard.Element}")} Wizards it's wonderful to have you here");
        Console.WriteLine($" Your Hp Stat is {wizard.Hp}\n Your Dexterity Stat is {wizard.Dexterity}\n Your Wisdom Stat is {wizard.Wisdom}\n Your Maximum Health is {wizard.MaxHealth}");
        // next day logic
        bool tourneyWon = false;
        bool dayEnded = false;
        if (wizard.TheWiseOne)
        {
            tourneyWon = true;
            dayEnded = true;
        }        
        while (!dayEnded)
        {
            Console.Title = $"Round {wizard.RoundsWon + 1} of The Wise One Tournament";
            Console.WriteLine($"Welcome one and all to Round {wizard.RoundsWon + 1} of the Wise One Tour");

            EnemyWizard enemyWizard = wizardStorage.CreateEnemyWizard();
            Console.WriteLine($"Greetings! Today you will be fighting. The one, the only,");
            Console.WriteLine($" {CreateElementText(enemyWizard.Element,$"{enemyWizard.Name}")} of the {CreateElementText($"{enemyWizard.Element}")} Wizards");
            Console.ResetColor();
            Console.WriteLine($" Their Hp Stat is {enemyWizard.Hp}\n Their Dexterity Stat is {enemyWizard.Dexterity}\n Their Wisdom Stat is {enemyWizard.Wisdom}\n Their Maximum Health is {enemyWizard.MaxHealth}");
            // here decide if any element buffs are active, decide damage, then go into battle
            // switch (wizard.Element) {
            //     case "Air":
            //         if (enemyWizard.Element == "")
            // }

            string fightStarter = "";
            if (wizard.Dexterity > enemyWizard.Dexterity)
            {
                fightStarter = name;
            }
            else if (wizard.Dexterity < enemyWizard.Dexterity)
            {
                fightStarter = enemyWizard.Name;
            }
            else
            {
                while (fightStarter == "")
                {
                    double playerGoesFirst = GenerateRandomDouble();
                    double enemyGoesFirst = GenerateRandomDouble();
                    if (playerGoesFirst > enemyGoesFirst)
                    {
                        fightStarter = name;
                    }
                    else if (playerGoesFirst < enemyGoesFirst)
                    {
                        fightStarter = enemyWizard.Name;
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            Console.WriteLine($"{name} vs {enemyWizard.Name}");
            Console.WriteLine($"Let the fight begin!!\n {fightStarter} will go first!");
            int currPlayerHealth = wizard.MaxHealth;
            int currEnemyHealth = enemyWizard.MaxHealth;
            // Entire Battle, turn based system
            while (currPlayerHealth > 0 && currEnemyHealth > 0)
            {
                if (fightStarter == enemyWizard.Name)
                {
                    int damage = CalculateDamage(enemyWizard.Wisdom, wizard, enemyWizard, "ENEMY");
                    currPlayerHealth -= damage;
                    if (currPlayerHealth < 0)
                    {
                        currPlayerHealth = 0;
                        
                    }
                    Console.WriteLine($"{enemyWizard.Name} hits {name} for {damage} damage leaving {name} at {currPlayerHealth}!\n");
                    fightStarter = name;
                }
                else
                {
                    int damage = CalculateDamage(enemyWizard.Wisdom, wizard, enemyWizard, "PLAYER");
                    currEnemyHealth -= damage;
                    if (currPlayerHealth < 0)
                    {
                        currEnemyHealth = 0;
                    }
                    Console.WriteLine($"{name} hits {enemyWizard.Name} for {damage} damage leaving {enemyWizard.Name} at {currEnemyHealth}!\n");
                    fightStarter = enemyWizard.Name;
                }
            }

            // the below is the FINAL two lines that should be executed, they save the round
            // if the game crashes or you leave before the end of the round, the round is forgotten
            if (currEnemyHealth <= 0)
            {
                Console.WriteLine($"{enemyWizard.Name} has been defeated!");
                wizard.RoundsWon++;
                wizardStorage.SaveWizardState(name, wizard);
            } else if (currPlayerHealth <= 0) {
                Console.WriteLine($"{name} has been defeated!");
            }
            dayEnded = true;
        }
        if (wizard.RoundsWon == 5)
        {
            tourneyWon = true;
        }
        if (tourneyWon)
        {
            Console.WriteLine($"You have won the tournament, Wizard {name}!\nYou have proven that {wizard.Element} is the strongest of them all!\n You are now declared as \"The Wise One\"!");
            wizard.TheWiseOne = true;
            wizardStorage.SaveWizardState(name, wizard);
        }
    }
}
