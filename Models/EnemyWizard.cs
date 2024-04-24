
namespace TournamentGame.Models;

class EnemyWizard : Wizard
{
    // 
    public string Name {get; private set;}
    public EnemyWizard(string name, string element) : base(element)
    {
        Name = name;
     }

}
