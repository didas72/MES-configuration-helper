using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup
{
    public string SubtypeId;
    public string Description;
    public double Frequency;
    public List<Prefab> Prefabs;

    public SpawnGroup()
    {
        SubtypeId = "";
        Description = "";
        Frequency = 1.0;
        Prefabs = new List<Prefab>();
    }
    public SpawnGroup(string SubtypeId, string Description, double Frequency, List<Prefab> Prefabs)
    {
        this.SubtypeId = SubtypeId;
        this.Description = Description;
        this.Frequency = Frequency;
        this.Prefabs = Prefabs;
    }

    public bool AddPrefab(Prefab pre)
    {
        try
        {
            Prefabs.Add(pre);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
