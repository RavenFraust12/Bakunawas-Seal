using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //[Header("Scripts")]

    [Header("Spawn")]
    public GameObject[] unitsToSpawn;
    public float spawnInterval;
    public float minSpawnInterval;
    public float enemyStatIncrement;
    public float currentStat;

    [Header("Z Axis Spawn Point")]
    public int zMinSpawnPoint;
    public int zMaxSpawnPoint;
    public int zMinRandom;
    public int zMaxRandom;

    [Header("X Axis Spawn Point")]
    public int xMinSpawnPoint;
    public int xMaxSpawnPoint;
    public int xMinRandom;
    public int xMaxRandom;

    [Header("Wave")]
    public int waveCount;
    public int currentUnitCount;
    public int spawnedUnitThisWave;
    public int maxUnitCount;
    public int killedUnits;
    public bool onGoingWave;

    private void Start()
    {
        StartCoroutine(SpawnWaveRoutine());
    }

    void Spawn()
    {
        currentUnitCount++;
        spawnedUnitThisWave++;

        //Set the point for x and z axis
        int xspawnpoint = Random.Range(0, 2) == 0 ? xMinSpawnPoint : xMaxSpawnPoint;
        int zspawnpoint = Random.Range(0, 2) == 0 ? zMinSpawnPoint : zMaxSpawnPoint;

        Vector3 spawnPosition;
        if (Random.Range(0, 2) == 0)
        {
            // x is fixed, z is random between 0 and 100
            spawnPosition = new Vector3(xspawnpoint, 0.5f, Random.Range(zMinRandom, zMaxRandom));
        }
        else
        {
            // z is fixed, x is random between 0 and 100
            spawnPosition = new Vector3(Random.Range(xMinRandom, xMaxRandom), 0.5f, zspawnpoint);
        }

        int randomEnemyIndex = Random.Range(0, unitsToSpawn.Length);

        GameObject newEnemy = Instantiate(unitsToSpawn[randomEnemyIndex], spawnPosition, Quaternion.identity, this.transform);

        EnemyStats enemyStats = newEnemy.GetComponent<EnemyStats>();

        enemyStats.currentStat = currentStat;
        currentStat += enemyStatIncrement;

        if (spawnedUnitThisWave >= maxUnitCount)
        {
            onGoingWave = false;
        }
    }

    IEnumerator SpawnWaveRoutine()
    {
        while (true)
        {
            if (!onGoingWave && currentUnitCount == 0)
            {
                StartNewWave();
            }

            if (onGoingWave && spawnedUnitThisWave < maxUnitCount)
            {
                yield return new WaitForSeconds(spawnInterval);
                Spawn();
            }

            yield return null;
        }
    }
    
    void StartNewWave()
    {
        Debug.Log("Starting Wave: " + waveCount);
        onGoingWave = true;
        spawnedUnitThisWave = 0;

        maxUnitCount += 10;
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.1f);
    }

    public void EnemyDestroyed()
    {
        currentUnitCount--;
        killedUnits++;

        if (currentUnitCount <= 0 && !onGoingWave && spawnedUnitThisWave >= maxUnitCount)
        {
            waveCount++;
            StartNewWave();
        }

    }
}
