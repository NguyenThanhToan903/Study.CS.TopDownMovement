using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemyMovement> enemies = new List<EnemyMovement>();
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] public BoxCollider2D spawnArea;

    [SerializeField] private SpawnAreaData spawnAreaData; // Chứa danh sách các SpawnArea
    [SerializeField] private int totalEnemyCount = 20;

    private bool hasSpawned = false;

    private void Start()
    {
        if (!hasSpawned)
        {
            SpawnEnemies();
            hasSpawned = true;
        }
    }

    private void SpawnEnemies()
    {
        if (enemies.Count > 0) enemies.Clear();

        Bounds bounds = spawnArea.bounds;
        List<SpawnArea> areas = spawnAreaData.spawnAreas;

        foreach (var areaData in areas)
        {
            Bounds spawnBounds = new Bounds(new Vector3(areaData.center.x, areaData.center.y, 0), new Vector3(areaData.radius * 2, areaData.radius * 2, bounds.size.z));
            int enemiesInArea = Mathf.RoundToInt(totalEnemyCount * GetAreaDensity(spawnBounds, bounds));

            for (int i = 0; i < enemiesInArea; i++)
            {
                Vector3 position = GetRandomPositionInCircle(spawnBounds);
                float direction = Random.Range(0f, 360f);
                GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.Euler(Vector3.forward * direction) * enemyPrefab.transform.localRotation);
                enemy.transform.SetParent(transform);
                enemies.Add(enemy.GetComponent<EnemyMovement>());
            }
        }
    }

    private float GetAreaDensity(Bounds area, Bounds fullBounds)
    {
        float areaSize = area.size.x * area.size.y;
        float fullSize = fullBounds.size.x * fullBounds.size.y;
        return areaSize / fullSize;
    }

    private Vector3 GetRandomPositionInCircle(Bounds circleBounds)
    {
        Vector3 center = circleBounds.center;
        float radius = circleBounds.extents.x;

        Vector2 randomDirection = Random.insideUnitCircle * radius;
        return new Vector3(center.x + randomDirection.x, center.y + randomDirection.y, 0);
    }

    private void OnDrawGizmos()
    {
        if (spawnArea != null && spawnAreaData != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(spawnArea.transform.position, spawnArea.size);

            Gizmos.color = Color.red;
            foreach (var area in spawnAreaData.spawnAreas)
            {
                Gizmos.DrawWireSphere(new Vector3(area.center.x, area.center.y, 0), area.radius);
            }
        }
    }
}
