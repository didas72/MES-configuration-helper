using System;
using System.IO;
using System.Collections.Generic;

namespace SpawnGroupsMaker
{
    class Program
    {
        #region variables
        private static bool _RUNNING = false;
    	
        static readonly string[] MenuOptions = {
            "Create Spawn Group File",
            "Add Spawn Group To Existing File",
            "Exit"
        };
        static int SelectedOption = 0;

        List<SpawnGroup> spawnGroups;
        #endregion

        #region EntryPoint
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
        #endregion

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

        #region Functions
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
				Console.WriteLine(type + " is not a valid option. Please insert one of the given.");
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
            while(factionName.Contains(" "))
            {
            	Console.WriteLine("Name Has Spaces! Please insert a valid name.");
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
                Console.WriteLine(isPirate + " is not a valid value. Please insert one of the given");
                isPirate = Console.ReadLine();
            }
            output += $"\t\t<IsPirate>{isPirate}</IsPirate>\n";
			
            Console.WriteLine("Frequency: ");
            string freq = Console.ReadLine();
            double result;
            while (!double.TryParse(freq, out result))
            {
                Console.WriteLine(freq + " is not a valid number. Please insert a valid number.");
                freq = Console.ReadLine();
            }
            output += $"\t\t<Frequency>{freq}</Frequency>\n";
            output += "\t\t<Prefabs>\n";//here-------------------------------------------------------------
			
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
            while (!double.TryParse(speed, out result))
            {
                Console.WriteLine(speed + " is not a valid number. Please insert a valid number.");
                speed = Console.ReadLine();
            }
            output += $"\t\t\t\t<Speed>{speed}</Speed>\n";
            output += "\t\t\t\t<Behaviour></Behaviour>\n";
            output += "\t\t\t\t<BehaviourActivationDistance>20000</BehaviourActivationDistance>\n";
            output += "\t\t\t</Prefab>\n";//end of here----------------------------------------------------
            output += "\t\t</Prefabs>\n";
            output += "\t</SpawnGroup>\n";
            
            Console.WriteLine("Spawn Group Complete!");
            
            return output;
        }

