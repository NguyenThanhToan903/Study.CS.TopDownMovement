using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/List GameObject Manager")]

public class ListEnemyVariable : ScriptableObject
{
    public List<EnemyMovement> enemyMovements = new List<EnemyMovement>();
}
