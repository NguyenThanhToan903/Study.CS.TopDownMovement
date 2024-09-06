using UnityEngine;

[CreateAssetMenu(fileName = "SpawnArea", menuName = "ScriptableObjects/SpawnArea", order = 1)]
public class SpawnArea : ScriptableObject
{
    public Vector2 center;
    public float radius;
}
