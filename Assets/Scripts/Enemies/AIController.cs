using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Start is called before the first frame update// Time interval in seconds for scaling up the GameObject
    public float interval = 5.0f;

    // Scale increment
    public Vector3 scaleIncrement = new Vector3(0.1f, 0.1f, 0.1f);

    // Internal variable to keep track of the next scaling time
    private float nextScaleTime;

    void Start()
    {
        // Initialize the next scale time to 5 seconds after the start
        nextScaleTime = interval;
    }

    void Update()
    {
        ScaleObjectOverTime();
    }

    void ScaleObjectOverTime()
    {
        // Check if the current time has reached the next scaling time
        if (Time.time >= nextScaleTime)
        {
            // Scale the GameObject up by the increment
            transform.localScale += scaleIncrement;

            // Set the next scale time
            nextScaleTime += interval;
        }
    }
}
