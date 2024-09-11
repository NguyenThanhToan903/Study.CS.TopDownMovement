﻿using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private ListEnemyVariable enemy;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemyCount;


    private void Start()
    {
        
    }


    private void setQuaternion()
    {
        if (enemy.enemies.Count > 0) enemy.enemies.Clear();

        for (int i = 0; i < enemyCount; i++)
        {
            float direction = Random.Range(0.0f, 360.0f);
        }
    }



}