        static string CreatePrefab()
        {
            string output = "\t\t<Prefabs>\n";//here-------------------------------------------------------------

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
            double result;
            while (!double.TryParse(speed, out result))
            {
                Console.WriteLine(speed + " is not a valid number. Please insert a valid number.");
                speed = Console.ReadLine();
            }
            output += $"\t\t\t\t<Speed>{speed}</Speed>\n";
            output += "\t\t\t\t<Behaviour></Behaviour>\n";
            output += "\t\t\t\t<BehaviourActivationDistance>20000</BehaviourActivationDistance>\n";
            output += "\t\t\t</Prefab>\n";//end of here----------------------------------------------------

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
        #endregion

        class SpawnGroup
        {
            public const string TypeId = "SpawnGroupDefinition";
            public string subTypeId;
            public Description description;
            public List<Prefab> prefabs = new List<Prefab>();
            public const string icon = "Textures\\GUI\\Icons\\Fake.dds";
            public double frequency;
            public bool isCargoShip;
            public bool isPirate;
            public bool isEncounter;
        }

        class Rotation
        {
            public double X, Y, Z;
        }

        class Description
        {
            public const string spawngroupHeader = "[Modular Encounters SpawnGroup]";
            public const string territoryHeader = "[Modular Encounters Territory]";

            //territory only
            public string name;
            public string type;//Static or Planetary
            public bool active;
            public double radius;
            public bool scaleRadiusWithPlanetSize;
            public double coordsX, coordsY, coordsZ;
            public bool announceArriveDepart;
            public string customArriveMessage;
            public string customDepartMessage;
            public string planetGeneratorName;//EarthLike,Mars,Titan, other planet generator name

            //spawngroup only
            public bool spaceCargoShip;
            public bool lunarCargoShip;
            public bool useAutoPilotInSpace;
            public bool spaceRandomEncounter;
            public bool atmosphericCargoShip;
            public bool planetaryInstallation;
            public string planetaryInstallationType;//Small, Medium or Large. default is Small
            public List<Rotation> rotateInstallations;
            public List<bool> reverseForwardDirections;
            public bool cutVoxelsAtAirtightCells;
            public string factionOwner;
            public bool replenishSystems;
            public bool uniqueEncounter;
            public bool useRandomMinerFaction;
            public bool useRandomBuilderFaction;
            public bool useRandomTraderFaction;
            public bool ignoreCleanupRules;
            public bool initializeStoreBlocks;
            public List<string> containerTypesForStoreOrders;
            public double pauseAutopilotAtPlayerDistance;//-1 for ignore
            public bool forceStaticGrid;
            public bool adminSpawnOnly;
            public double minSpawnFromWorldCenter;
            public double maxSpawnFromWorldCenter;
            public List<string> planetBlackList;
            public List<string> planetWhitelist;
            public bool planetRequiresVacuum;
            public bool planetRequiresAtmo;
            public bool planetRequiresOxygen;
            public double planetMinimumSize;
            public double planetMaximumSize;
            public bool useThreatLevelCheck;
            public double threatLevelCheckRange;
            public bool threatIncludeOtherNpcOwners;
            public double threatScoreMinimum;
            public double threatScoreMaximum;
            public bool usePCUCheck;
            public double PCUCheckRadius;
            public double PCUMinimun;
            public double PCUMaximum;
            public bool usePlayerCountCheck;
            public double playerCountCheckRadius;
            public int MinimumPlayers;
            public int MaximumPlayers;
            public bool usePlayerCredits;
            public bool IncludeAllPlayersInRadious;
            public bool includeFactionBalance;
            public double playerCreditsCheckRadius;
            public int minimumPlayerCredits;
            public int maximumPlayerCredits;
            public bool usePlayerFactionReputation;
            public double playerReputationCheckRadious;
            public string checkReputationAgainstOtherNPCFaction;
            public double minimumReputation;
            public double maximumReputation;
            public bool attachModStorageComponentToGrid;
            public string storageKey;
            public string storageValue;
            public List<string> requireAllMods;
            public List<string> requireAnyMods;
            public List<string> excludeAnyMods;
            public List<string> excludeAllMods;
            public List<string> modBlockExists;
            public List<int> requiredPlayersOnline;
            public bool usePlayerLocations;
            public bool knownPlayerLocationMustMatchFaction;
            public double knownPlayerLocationMinSpawnedEncounters;
            public double knownPlayerLocationMaxSpawnedEncounters;
            public string territory;
            public double minDistanceFromTerritoryCenter;
            public double maxDistanceFromTerritoryCenter;
            public bool rotateFirstCockpitToForward;
            public bool spawnRandomCargo;
            public bool disableDampners;
            public bool reactorsOn;
            public bool removeVoxelsIfGridRemoved;//88 total
        }

        class Prefab
        {
            public string subtypeId;
            public Vector3 position;
            public string beaconText;
            public double speed;
            public string behaviour;
            public double behaviourActivationDistance;

            public Prefab(string SubtypeId, string BeaconText, double Speed)
            {
                subtypeId = SubtypeId;
                position = new Vector3();
                beaconText = BeaconText;
                speed = Speed;
                behaviour = "";
                behaviourActivationDistance = 20000.0;
            }

            public Prefab(string SubtypeId, Vector3 Position, string BeaconText, double Speed)
            {
                subtypeId = SubtypeId;
                position = Position;
                beaconText = BeaconText;
                speed = Speed;
                behaviour = "";
                behaviourActivationDistance = 20000.0;
            }

            public Prefab(string SubtypeId, string BeaconText, double Speed, string Behaviour, double BehaviourActivationDistance)
            {
                subtypeId = SubtypeId;
                position = new Vector3();
                beaconText = BeaconText;
                speed = Speed;
                behaviour = Behaviour;
                behaviourActivationDistance = BehaviourActivationDistance;
            }
        }

        class Vector3
        {
            public double X, Y, Z;

            public Vector3(double x, double y, double z)
            {
                X = x; Y = y; Z = z;
            }

            public Vector3()
            {
                X = 0; Y = 0; Z = 0;
            }
        }
    }
}
