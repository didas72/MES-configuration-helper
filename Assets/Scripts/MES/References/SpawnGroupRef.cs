using UnityEngine;
using Utils;

public class SpawnGroupRef : MonoBehaviour
{
    public int MySpawnGroupId;

    public SpawnGroup GetMySpawnGroup()
    {
        Logging.Log($"SpawngroupRef with ID {MySpawnGroupId} is trying to get spawngroup out of a total of {ModDataHolder.AllSpawnGroups.Count}");
        return ModDataHolder.AllSpawnGroups[MySpawnGroupId];
    }
}
