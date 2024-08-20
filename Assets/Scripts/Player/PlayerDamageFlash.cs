using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    public float flashDuration = 0.2f;
    private float flashTimer = 0f;

    private Renderer objectRenderer;
    private MaterialPropertyBlock propertyBlock;
    private bool isFlashing = false;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        // Initialize the MaterialPropertyBlock
        propertyBlock = new MaterialPropertyBlock();

        // Ensure the flash alpha is initially set to 0
        objectRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_FlashAlpha", 0);
        objectRenderer.SetPropertyBlock(propertyBlock);


        //Damage Flash timer
        flashTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the flash if it's active
        if (isFlashing)
        {
            HandleFlash();
        }
    }

    public void StartFlash()
    {
        // Reset the elapsed time and start the flash effect
        flashTimer = 0f;
        isFlashing = true;

        // Set the FlashAlpha to 1 to start the flash
        objectRenderer.GetPropertyBlock(propertyBlock);
        propertyBlock.SetFloat("_FlashAlpha", 1);
        objectRenderer.SetPropertyBlock(propertyBlock);
    }

    public void HandleFlash()
    {
        // Increment the elapsed time
        flashTimer += Time.deltaTime;

        // Check if the flash duration has been reached
        if (flashTimer >= flashDuration)
        {
            // Reset the FlashAlpha to 0
            objectRenderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat("_FlashAlpha", 0);
            objectRenderer.SetPropertyBlock(propertyBlock);

            isFlashing = false; // Stop the flashing
        }
    }
}
