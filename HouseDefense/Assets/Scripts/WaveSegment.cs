using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WaveSegment{

    public EnemyPrefab enemyToSpawn;
    public int NumberOfEnemies = 1;
    public int EnemyLevel = 0;

    public WaveSegment(EnemyPrefab enemyToSpawn, int numberOfEnemies = 1, int enemyLevel = 0)
    {
        this.enemyToSpawn = enemyToSpawn;
        NumberOfEnemies = numberOfEnemies;
        EnemyLevel = enemyLevel;
    }
}
