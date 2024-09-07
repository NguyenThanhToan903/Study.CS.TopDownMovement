using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private ListEnemyVariable enemies; // Reference to the ScriptableObject
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int totalEnemyCount = 20;

    [SerializeField] private Vector3 AreaPointA;
    [SerializeField] private Vector3 AreaPointB;

    [SerializeField] private int minimumZones = 2;
    [SerializeField] private int maximumZones = 4;
    [SerializeField] private int ZoneCount;

    [SerializeField] private float minimumRadius = 2.0f;
    [SerializeField] private float maximumRadius = 4.0f;

    private bool hasMiniSpawnedArea = false;

    [System.Serializable]
    public struct SpawnZone
    {
        public Vector3 Center;
        public float Radius;
        public float SpawnRatio; // Tỉ lệ spawn cho khu vực này
    }

    [SerializeField] private List<SpawnZone> spawnZones = new List<SpawnZone>();

    private void Start()
    {
        InitSpawn();
    }

    private void InitSpawn()
    {
        if (!hasMiniSpawnedArea)
        {
            ZoneCount = Random.Range(minimumZones, maximumZones + 1);
            CreateSpawnZones();
            hasMiniSpawnedArea = true;
        }
    }

    //private void CreateSpawnZones()
    //{
    //    spawnZones.Clear();
    //    float totalSpawnRatio = 0f;

    //    for (int i = 0; i < ZoneCount; i++)
    //    {
    //        bool validPosition = false;
    //        Vector3 spawnPosition = Vector3.zero;
    //        float zoneRadius;

    //        // Try to find a valid position for the spawn zone
    //        while (!validPosition)
    //        {
    //            spawnPosition = new Vector3(
    //                Random.Range(AreaPointA.x, AreaPointB.x),
    //                Random.Range(AreaPointA.y, AreaPointB.y),
    //                0
    //            );

    //            zoneRadius = Random.Range(minimumRadius, maximumRadius);

    //            validPosition = true;

    //            // Check for overlap with existing zones
    //            foreach (var existingZone in spawnZones)
    //            {
    //                float distance = Vector3.Distance(spawnPosition, existingZone.Center);
    //                if (distance < (zoneRadius + existingZone.Radius + 1)) // 1 unit minimum distance
    //                {
    //                    validPosition = false;
    //                    break;
    //                }
    //            }
    //        }

    //        float spawnRatio = Random.Range(0.1f, 1.0f);

    //        spawnZones.Add(new SpawnZone { Center = spawnPosition, Radius = zoneRadius, SpawnRatio = spawnRatio });
    //        totalSpawnRatio += spawnRatio;
    //    }

    //    // Normalize spawn ratios so they sum to 1
    //    for (int i = 0; i < spawnZones.Count; i++)
    //    {
    //        var zone = spawnZones[i];
    //        zone.SpawnRatio /= totalSpawnRatio;
    //        spawnZones[i] = zone;
    //    }

    //    SpawnEnemiesInZones();
    //}

    private void CreateSpawnZones()
    {
        spawnZones.Clear();
        float totalSpawnRatio = 0f;

        for (int i = 0; i < ZoneCount; i++)
        {
            bool validPosition = false;
            Vector3 spawnPosition = Vector3.zero;
            float zoneRadius = 0f;

            // Try to find a valid position for the spawn zone
            while (!validPosition)
            {
                spawnPosition = new Vector3(
                    Random.Range(AreaPointA.x, AreaPointB.x),
                    Random.Range(AreaPointA.y, AreaPointB.y),
                    0
                );

                zoneRadius = Random.Range(minimumRadius, maximumRadius);

                validPosition = true;

                // Check for overlap with existing zones
                foreach (var existingZone in spawnZones)
                {
                    float distance = Vector3.Distance(spawnPosition, existingZone.Center);
                    if (distance < (zoneRadius + existingZone.Radius + 1)) // 1 unit minimum distance
                    {
                        validPosition = false;
                        break;
                    }
                }
            }

            float spawnRatio = Random.Range(0.1f, 1.0f);

            spawnZones.Add(new SpawnZone { Center = spawnPosition, Radius = zoneRadius, SpawnRatio = spawnRatio });
            totalSpawnRatio += spawnRatio;
        }

        // Normalize spawn ratios so they sum to 1
        for (int i = 0; i < spawnZones.Count; i++)
        {
            var zone = spawnZones[i];
            zone.SpawnRatio /= totalSpawnRatio;
            spawnZones[i] = zone;
        }

        SpawnEnemiesInZones();
    }


    private void SpawnEnemiesInZones()
    {
        foreach (var zone in spawnZones)
        {
            int enemiesInZone = Mathf.RoundToInt(totalEnemyCount * zone.SpawnRatio);
            SpawnEnemies(zone.Center, zone.Radius, enemiesInZone);
        }
    }

    private void SpawnEnemies(Vector3 center, float radius, int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition;
            do
            {
                Vector2 randomCircle = Random.insideUnitCircle * radius;
                spawnPosition = center + new Vector3(randomCircle.x, randomCircle.y, 0);
                spawnPosition.z = 0;
            } while (!IsPositionWithinBounds(spawnPosition, center, radius));

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (enemy.TryGetComponent<EnemyMovement>(out var enemyMovement))
            {
                enemies.Add(enemyMovement);
            }
        }
    }

    private bool IsPositionWithinBounds(Vector3 position, Vector3 center, float radius)
    {
        return Vector3.Distance(position, center) <= radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var zone in spawnZones)
        {
            Gizmos.DrawWireSphere(zone.Center, zone.Radius);
        }
    }
}
