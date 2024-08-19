using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // The prefab to spawn
    public GameObject objectToSpawn;

    // The time interval between spawns
    public float spawnInterval = 2f;

    // The area within which objects will spawn
    public Vector3 spawnAreaSize = new Vector3(10f, 5f, 10f);
    public Vector3 maxSpawnArea = new Vector3(100f, 5f, 100f);
    private float baseSpawnAmount = 1f;
    public float spawnAmount = 0f;
    public float minSpawnDistance = 5f;

    // Reference to store the time since the last spawn
    private float timeSinceLastSpawn;

    public PlayerController playerController;

    private void Start()
    {
        spawnAmount = baseSpawnAmount;
    }
    void Update()
    {
        // Increment the time since the last spawn
        timeSinceLastSpawn += Time.deltaTime;

        // Check if the spawn interval has passed
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f; // Reset the time since last spawn
        }

        Vector3 playerPosition = playerController.gameObject.transform.position;
        transform.position = playerPosition;
    }


    void SpawnObject()
    {
        float playerSize = playerController.transform.localScale.magnitude;
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 spawnPosition;
            int attempts = 0;
            do
            {
                // Generate a random position within the spawn area
                float spawnPosX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
                float spawnPosZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

                // Set the spawn position relative to the spawner's position with an offset
                spawnPosition = new Vector3(spawnPosX, 0f, spawnPosZ) + transform.position;
                // Increment the attempts counter
                attempts++;
            }
            while (Vector3.Distance(spawnPosition, playerController.transform.position) < playerSize + minSpawnDistance && attempts < 10);

            // Instantiate the object at the calculated position if the distance is valid
            if (attempts < 10)
            {
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
            }
        }

    }

    public void ScaleSpawnAmount()
    {
        // Get the current level from the PlayerController
        float currentLvl = playerController.currentLevel;

        spawnAmount = baseSpawnAmount + (currentLvl * 2);

        if (spawnAreaSize.magnitude > maxSpawnArea.magnitude)
        {
            spawnAreaSize = maxSpawnArea;
        }

        else
        {
            float adjustedX = spawnAreaSize.x + currentLvl * 1.5f;
            float adjustedZ = spawnAreaSize.z + currentLvl * 1.5f;
            spawnAreaSize = new Vector3(adjustedX, spawnAreaSize.y, adjustedZ);
        }


        // Calculate the number of enemies to spawn using exponential growth
        //spawnAmount = baseSpawnAmount * (int)Mathf.Pow(2, currentLvl);

        // Alternatively, you can use another growth factor, like tripling every level:
        // numberOfEnemiesToSpawn = baseEnemiesToSpawn * (int)Mathf.Pow(3, currentLvl);
    }

    // Optional: Visualize the spawn area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 1f, spawnAreaSize.z));
    }
}
