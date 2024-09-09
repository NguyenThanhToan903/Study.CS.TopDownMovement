using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{

    [SerializeField] private ListEnemyVariable enemies; // Ds cac enemy
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
    private bool IsCircleIntersectingRectangle(Vector3 circleCenter, float radius, Vector3 rectMin, Vector3 rectMax)
    {
        // Tìm điểm gần nhất trên hình chữ nhật đến tâm vòng tròn
        Vector3 closestPoint = new Vector3(
            Mathf.Clamp(circleCenter.x, rectMin.x, rectMax.x),
            Mathf.Clamp(circleCenter.y, rectMin.y, rectMax.y),
            circleCenter.z
        );

        // Tính khoảng cách từ tâm vòng tròn đến điểm gần nhất
        float distance = Vector3.Distance(circleCenter, closestPoint);
        return distance <= radius;
    }

    private Vector3 GetClampedPosition(Vector3 position, float radius, Vector3 rectMin, Vector3 rectMax)
    {
        // Clamp position within the bounds of the rectangle
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(position.x, rectMin.x + radius, rectMax.x - radius),
            Mathf.Clamp(position.y, rectMin.y + radius, rectMax.y - radius),
            position.z
        );
        return clampedPosition;
    }

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
                    Random.Range(AreaPointA.x + maximumRadius, AreaPointB.x - maximumRadius),
                    Random.Range(AreaPointA.y + maximumRadius, AreaPointB.y - maximumRadius),
                    0
                );

                zoneRadius = Random.Range(minimumRadius, maximumRadius);

                // Adjust position if it intersects with the rectangle
                Vector3 rectMin = new Vector3(Mathf.Min(AreaPointA.x, AreaPointB.x), Mathf.Min(AreaPointA.y, AreaPointB.y), 0);
                Vector3 rectMax = new Vector3(Mathf.Max(AreaPointA.x, AreaPointB.x), Mathf.Max(AreaPointA.y, AreaPointB.y), 0);
                spawnPosition = GetClampedPosition(spawnPosition, zoneRadius, rectMin, rectMax);

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


        Gizmos.color = Color.green;
        Vector3 center = (AreaPointA + AreaPointB) / 2f;
        Vector3 size = new Vector3(Mathf.Abs(AreaPointA.x - AreaPointB.x),
                                   Mathf.Abs(AreaPointA.y - AreaPointB.y),
                                   Mathf.Abs(AreaPointA.z - AreaPointB.z));
        Gizmos.DrawWireCube(center, size);
    }
}

