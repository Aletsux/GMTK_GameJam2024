using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerController playerController;
    public AIController aIController;
    public ParticleSystem particleEffect;
    // Reference to the Animator component

    // The name of the attack animation trigger
    public string attackAnimationTriggerName = "Attack";

    private Animator animator;
    private bool isEffectPlaying = false;


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

        TriggerEffect();
    }

    void TriggerAttack()
    {
        // Ensure the Animator component is available
        if (animator != null)
        {
            // Trigger the attack animation
            animator.SetTrigger(attackAnimationTriggerName);
            ChangeParticleSize();

            if (!isEffectPlaying)
            {
                particleEffect.Play();
                isEffectPlaying = true;
            }


        }
    }
    private void TriggerEffect()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(attackAnimationTriggerName))
        {
            if (stateInfo.normalizedTime >= 1f)
            {
                isEffectPlaying = false;
            }
        }
    }

    private void ChangeParticleSize()
    {
        Vector3 playerScale = playerController.gameObject.transform.localScale;
        var mainModule = particleEffect.main;

        float newSize = (playerScale.x + playerScale.y + playerScale.z) / 3f * 10f;
        mainModule.startSize = newSize;

    }





}
