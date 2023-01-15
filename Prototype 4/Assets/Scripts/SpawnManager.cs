using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Variables for the prefabs.
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn powerups with new waves.
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    // Method that spawn the enemy wave.
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Method that return the spawn point of the prefabs at different location.
    private Vector3 GenerateSpawnPosition()
    {
        // Variables that contains random spawning point for the prefabs.
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
