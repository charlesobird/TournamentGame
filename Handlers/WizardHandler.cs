using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using TournamentGame.Models;

using static TournamentGame.Utils;

namespace TournamentGame.Handlers;

class WizardHandler
{
    public static Dictionary<string, PlayerWizard> Wizards { get; set; }
    public JsonHandler storage = new JsonHandler();
    public WizardHandler()
    {
        Wizards = GetWizards();
    }
    public void CheckForWizardStore()
    {
        if (!File.Exists(WIZARD_STORE_FILE_NAME))
        {
            File.Create(WIZARD_STORE_FILE_NAME);
        }
    }
    public Dictionary<string, PlayerWizard> GetWizards()
    {
        var wizards = storage.ReadFromFile<Dictionary<string, PlayerWizard>>(WIZARD_STORE_FILE_NAME);
        return wizards;
    }

    public PlayerWizard GetWizard(string name, string element = "NONE")
    {
        Dictionary<string, PlayerWizard> wizards = GetWizards();

        PlayerWizard matchingWizard = wizards.ContainsKey(name) ? wizards[name] : CreateWizard(name, element);
        return matchingWizard;
    }

    public bool CheckForWizard(string name)
    {
        return Wizards.ContainsKey(name);
    }

    public PlayerWizard CreateWizard(string name, string element)
    {
        PlayerWizard wizard = new PlayerWizard(name, element);
        SaveWizardState(name, wizard);
        return wizard;
    }

    public void SaveWizardState(string wizardName, PlayerWizard wizard)
    {
        storage.WriteToFile<PlayerWizard>(WIZARD_STORE_FILE_NAME, wizardName, wizard);
        Wizards = GetWizards();
    }

    public EnemyWizard CreateEnemyWizard()
    {
        string[] enemyNames = storage.ReadFromFile<string[]>(@"enemyNames.json");
        string enemyName = enemyNames.ElementAt(GenerateRandomInteger(enemyNames.Length));
        string enemyElement = ELEMENTS.ElementAt(GenerateRandomInteger(ELEMENTS.Length));
        string[] enemyDescriptors = storage.ReadFromFile<string[]>(@"enemyDescriptors.json");
        string enemyDescriptor = enemyDescriptors.ElementAt(GenerateRandomInteger(enemyDescriptors.Length));
        enemyName = $"{enemyDescriptor} {enemyName}";
        EnemyWizard enemyWizard = new EnemyWizard(enemyName, enemyElement);
        return enemyWizard;
    }

}
