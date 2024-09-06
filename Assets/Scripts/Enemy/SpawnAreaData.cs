using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnAreaData", menuName = "ScriptableObjects/SpawnAreaData", order = 2)]
public class SpawnAreaData : ScriptableObject
{
    public List<SpawnArea> spawnAreas = new List<SpawnArea>();
}
