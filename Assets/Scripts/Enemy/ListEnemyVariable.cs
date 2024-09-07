using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/List GameObject Manager")]

public class ListEnemyVariable : ScriptableObject
{
    public List<EnemyMovement> enemies = new List<EnemyMovement>();

    public void Add(EnemyMovement enemy)
    {
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }
}
