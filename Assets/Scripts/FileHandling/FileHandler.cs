using System;
using System.IO;
using UnityEngine;

namespace Utils
{
    public class FileHandler
    {
        public static bool OpenMod()
        {
            if (XMLHandler.LoadFile(ModDataHolder.mod.Name, XMLHandler.FileType.SpawnGroups, out ModDataHolder.mod.SpawnGroupsFile))
            {
                Logging.Log("Spawngroups file loaded sucessfully");
                
            }
            else
            {
                Logging.LogErr("Couldn't load spawngorups file");
                ModDataHolder.mod.SpawnGroupsFile = new System.Xml.Linq.XDocument();
                return false;
            }

            if (XMLHandler.LoadFile(ModDataHolder.mod.Name, XMLHandler.FileType.Territories, out ModDataHolder.mod.TerritoriesFile))
            {
                Logging.Log("Territories file loaded sucessfully");
            }
            else
            {
                Logging.LogErr("Couldn't load territories file");
                ModDataHolder.mod.TerritoriesFile = new System.Xml.Linq.XDocument();
                return false;
            }

            return true;
        }

        public static bool CreateMod()
        {
            try
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SpaceEngineers\\Mods\\" + ModDataHolder.mod.Name);
            }
            catch
            {
                Logging.LogErr($"Couldn't create folder with path {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SpaceEngineers\\Mods\\" + ModDataHolder.mod.Name}");
                return false;
            }

            try
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SpaceEngineers\\Mods\\" + ModDataHolder.mod.Name + "\\Data");
            }
            catch
            {
                Logging.LogErr($"Couldn't create folder with path {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SpaceEngineers\\Mods\\" + ModDataHolder.mod.Name + "\\Data"}");
                return false;
            }

            ModDataHolder.mod.SpawnGroupsFile = new System.Xml.Linq.XDocument();
            ModDataHolder.mod.TerritoriesFile = new System.Xml.Linq.XDocument();
            ModDataHolder.mod.PrefabsFile = new System.Xml.Linq.XDocument();

            return true;
        }

        public static bool SaveMod()
        {
            XMLHandler.SpawngroupsSbc(ref ModDataHolder.mod.SpawnGroupsFile, ModDataHolder.AllSpawnGroups);
            XMLHandler.TerritoriesSbc(ref ModDataHolder.mod.TerritoriesFile, ModDataHolder.AllTerritories);

            if (!XMLHandler.SaveFile(ModDataHolder.mod.Name, XMLHandler.FileType.SpawnGroups, ModDataHolder.mod.SpawnGroupsFile))
            {
                Logging.LogErr($"Couldn't save the spawngroups file with mod name {ModDataHolder.mod.Name}");
                return false;
            }

            if (!XMLHandler.SaveFile(ModDataHolder.mod.Name, XMLHandler.FileType.Territories, ModDataHolder.mod.TerritoriesFile))
            {
                Logging.LogErr($"Couldn't save the territories file with mod name {ModDataHolder.mod.Name}");
                return false;
            }

            if(!XMLHandler.SaveFile(ModDataHolder.mod.Name, XMLHandler.FileType.MOD_Prefabs, ModDataHolder.mod.PrefabsFile))
            {
                Logging.LogErr($"Couldn't save the territories file with mod name {ModDataHolder.mod.Name}");
                return false;
            }

            ModDataHolder.mod = new Mod();
            ModDataHolder.AllPrefabs = new System.Collections.Generic.List<Prefab>();
            ModDataHolder.AllSpawnGroups = new System.Collections.Generic.List<SpawnGroup>();
            ModDataHolder.AllTerritories = new System.Collections.Generic.List<SpawnGroup>();

            return true;
        }
    }

    /*public class Log
    {
        public Type type;
        public int code;
        public string name;
        public string description;

        public Log(Type type, int code, string name, string description)
        {
            this.type = type;
            this.code = code;
            this.name = name;
            this.description = description;
        }

        //ERRORS
        public Log Prefab_File_Not_Found(string path)
        {
            return new Log(Type.ERR, 0, "PREFABS_FILE_NOT_FOUND", $"Couldn't load the Prefabs file because it wasn't found. Path given was {path}");
        }
        public Log Spawngroup_File_Not_Found(string path)
        {
            return new Log(Type.ERR, 1, "SPAWNGROUPS_FILE_NOT_FOUND", $"Couldn't load the Spawngroups file because it wasn't found. Path given was {path}");
        }
        public Log Territories_File_Not_Found(string path)
        {
            return new Log(Type.ERR, 2, "TERRITORIES_FILE_NOT_FOUND", $"Couldn't load the Territories file because it wasn't found. Path given was {path}");
        }

        //WARNINGS

        //LOGS
        public Log Prefab_File_Loaded(string path)
        {
            return new Log(Type.LOG, 0, "Pref", $"Couldn't load the Spawngroup file because it wasn't found. Path given was {path}");
        }

        public enum Type
        {
            LOG = 0,
            WRN,
            ERR,
        }
    }*/
}