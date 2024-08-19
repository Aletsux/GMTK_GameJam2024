using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerController playerController;
    public AIController aIController;
    // Reference to the Animator component
    private Animator animator;

    // The name of the attack animation trigger
    public string attackAnimationTriggerName = "Attack";


    void Start()
    {
        // Get the Animator component attached to the GameObject
        animator = GetComponent<Animator>();

        // Check if the Animator component is missing
        if (animator == null)
        {
            Debug.LogError("Animator component is missing from the GameObject.");
        }

    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            TriggerAttack();   
        }
    }

    void TriggerAttack()
    {
        // Ensure the Animator component is available
        if (animator != null)
        {   
            // Trigger the attack animation
            animator.SetTrigger(attackAnimationTriggerName);
            
        }
    }



    

}
