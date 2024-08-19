using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 cameraOffset;
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's position
        Vector3 playerPosition = player.transform.position;

        // Adjust the camera's y-position based on the player's level
        float adjustedY = cameraOffset.y + (playerController.currentLevel / 2);

        // Set the camera's position, keeping x and z relative to the player, but modifying y
        gameObject.transform.position = new Vector3(playerPosition.x + cameraOffset.x, adjustedY, playerPosition.z + cameraOffset.z);
    }


}
