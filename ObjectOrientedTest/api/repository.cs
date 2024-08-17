using System;
using System.Xml;
using System.Text.Json;
using IniParser;
using IniParser.Model;
using System.Data.SqlTypes;

namespace Repository
{
    enum ItemType
    {
        XML,
        JSON
    }

    public class IO
    {
        private FileIniDataParser parser = new FileIniDataParser();
        private string iniPath = "repository.ini";
        public void SaveIni(string? itemName, String ?itemContent, int itemType)
        {
            // Load INI file
            IniData data = parser.ReadFile(iniPath);

            // Write a value
            data[itemName]["content"] = itemContent;
            data[itemName]["itemType"] = itemType.ToString();
            parser.WriteFile(iniPath, data);
        }
        public string LoadIniString(string itemName)
        {
            IniData data = parser.ReadFile(iniPath);
            string? value = data[itemName]["content"]; 
            return value;
            
        }
        public int LoadIniInt(string itemName)
        {
            IniData data = parser.ReadFile(iniPath);
            string value = data[itemName]["itemType"]; 
            return int.Parse(value);
        }

        public void DeleteIni(string section)
        {
            IniData data = parser.ReadFile(iniPath);
            if (data.Sections.ContainsSection(section))
            {
                // Remove the key
                data.Sections.RemoveSection(section);

                // Save the changes back to the INI file
                parser.WriteFile(iniPath, data);

                Console.WriteLine($"Key removed from section '{section}'.");
            }
            else
            {
                Console.WriteLine($"Key not found in section '{section}'.");
            }
        }
    }

    public class RepositoryHandler : IO
    {
        public class ItemContent
        {
            public string? content { get; set;}
        }

        public void Register(string itemName, string itemContent, int itemType)
        {
            string? content = null;

            if(itemType == (int)ItemType.XML)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(itemContent);

                // Access elements
                XmlNode? root = xmlDoc?.DocumentElement;
                XmlNode? element = root?.SelectSingleNode("content");
                content = element?.InnerText;
                

                // loging
                Console.WriteLine("register content: " + content );
            }
            if(itemType == (int)ItemType.JSON)
            {
                var contentDeserial = JsonSerializer.Deserialize<ItemContent>(itemContent);
                content = contentDeserial?.content;
                
                // loging
                Console.WriteLine("register  content: "+ content);
            }

            // listRepo.Add(repo);
            SaveIni(itemName , content, itemType);
        }

        public string? Retrieve(string itemName)
        {
            return LoadIniString(itemName);
        }

        public int? GetType(string itemName)
        {
            return LoadIniInt(itemName);
        }

        public void DeRegister(string itemName)
        {
            DeleteIni(itemName);
        }   
    }
}