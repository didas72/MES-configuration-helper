using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace Utils
{
    public static class XMLHandler
    {
        static string APPDATA = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);

        public enum FileType
        {
            SpawnGroups = 0,
            Territories,
            MOD_Prefabs,
        }

        public static void Test()
        {
            List<Prefab> prefabs = new List<Prefab>()
            {
                new Prefab("Prefab 1", "Prefab1", 25.0)
            };

            List<SpawnGroup> spawngroups = new List<SpawnGroup>() {
                new SpawnGroup("Spawngroup 1", "Description 1", 5.0, prefabs),
                new SpawnGroup("Spawngroup 2", "Description 2", 5.0, prefabs)
            };

            XDocument SpawngroupsDoc = new XDocument();
            SpawngroupsSbc(ref SpawngroupsDoc, spawngroups);
            if (SaveFile("New", FileType.SpawnGroups, SpawngroupsDoc))
                Logging.Log("Sucessfully saved!");
            else
                Logging.LogErr("Failed to save.");
        }

        public static void SpawngroupsSbc(ref XDocument document, List<SpawnGroup> spawnGroups)
        {
            XDeclaration declaration = new XDeclaration("1.0", "UTF-8", string.Empty);
            document.Declaration = declaration;

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XElement root = new XElement("Definitions",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xdi", xsd.NamespaceName)
            );

            document.Add(root);

            XElement spawngroups = new XElement("Spawngroups");
            root.Add(spawngroups);


            foreach (SpawnGroup sp in spawnGroups)
            {
                spawngroups.Add(NewSpawnGroupXml(sp));
            }
        }

        public static void TerritoriesSbc(ref XDocument document, List<SpawnGroup> territories)
        {
            XDeclaration declaration = new XDeclaration("1.0", "UTF-8", string.Empty);
            document.Declaration = declaration;

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XElement root = new XElement("Definitions",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xdi", xsd.NamespaceName)
            );

            document.Add(root);

            XElement spawngroups = new XElement("Spawngroups");
            root.Add(spawngroups);


            foreach (SpawnGroup sp in territories)
            {
                spawngroups.Add(NewSpawnGroupXml(sp));
            }
        }

        public static XElement NewSpawnGroupXml(SpawnGroup sp)
        {
            XElement Spawngroup = new XElement("SpawnGroup");

            XElement Id = new XElement("Id",
                new XElement("TypeId", "SpawnGroupDefinition"),
                new XElement("SubtypeId", sp.SubtypeId)
            );
            Spawngroup.Add(Id);

            XElement Description = new XElement("Description", sp.Description);
            Spawngroup.Add(Description);

            XElement Icon = new XElement("Icon", "Textures\\GUI\\Icons\\Fake.dds");
            Spawngroup.Add(Icon);

            XElement Frequency = new XElement("Frequency", sp.Frequency);
            Spawngroup.Add(Frequency);

            XElement Prefabs = new XElement("Prefabs");
            foreach (Prefab pre in sp.Prefabs)
            {
                Prefabs.Add(NewPrefabXml(pre));
            }
            Spawngroup.Add(Prefabs);

            return Spawngroup;
        }

        public static XElement NewPrefabXml(Prefab pre)
        {
            XElement Prefab = new XElement("Prefab",
                new XAttribute("SubtypeId", pre.SubtypeId)
            );

            XElement Position = new XElement("Position",
                new XElement("x", pre.Position.x),
                new XElement("y", pre.Position.y),
                new XElement("z", pre.Position.z)
            );
            Prefab.Add(Position);

            XElement BeaconText = new XElement("BeaconText", pre.BeaconText);
            Prefab.Add(BeaconText);

            XElement Speed = new XElement("Speed", pre.Speed);
            Prefab.Add(Speed);

            return Prefab;
        }

        public static bool LoadFile(string modName, FileType type, out XDocument document)
        {
            if(type != FileType.MOD_Prefabs)
            {
                try
                {
                    document = XDocument.Load(APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\" + type.ToString() + ".sbc");
                }
                catch(System.Exception e)
                {
                    Logging.LogErr($"Failed to load {type} file from {APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\" + type.ToString() + ".sbc"} with exception {e}");

                    document = new XDocument();
                    return false;
                }

                Logging.Log($"Loaded {type} file sucessfully from {APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\" + type.ToString() + ".sbc"}");

                return true;
            }
            else
            {
                try
                {
                    document = XDocument.Load(APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\Prefabs.mrch");
                }
                catch(System.Exception e)
                {
                    Logging.LogErr($"Failed to load {type} file from {APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\" + type.ToString() + ".sbc"} with exception {e}");

                    document = new XDocument();
                    return false;
                }

                Logging.Log($"Loaded {type} file sucessfully from {APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\Prefabs.mrch"}");
                
                return true;
            }
        }

        public static bool LoadFile(string path, out XDocument document)
        {
            try
            {
                document = XDocument.Load(path);
            }
            catch(System.Exception e)
            {
                Logging.LogErr($"Failed to load file from {path} with exception {e}");

                document = new XDocument();
                return false;
            }

            Logging.Log($"Loaded file sucessfully from {path}");

            return true;
        }

        public static bool SaveFile(string modName, FileType type, XDocument document)
        {
            if(type != FileType.MOD_Prefabs)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\" + type.ToString() + ".sbc");
                    document.Save(writer);
                }
                catch
                {
                    return false;
                }

                
                
                return true;
            }
            else
            {
                try
                {
                    StreamWriter writer = new StreamWriter(APPDATA + "\\SpaceEngineers\\Mods\\" + modName + "\\Data\\Prefabs.mrch");
                    document.Save(writer);
                }
                catch
                {
                    return false;
                }

                return true;
            }
        }

        public static bool SaveFile(string path, XDocument document)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path);
                document.Save(writer);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}