namespace TournamentGame.Handlers;

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using TournamentGame.Models;

// This Handler is to read from / write to wizards.json instead of a database
class JsonHandler
{
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
    public void WriteToFile<T>(string fileName, T contentToAdd)
    {
        // if (File.Exists(fileName) && !string.IsNullOrEmpty(contents))
        // {
        //     File.WriteAllText(fileName, contents);
        // }
        try
        {
            string fileContents = File.ReadAllText(fileName);
            var list = JsonConvert.DeserializeObject<List<T>>(fileContents);
            // foreach (string key in keys){
            //     foreach (dynamic value in values) {
            //         list.Add(key)
            //     }
            // }
            list.Add(contentToAdd);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(fileName, convertedJson);
        }
        catch (FileNotFoundException)
        {
            throw;
        }
    }
    // public object AppendToObject() {
    //     object test = new object();
    // }
    public object ConvertToObject(string fileContents)
    {
        return JsonConvert.DeserializeObject(fileContents);
    }
}
