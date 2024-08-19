using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for movement
    public float moveSpeed = 5f;


    // Reference to the character controller
    private CharacterController controller;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Handle player movement
        HandleMovement();

        // Handle player rotation to face the mouse cursor
        HandleRotation();
    }

    void HandleMovement()
    {
        // Get input from player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Move the player
        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = direction * moveSpeed * Time.deltaTime;
            controller.Move(moveDir);
        }
    }

    void HandleRotation()
    {
        // Get the mouse position in world space
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);
            Vector3 direction = (pointToLook - transform.position).normalized;
            direction.y = 0f; // Keep the player upright, don't tilt the character

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed * 10); //*10 to rotate faster
        }
    }
}

