using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Reference to the Text component where the timer will be displayed
    public Text timerText;

    // Internal variable to store the elapsed time
    private float elapsedTime;

    void Start()
    {
        // Initialize the elapsed time to 0
        elapsedTime = 0f;

        // Ensure the timer text is cleared at the start
        if (timerText != null)
        {
            timerText.text = "Time: 0.00s";
        }
        else
        {
            Debug.LogError("Timer Text component is not assigned.");
        }
    }

    void Update()
    {
        // Update the elapsed time based on the time since the game started
        elapsedTime += Time.deltaTime;

        // Format and display the elapsed time
        if (timerText != null)
        {
            timerText.text = "Time: " + elapsedTime.ToString("F2") + "s";
        }
    }
}
