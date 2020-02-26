using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SpawnGroupsMaker
{
    class Program
    {
        static void Main(string[] args)
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
                throw new Exception();
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

            Console.Write(output);
            Console.ReadLine();
        }
    }
}
