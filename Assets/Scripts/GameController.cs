using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;
    public Text deathTimestampText; // Reference to the UI Text component
    public Button restartButton;
    public Timer timer;
    private bool playerIsDead = false;

    private void Start() {
        deathTimestampText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerIsDead && IsPlayerDead())
        {
            HandlePlayerDeath();
        }
    }

    void HandlePlayerDeath()
    {
        playerIsDead = true;
        deathTimestampText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

        // Get the in-game time in seconds since the game started
        float gameTime = timer.GetTime();
        
        // Format the time as "MM:SS"
        string timestamp = gameTime.ToString("F2") + "s";

        // Display the timestamp in the UI
        deathTimestampText.text = "Player died at: " + timestamp;

        // Optionally, log the timestamp to the console
        Debug.Log("Player died at: " + timestamp);
    }


    // This method checks if the player is dead
    // Replace this with your actual logic to check for player death
    bool IsPlayerDead()
    {
        // Example condition to simulate player death (replace with actual logic)
        return playerController.health <= 0;
    }

    // Optionally, you can call this method externally to trigger death
    public void TriggerPlayerDeath()
    {
        HandlePlayerDeath();
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
