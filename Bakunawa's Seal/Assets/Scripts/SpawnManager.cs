using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject[] unitsToSpawn;
    public GameObject[] spawnPoint;
    public float spawnInterval;
    public float minSpawnInterval;

    [Header("Wave")]
    public int waveCount;
    public int currentUnitCount;
    public int spawnedUnitThisWave;
    public int maxUnitCount;
    public int destroyedUnits;
    public bool onGoingWave;


    private void Start()
    {
        StartCoroutine(SpawnWaveRoutine());
    }

    void Spawn()
    {
        currentUnitCount++;
        spawnedUnitThisWave++;
        float spawnPoint_x = Random.Range(0, 100);
        float spawnPoint_y = Random.Range(0, 100);
        Vector3 whereToSpawn = new Vector3(spawnPoint_x, spawnPoint_y, 0.5f);
        //int spawnPointIndex = Random.Range(0, spawnPoint.Length);
        int randomEnemyIndex = Random.Range(0, unitsToSpawn.Length);

        //Instantiate(unitsToSpawn[randomEnemyIndex], spawnPoint[spawnPointIndex].transform.position, Quaternion.identity);
        Instantiate(unitsToSpawn[randomEnemyIndex], whereToSpawn, Quaternion.identity);

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
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.2f);
    }

    public void EnemyDestoryed()
    {
        currentUnitCount--;
        destroyedUnits++;

        if (currentUnitCount <= 0 && !onGoingWave && spawnedUnitThisWave >= maxUnitCount)
        {
            waveCount++;
            StartNewWave();
        }

    }
}
