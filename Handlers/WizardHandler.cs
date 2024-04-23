using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using TournamentGame.Models;

namespace TournamentGame.Handlers;

internal class WizardHandler
{

    public JsonHandler storage = new JsonHandler();

    bool _IsNullOrEmpty<T>(IEnumerable<T> list)
    {
        return !(list?.Any() ?? false);
    }
    public IEnumerable<Wizard> GetWizards()
    {
        IEnumerable<Wizard> wizards = storage.ReadFromFile<IEnumerable<Wizard>>(@"wizards.json");
        return wizards;
    }

    public Wizard GetWizard(string name, string element = "NONE")
    {
        // if (CheckForExistingWizard(name))
        // {
        IEnumerable<Wizard> wizards = GetWizards();
        IEnumerable<Wizard> matchingWizards = wizards.Where(wizard => wizard.Name == name);
        if (!_IsNullOrEmpty(matchingWizards))
        {
            return matchingWizards.First();
        }
        return CreateWizard(name, element);
    }

    public Wizard CreateWizard(string name, string element)
    {
        Wizard wizard = new Wizard(name, 10, 10, 10, element);
        storage.WriteToFile<Wizard>(@"wizards.json", wizard);
        return wizard;
    }
}
