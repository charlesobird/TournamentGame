namespace TournamentGame.Models;

class Wizard
{
    private string v;

    public string Name { get; set; }
    public int Hp { get; set; }
    public string Element { get; private set; }

    public int MaxHealth { get; private set; }
    public int Dexterity { get; private set; }
    public int Wisdom { get; private set; }
    public Wizard(string name, int hp, int dexterity, int wisdom, string element)
    {
        Name = name;
        Hp = hp;
        Element = element;
        Dexterity = dexterity;
        Wisdom = wisdom;
        MaxHealth = 100;
    }
}
