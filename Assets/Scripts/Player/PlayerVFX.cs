using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] VisualEffect smokeTrail;
    public float particleScale = 0.5f;

    private Vector3 lastPlayerPosition;
    private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        smokeTrail = Instantiate(smokeTrail, transform.position, transform.rotation, transform);
        smokeTrail.SetFloat("Spawn Rate", 0);
        smokeTrail.enabled = true;
        lastPlayerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the player's speed
        playerSpeed = Vector3.Distance(transform.position, lastPlayerPosition) / Mathf.Max(Time.deltaTime, 0.0001f);

       if (playerSpeed >= 0.15f)
        {
            smokeTrail.SetFloat("Spawn Rate", 8);
        }
        else
        {
            smokeTrail.SetFloat("Spawn Rate", 0);
        }
        // Update the last player position
        lastPlayerPosition = transform.position;
    }

}

