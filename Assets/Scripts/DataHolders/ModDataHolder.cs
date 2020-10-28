using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModDataHolder
{
    public static List<Prefab> AllPrefabs = new List<Prefab>();
    public static List<SpawnGroup> AllSpawnGroups = new List<SpawnGroup>();
    public static List<SpawnGroup> AllTerritories = new List<SpawnGroup>();

    public static Mod mod = new Mod();
}


public struct Mod
{
    public string Name;
    public System.Xml.Linq.XDocument SpawnGroupsFile;
    public System.Xml.Linq.XDocument TerritoriesFile;
    public System.Xml.Linq.XDocument PrefabsFile;
}
