using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Speed at which the enemy moves towards the player
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    // Reference to the player's transform
    private Transform player;
    private float lifeTime = 0f;

    void Start()
    {
        // Find the player in the scene (assuming the player has a tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Check if player reference was found
        if (player == null)
        {
            Debug.LogError("Player object not found. Please ensure the player has the tag 'Player'.");
        }
    }

    void Update()
    {
        // Move the enemy towards the player on the Z and Y axes
        MoveTowardsPlayer();
        lifeTime += Time.deltaTime;
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction from the enemy to the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player along the X and Z axes
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.z);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotate the enemy mesh to face the player along the Y-axis
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}
