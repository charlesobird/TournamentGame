
using System.Dynamic;

namespace TournamentGame.Models;

class PlayerWizard : Wizard
{
    public int RoundsWon { get; set; }
    public bool TheWiseOne { get; set; }
    public PlayerWizard(string name, string element) : base(element)
    {}
}
