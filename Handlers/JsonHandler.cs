namespace TournamentGame.Handlers;

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using TournamentGame.Models;

class JsonHandler
{
    public dynamic ReadFromFile(string fileName)
    {
        try
        {
            string fileContents = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject(fileContents);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
        public dynamic ReadFromFile<T>(string fileName)
    {
        try
        {
            string fileContents = File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<T>(fileContents);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
    public void WriteToFile<T>(string fileName,string key, T contentToAdd)
    {
        try
        {
            var jsonContents = ReadFromFile<Dictionary<string,T>>(fileName);
            jsonContents[key] = contentToAdd;
            File.WriteAllText(fileName, JsonConvert.SerializeObject(jsonContents, Formatting.Indented));
        }
        catch (FileNotFoundException)
        {
            throw;
        }
        catch (NullReferenceException) {
            throw;
        }
    }
    public object ConvertToObject(string fileContents)
    {
        return JsonConvert.DeserializeObject(fileContents);
    }
}
