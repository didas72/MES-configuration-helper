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
            "Create Spawn Group",
            "Exit"
        };

        static int SelectedOption = 0;

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
                else if(key == ConsoleKey.Enter)
                {
                    switch(SelectedOption)
                    {
                        case 0:
                            CreateSpawnGroup();
                            break;

                        case 1:
                            return;

                    }
                }

                if (SelectedOption < 0)
                    SelectedOption = MenuOptions.Length - 1;
                else if (SelectedOption >= MenuOptions.Length)
                    SelectedOption = 0;
            }
        }

        static void CreateSpawnGroup()
        {
            string output = "<SpawnGroup>\n";

            output += "\t<Id>\n";
            output += "\t\t<TypeId>SpawnGroupDefinition</TypeId>\n";

            Console.WriteLine("Groupe name: ");
            output += $"\t\t<SubtypeId>{Console.ReadLine()}</SubtypeId>\n";
            output += "\t</Id>\n";
            output += "\t<Description>\n";

            output += "\t[Modular Encounters SpawnGroup]\n";

            Console.WriteLine("Craft type:\n1)Atmospheric cargo ship\n2)Space cargo ship\n3)Lunar cargo ship\n4)Random space encounter\n5)Planetary installation");
            string type = Console.ReadLine();

            if (type == "1")
                output += "\t[AtmosphericCargoShip:true]\n";
            else if (type == "2")
                output += "\t[SpaceCargoShip:true]\n";
            else if (type == "3")
                output += "\t[LunarCargoShip:true]\n";
            else if (type == "4")
                output += "\t[RandomSpaceEncounter:true]\n";
            else if (type == "5")
                output += "\t[PlanetaryInstallation:true]\n";
            else
            {
                Console.WriteLine("Invalid option");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Faction name (no spaces): ");
            output += $"\t[FactionOwner:{Console.ReadLine()}]\n";
            output += "\t[ReplenishSystems:true]\n";
            output += "\t</Description>\n";
            output += "\t<Icon>Textures\\GUI\\Icons\\Fake.dds</Icon>";

            Console.WriteLine("Is pirate (true/false): ");
            string isPirate = Console.ReadLine();
            output += $"\t<IsPirate>{isPirate}</IsPirate>\n";

            Console.WriteLine("Frequency: ");
            string freq = Console.ReadLine();
            output += $"\t<Frequency>{freq}</Frequency>\n";
            output += "\t<Prefabs>\n";

            Console.WriteLine("File name (no extension): ");
            string fileName = Console.ReadLine();
            output += $"\t\t<Prefab SubtypeId=\"{fileName}\">\n";
            output += "\t\t\t<Position>\n";
            output += "\t\t\t\t<X>0.0</X>\n";
            output += "\t\t\t\t<Y>0.0</Y>\n";
            output += "\t\t\t\t<Z>0.0</Z>\n";
            output += "\t\t\t</Position>\n";

            Console.WriteLine("Speed: ");
            string speed = Console.ReadLine();
            output += $"\t\t\t<Speed>{speed}</Speed>\n";
            output += "\t\t\t<Behaviour></Behaviour>\n";
            output += "\t\t\t<BehaviourActivationDistance>20000</BehaviourActivationDistance>\n";
            output += "\t\t</Prefab>\n";
            output += "\t</Prefabs>\n";
            output += "</Prefabs>\n";

            Console.WriteLine("Save to file? (Y/N) ");
            string toFile = Console.ReadLine();

            if(toFile == "Y")
            {
                if(!File.Exists("SpawnGroups.sbc"))
                    File.Create("SpawnGroups.sbc");

                FileStream file = File.OpenWrite("SpawnGroups.sbc");

                byte[] data = Encoding.ASCII.GetBytes(output);
                
                file.Write(data);

                Console.WriteLine("Saved!");
            }
            else
            {
                Console.Write(output);
            }

            Console.ReadKey();
        }
    }
}
