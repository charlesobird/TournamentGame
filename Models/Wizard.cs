namespace TournamentGame.Models;

using static TournamentGame.Utils;
class Wizard
{
    public int Hp { get; set; }
    public string Element { get; set; }

    public int MaxHealth { get; set; }
    public int Dexterity { get; set; }
    public int Wisdom { get; set; }
    public Wizard(string element)
    {
        Element = element;
        double hpRandom = GenerateRandomDouble();
        double dexRandom = GenerateRandomDouble();
        double wisRandom = GenerateRandomDouble();
        Hp = (int)Math.Floor((1 + hpRandom) * DEFAULT_HP_STAT);
        Dexterity = (int)Math.Floor((1 + dexRandom) * DEFAULT_DEX_STAT);
        Wisdom = (int)Math.Floor((1 + wisRandom) * DEFAULT_WIS_STAT);
        MaxHealth = DEFAULT_MAX_HEALTH + (2 * Hp);
    }
    public bool CheckIfBuffActive(string attackingWizardElement)
    {
        if (Element == "Water" && attackingWizardElement == "Fire")
        {
            return true;
        }
        else if (Element == "Fire" && attackingWizardElement == "Air")
        {
            return true;
        }
        else if (Element == "Air" && attackingWizardElement == "Earth")
        {
            return true;
        }
        else if (Element == "Earth" && attackingWizardElement == "Water")
        {
            return true;
        }
        return false;
    }
}
