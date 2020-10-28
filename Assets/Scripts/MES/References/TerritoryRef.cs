using UnityEngine;

public class TerritoryRef : MonoBehaviour
{
    public int MyTerritoryId;

    public SpawnGroup GetMyTerritory()
    {
        return ModDataHolder.AllTerritories[MyTerritoryId];
    }
}
