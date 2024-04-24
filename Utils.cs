using System.Security.Cryptography.X509Certificates;

namespace TournamentGame;

class Utils
{
    public static int DEFAULT_HP_STAT = 5;
    public static int DEFAULT_DEX_STAT = 5;
    public static int DEFAULT_WIS_STAT = 5;
    public static int DEFAULT_STARTER_POINTS = 20;
    public static int DEFAULT_MAX_HEALTH = 100;
    public static string[] ELEMENTS = { "Water", "Earth", "Fire", "Air" };
    public static string WIZARD_STORE_FILE_NAME = "wizards.json";

    public static string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
    public static string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
    public static string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
    public static string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
    public static string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
    public static string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
    public static string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
    public static string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
    public static string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";

    public static double PERFECT_BLOCK_PERCENTAGE_CHANCE = 0.2;
    public static string CreateColouredText(string text, string colour, bool addUnderline=false, bool addBold=false)
    {
        static string _addUnderline(string _text) {
            return $"{UNDERLINE}{_text}{NOUNDERLINE}";
        }
        static string _addBold(string _text) {
            return $"{BOLD}{_text}{NOBOLD}";
        }
        string finalString = $"{colour}{text}{NORMAL}";
        if (addUnderline) {
            finalString = _addUnderline(finalString);
        }
        if (addBold){
            finalString = _addBold(finalString);
        }
        return finalString;
    }
    public static string CreateElementText(string element, string text = null)
    {
        switch (element)
        {
            case "Fire":
                return CreateColouredText(text == null ? element : text, RED, true);
            case "Water":
                return CreateColouredText(text == null ? element : text, BLUE, true);
            case "Air":
                return CreateColouredText(text == null ? element : text, GREY, true);
            case "Earth":
                return CreateColouredText(text == null ? element : text, GREEN, true);
        }
        return CreateColouredText(text == null ? element : text, NORMAL);
    }
    public static double GenerateRandomDouble()
    {
        return new Random().NextDouble();
    }
    public static int GenerateRandomInteger(int maxValue = int.MaxValue)
    {
        return new Random().Next(maxValue);
    }
}