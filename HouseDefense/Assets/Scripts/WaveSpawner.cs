using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveSpawner : MonoBehaviour {

    public List<Transform> spawnPoints = new List<Transform>();
    [Space()]
    public Enemies EnemiesList;
    public Turrets TurretsList;
    public Walls WallsList;
    public House House;

    [Space()]
    public float WaveDelayTime = 10;
    public float currentWaveDelayTime = 0;


    [Space()]
    public List<Wave> waves = new List<Wave>();

    public SpawnerState spawnerState;

    public EnemyPrefab basicEnemy;



    void Awake () {
        TurretsList.List.Clear();
        EnemiesList.List.Clear();
        WallsList.List.Clear();
        EditorUtility.SetDirty(TurretsList);
        EditorUtility.SetDirty(EnemiesList);
        EditorUtility.SetDirty(WallsList);
        waves.Add(new Wave(new WaveSegment(basicEnemy, 2, 1)));
        waves.Add(new Wave(new WaveSegment(basicEnemy, 5, 2)));
        waves.Add(new Wave(new WaveSegment(basicEnemy, 5, 5)));
        waves.Add(new Wave(new WaveSegment(basicEnemy, 5, 10)));
    }

    private void FixedUpdate()
    {
        Spawner();
    }

    public void Spawner()
    {
        switch (spawnerState)
        {
            case SpawnerState.Spawning:
                {
                    SpawnNextWave();
                    break;
                }
            case SpawnerState.WaitingForFinish:
                {
                    CheckForFinishedWave();
                    break;
                }
            case SpawnerState.Counting:
                {
                    WaveCountDown();
                    break;
                }
            case SpawnerState.Finished:
                {
                    if (waves.Count > 0)
                    {
                        spawnerState = SpawnerState.Counting;
                    }
                    break;
                }
            default:
                break;
        }


    }

    public void SpawnNextWave()
    {
        if (spawnerState == SpawnerState.Spawning)
        {
            if (waves.Count < 1)
            {
                print("No more waves to spawn!");
                spawnerState = SpawnerState.Finished;
                return;
            }
            Wave waveToSpawn = waves[0];            

            if (waveToSpawn.segments.Count < 1)
            {
                print("No wave segments to spawn!");
                return;
            }
            foreach (var segment in waveToSpawn.segments)
            {
                if (segment.enemyToSpawn != null)
                {
                    for (int i = 0; i < segment.NumberOfEnemies; i++)
                    {
                        SpawnEnemy(segment.enemyToSpawn, segment.EnemyLevel);
                    }
                }
            }

            currentWaveDelayTime = 0;
            spawnerState = SpawnerState.WaitingForFinish;
            waves.RemoveAt(0);
        }
    }

    public void CheckForFinishedWave()
    {
        if (spawnerState == SpawnerState.WaitingForFinish && EnemiesList.List.Count == 0)
        {
            spawnerState = SpawnerState.Counting;
        }
    }

    public void WaveCountDown()
    {
        if (spawnerState == SpawnerState.Counting)
        {
            if (currentWaveDelayTime < WaveDelayTime)
            {
                currentWaveDelayTime += Time.deltaTime;
            }
            else
            {
                spawnerState = SpawnerState.Spawning;
            }
        }
    }

    public void SpawnEnemy(EnemyPrefab enemyPrefab, int level = 0)
    {
        if (enemyPrefab == null)
        {
            return;
        }
        GameObject go = Instantiate(enemyPrefab.BasePrefab, GetRandomSpawnPoint(), Quaternion.identity, transform);
        go.name = enemyPrefab.name + " lvl: " + level;
        Enemy enemy = go.AddComponent<Enemy>();
        enemy.enemyPrefab = enemyPrefab;
        enemy.Level = level;
        enemy.PopulateFromPrefab();
        enemy.EnemiesList = EnemiesList;
        enemy.TurretsList = TurretsList;
        enemy.WallsList = WallsList;
        enemy.House = House;
        EnemiesList.Register(enemy);
        

    }

    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        //print(pos);
        return pos;
    }
}

public enum SpawnerState
{
    Spawning,
    WaitingForFinish,
    Counting,
    Finished
}
