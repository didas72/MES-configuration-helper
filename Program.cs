using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SpawnGroupsMaker
{
    class Program
    {
        static readonly string[] MenuOptions = {
            "Create Spawn Group File",
            "Add Spawn Group To Existing File",
            "Exit"
        };

        static int SelectedOption = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                for(int i = 0; i < MenuOptions.Length; i++)
                {
                    if (SelectedOption == i)
                        Console.WriteLine($"<{MenuOptions[i]}>");
                    else
                        Console.WriteLine(MenuOptions[i]);
                }

                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow)
                    SelectedOption--;
                else if (key == ConsoleKey.DownArrow)
                    SelectedOption++;
                else if(key == ConsoleKey.Enter || key == ConsoleKey.E)
                {
                    switch(SelectedOption)
                    {
                        case 0:
                            CreateFile(CreateSpawnGroup());
                            break;
                            
                        case 1:
                        	AddSpawnGroup(CreateSpawnGroup());
                            break;
                            

                        case 2:
                            return;

                    }
                }

                if (SelectedOption < 0)
                    SelectedOption = MenuOptions.Length - 1;
                else if (SelectedOption >= MenuOptions.Length)
                    SelectedOption = 0;
            }
        }

        static string CreateSpawnGroup()
        {
        	Console.Clear();
            string output = "<SpawnGroups>\n";
            output += "\t<SpawnGroup>\n";

            output += "\t\t<Id>\n";
            output += "\t\t\t<TypeId>SpawnGroupDefinition</TypeId>\n";

            Console.WriteLine("Groupe name: ");
            output += $"\t\t\t<SubtypeId>{Console.ReadLine()}</SubtypeId>\n";
            output += "\t\t</Id>\n";
            output += "\t\t<Description>\n";

            output += "\t\t[Modular Encounters SpawnGroup]\n";

            Console.WriteLine("Craft type:\n1)Atmospheric cargo ship\n2)Space cargo ship\n3)Lunar cargo ship\n4)Random space encounter\n5)Planetary installation");
            string type = Console.ReadLine();

            if (type == "1")
                output += "\t\t[AtmosphericCargoShip:true]\n";
            else if (type == "2")
                output += "\t\t[SpaceCargoShip:true]\n";
            else if (type == "3")
                output += "\t\t[LunarCargoShip:true]\n";
            else if (type == "4")
                output += "\t\t[RandomSpaceEncounter:true]\n";
            else if (type == "5")
                output += "\t\t[PlanetaryInstallation:true]\n";
            else
            {
                Console.WriteLine("Invalid option");
                Console.ReadKey();
                throw new Exception();
            }

            Console.WriteLine("Faction name (no spaces): ");
            output += $"\t\t[FactionOwner:{Console.ReadLine()}]\n";
            output += "\t\t[ReplenishSystems:true]\n";
            output += "\t\t</Description>\n";
            output += "\t\t<Icon>Textures\\GUI\\Icons\\Fake.dds</Icon>";

            Console.WriteLine("Is pirate (true/false): ");
            string isPirate = Console.ReadLine();
            output += $"\t\t<IsPirate>{isPirate}</IsPirate>\n";

            Console.WriteLine("Frequency: ");
            string freq = Console.ReadLine();
            output += $"\t\t<Frequency>{freq}</Frequency>\n";
            output += "\t\t<Prefabs>\n";

            Console.WriteLine("Ship file name (no extension): ");
            string fileName = Console.ReadLine();
            output += $"\t\t\t<Prefab SubtypeId=\"{fileName}\">\n";
            output += "\t\t\t\t<Position>\n";
            output += "\t\t\t\t\t<X>0.0</X>\n";
            output += "\t\t\t\t\t<Y>0.0</Y>\n";
            output += "\t\t\t\t\t<Z>0.0</Z>\n";
            output += "\t\t\t\t</Position>\n";

            Console.WriteLine("Speed: ");
            string speed = Console.ReadLine();
            output += $"\t\t\t\t<Speed>{speed}</Speed>\n";
            output += "\t\t\t\t<Behaviour></Behaviour>\n";
            output += "\t\t\t\t<BehaviourActivationDistance>20000</BehaviourActivationDistance>\n";
            output += "\t\t\t</Prefab>\n";
            output += "\t\t</Prefabs>\n";
            output += "\t</SpawnGroup>\n";
            
            Console.WriteLine("Done!");
            
            Console.ReadKey();
            
            return output;
        }
        
        static void CreateFile(string text)
    	{
    		if(!File.Exists("SpawnGroups.sbc"))
                File.Create("SpawnGroups.sbc");
            else 
            	return;
            
            FileStream file = File.OpenWrite("SpawnGroups.sbc");
			
			string toBinary = "<?xml version=\"1.0\"?>\n<Definitions xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n" + text + "</Definitions>";
			
            byte[] data = Encoding.ASCII.GetBytes(text);
            
            file.Write(data);

            Console.WriteLine("Saved!");
            
            Console.ReadLine();
    	}
    	
    	static void AddSpawnGroup(string text)
    	{
    		//FileStream file = File.Open("SpawnGroups.sbc", FileMode.Open);
    		
    		//string str = Encoding.ASCII.GetString(File.ReadAllBytes("SpawnGroups.sbc"));
    		
    		string str = text;
    		
    		int Id = str.IndexOf("<SpawnGroups>");
    		if (Id != null)
    			Console.WriteLine("Found Id");
    		
    		str = str.Insert(Id+1, text);
    		
    		Console.Write(str);
    		
    		Console.ReadKey();
    	}
    }
}
