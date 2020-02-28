using System;
using System.IO;

namespace SpawnGroupsMaker
{
    class Program
    {
    	private static bool _RUNNING = false;
    	
        static readonly string[] MenuOptions = {
            "Create Spawn Group File",
            "Add Spawn Group To Existing File",
            "Exit"
        };

        static int SelectedOption = 1;

        static void Main(string[] args)
        {
        	_RUNNING = Start();
        		
            while (_RUNNING)
            {
            	try
            	{
            		Frame();
            	}
           		catch
           		{
           			Console.WriteLine("An unhandled error occured.");
           		}
            }
        }
        
        private static bool Start()
        {
        	try
        	{
        		//initialization code
        	}
        	catch
        	{
        		Console.WriteLine("Failed to launch the program.");
        		return false;
        	}
        	
        	return true;
        }
        
        private static void Frame()
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
				
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.U)
                    SelectedOption--;
                else if (key == ConsoleKey.DownArrow || key == ConsoleKey.D)
                    SelectedOption++;
                else if(key == ConsoleKey.Enter || key == ConsoleKey.E)
                {
                    switch(SelectedOption)
                    {
                        case 0:
                        	Console.Clear();
                        	if(File.Exists("SpawnGroups.sbc"))
                        		Console.WriteLine("File already exists. Move the current file to a different directory to avoid losing any data");
                        	else
                            	CreateFile();
                            
                            Console.ReadKey();
                            break;
                        
                        case 1:
                        	Console.Clear();
                        	if(File.Exists("SpawnGroups.sbc"))
                        		AddSpawnGroup(CreateSpawnGroup());
                        	else
                        		Console.WriteLine("Couldn't find the current file. Please create one before adding Spawn Groups.");
                        	
                        	Console.ReadKey();
                        	break;
                        
						
                        case 2:
                            _RUNNING = false;
                            break;
						
                    }
                }
				
                if (SelectedOption < 0)
                    SelectedOption = MenuOptions.Length - 1;
                else if (SelectedOption >= MenuOptions.Length)
                    SelectedOption = 0;
        }
        
        
	
        static string CreateSpawnGroup()
        {
            string output = "\t<SpawnGroup>\n";
			
            output += "\t\t<Id>\n";
            output += "\t\t\t<TypeId>SpawnGroupDefinition</TypeId>\n";
			
            Console.WriteLine("Groupe name: ");
            output += $"\t\t\t<SubtypeId>{Console.ReadLine()}</SubtypeId>\n";
            output += "\t\t</Id>\n";
            output += "\t\t<Description>\n";
			
            output += "\t\t[Modular Encounters SpawnGroup]\n";
			
            Console.WriteLine("Craft type:\n1)Atmospheric cargo ship\n2)Space cargo ship\n3)Lunar cargo ship\n4)Random space encounter\n5)Planetary installation");
            string type = Console.ReadLine();
			while(type != "1" && type != "2" && type != "3" && type != "4" && type != "5")
			{
				Console.WriteLine(type + "is not a valid option. Pick one of the give ones");
				type = Console.ReadLine();
			}
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
			
            Console.WriteLine("Faction name (no spaces): ");
            string factionName = Console.ReadLine();
            while(factioName.Constains(" "))
            {
            	factionName = Console.WriteLine("Name Has Spaces! Insert a valid name.");
            	factionName = Console.ReadLine();
        	}
            output += $"\t\t[FactionOwner:{factionName}]\n";
            output += "\t\t[ReplenishSystems:true]\n";
            output += "\t\t</Description>\n";
            output += "\t\t<Icon>Textures\\GUI\\Icons\\Fake.dds</Icon>";
			
            Console.WriteLine("Is pirate (true/false): ");
            string isPirate = Console.ReadLine();
            while(isPirate != "true" && isPirate != "false")
            {
            	Console.WriteLine(isPirate + "")//finish
            }
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
            
            Console.WriteLine("Spawn Group Complete!");
            
            return output;
        }
        
        static void CreateFile()
    	{
    		if(!File.Exists("SpawnGroups.sbc"))
                File.Create("SpawnGroups.sbc").Dispose();
            else 
           	{
        		Console.WriteLine("File already exists! Make sure you save the current file in a separate folder to avoid loosing progress.");
           		Console.ReadKey();
           		return;
           	}
            
            StreamWriter file = new StreamWriter("SpawnGroups.sbc");
			
			string toBinary = "<?xml version=\"1.0\"?>\n<Definitions xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n<SpawnGroups>\n</SpawnGroups>\n</Definitions>";
            
            file.Write(toBinary);
            file.Dispose();
            
            Console.WriteLine("File created!");
		}
		
		static void AddSpawnGroup(string text)
		{
			if(!File.Exists("SpawnGroups.sbc"))
    		{
    			Console.WriteLine("No existing file. Create one before adding more groups.");
    			Console.ReadKey();
    			return;
    		}
    		
    		StreamReader fileR = new StreamReader("SpawnGroups.sbc");
    		string str = fileR.ReadToEnd();
    		fileR.Dispose();
    		
    		int Id = -1;
    		Id = str.IndexOf("<SpawnGroups>");
    		if (Id != -1)
    			Console.WriteLine("Found Id");
    		
    		str = str.Insert(Id+14, text);
    		
    		File.WriteAllText("SpawnGroups.sbc", String.Empty);
    		
    		StreamWriter fileW = new StreamWriter("SpawnGroups.sbc");
    		fileW.Write(str);
    		fileW.Dispose();
    		
    		Console.WriteLine("Spawn Group Saved!");
    	}
    }
}
