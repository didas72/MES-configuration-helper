using UnityEngine;

public class PrefabRef : MonoBehaviour
{
    public int MyPrefabId;

    public Prefab GetMyPrefab()
    {
        return ModDataHolder.AllPrefabs[MyPrefabId];
    }
}
