using System;
using Repository;

public class Program
{
    public static string LoadXML(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    public static string LoadJson(string fileName)
    {
        return File.ReadAllText(fileName);
    }

    public static void Main(string[] args)
    {
        Manager manager = new Manager();

        // string content = LoadJson("data.json");

        // manager.RegisterManager("sensor",content, (int)ItemType.JSON);

        string content = LoadJson("data.xml");

        manager.RegisterManager("sensor",content, (int)ItemType.XML);

        manager.RetrieveManager("sensor", result => 
        {
            Console.WriteLine("Retrieve: " + result);
        });

        manager.GetTypeManager("sensor", result => 
        {
            Console.WriteLine("Gettype: " + result);
        });
        manager.RegisterManager("sensor2",content, (int)ItemType.XML);
        manager.DeRegisterManager("sensor2");
        

    }

}
