using UnityEngine;

public class Prefab
{
    public string SubtypeId;
    public Vector3 Position;
    public string BeaconText;
    public double Speed;

    public Prefab()
    {
        SubtypeId = "";
        Position = Vector3.zero;
        BeaconText = "";
        Speed = 0.0;
    }
    public Prefab(string SubtypeId, string BeaconText, double Speed)
    {
        this.SubtypeId = SubtypeId;
        this.BeaconText = BeaconText;
        this.Speed = Speed;
    }
    public Prefab(string SubtypeId, Vector3 Position, string BeaconText, double Speed)
    {
        this.SubtypeId = SubtypeId;
        this.Position = Position;
        this.BeaconText = BeaconText;
        this.Speed = Speed;
    }
}
