using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class RandomObjectSpawner : MonoBehaviour
{
    // Arrays of tree and rock prefabs
    public GameObject[] treePrefabs;
    public GameObject[] rockPrefabs;
    public GameObject objectHolder;

    // Terrain dimensions (width and length)
    public float terrainWidth;
    public float terrainLength;
    public float minSpawnHeight;
    public float maxSpawnHeight;

    // Number of objects to spawn
    public int numberOfTrees = 10;
    public int numberOfRocks = 5;

    // Reference to the Terrain
    public Terrain terrain;
    //public NavMeshSurface navMesh;

    // Start is called before the first frame update
    void Start()
    {
        //navMesh = FindObjectOfType<NavMeshSurface>();
        SpawnObjects(treePrefabs, numberOfTrees);
        SpawnObjects(rockPrefabs, numberOfRocks);
    }

    void SpawnObjects(GameObject[] prefabs, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Generate random position within the terrain bounds
            Vector3 spawnPosition = new Vector3(
                Random.Range(15, terrainWidth),
                Random.Range(minSpawnHeight, maxSpawnHeight),
                Random.Range(5, terrainLength)
            );

            // Adjust the Y position based on the terrain height at the x, z location
            spawnPosition.y = terrain.SampleHeight(spawnPosition);

            // Randomly select a prefab from the array
            GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

            // Set the rotation to -90 degrees on the X-axis
            Quaternion rotation = Quaternion.Euler(-90, 0, Random.Range(0,360));

            // Instantiate the selected prefab at the random position with the specified rotation
            Instantiate(prefabToSpawn, spawnPosition, rotation, objectHolder.transform);

            //navMesh.BuildNavMesh();
        }
    }
    //public GameObject[] treePrefabs;
    //public GameObject[] rockPrefabs;

    //// Terrain dimensions (width and length)
    //public float terrainWidth = 100f;
    //public float terrainLength = 100f;
    //public float minSpawnHeight = 0f;
    //public float maxSpawnHeight = 2f;

    //// Number of objects to spawn
    //public int numberOfTrees = 10;
    //public int numberOfRocks = 5;

    //// Reference to the Terrain
    //public Terrain terrain;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    SpawnObjects(treePrefabs, numberOfTrees);
    //    SpawnObjects(rockPrefabs, numberOfRocks);
    //}

    //void SpawnObjects(GameObject[] prefabs, int amount)
    //{
    //    for (int i = 0; i < amount; i++)
    //    {
    //        // Generate random position within the terrain bounds
    //        Vector3 spawnPosition = new Vector3(
    //            Random.Range(0, terrainWidth),
    //            Random.Range(minSpawnHeight, maxSpawnHeight),
    //            Random.Range(0, terrainLength)
    //        );

    //        // Adjust the Y position based on the terrain height at the x, z location
    //        spawnPosition.y = terrain.SampleHeight(spawnPosition);

    //        // Randomly select a prefab from the array
    //        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];

    //        // Instantiate the selected prefab at the random position
    //        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    //    }
    //}
}

