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
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    public float spawnAmount = 1f;

    // Reference to store the time since the last spawn
    private float timeSinceLastSpawn;
    public GameObject playerController;

    private void Start() {
        
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
    }

    void SpawnObject()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            // Generate a random position within the spawn area
            float spawnPosX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float spawnPosZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

            // Set the spawn position relative to the spawner's position
            Vector3 spawnPosition = new Vector3(spawnPosX, 0f, spawnPosZ) + transform.position;

            // Instantiate the object at the calculated position
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }

    }

    private void ScaleSpawnAmount() {
        
    }

    // Optional: Visualize the spawn area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 1f, spawnAreaSize.z));
    }
}
