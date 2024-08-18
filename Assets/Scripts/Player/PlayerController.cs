using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 5f;
    public Vector3 shrinkIncrement = new Vector3(1f, 1f, 1f);
    public float expLoss = 1f;
    public float shrinkInterval = 5f;
    public float maxExpIncrease = 5f;
    public float currentLevel = 1f;
    private float currentExp = 0f;
    private float maxExp = 0f;
    private float nextShrinkTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        maxExp = maxExpIncrease;
        nextShrinkTime = shrinkInterval;
    }

    // Update is called once per frame
    void Update()
    {
        ShrinkOverTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exp"))
        {
            currentExp++;
            if (currentExp >= maxExp)
            {
                LevelUp();
            }
        }
        Destroy(other.gameObject);
    }


    private void LevelUp()
    {
        currentLevel++;

        // Calculate the new scale using a logarithmic function
        float scaleMultiplier = Mathf.Log(currentLevel + 1, 2); // base 2 log
        Vector3 newScale = Vector3.one * scaleMultiplier;

        transform.localScale = newScale;

        // Increase the max experience required for the next level
        maxExp += maxExpIncrease;
    }

    private void ShrinkOverTime()
    {
        Vector3 minScale = Vector3.one;
        if (Time.time >= nextShrinkTime)
        {
            if (transform.localScale.sqrMagnitude > minScale.sqrMagnitude)
            {
                // Scale the GameObject up by the increment
                transform.localScale -= shrinkIncrement;
            }

            if (transform.localScale.sqrMagnitude < minScale.sqrMagnitude)
            {
                transform.localScale = minScale;
            }

            // Set the next scale time
            nextShrinkTime += shrinkInterval;
        }
    }
}
