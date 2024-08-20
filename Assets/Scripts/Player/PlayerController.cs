using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public PlayerDamageFlash[] childObjects;
    public float health = 5f;
    public float baseDamage = 1f;
    public static float damage = 1f;
    public float sizeExponent = 1f;
    public Vector3 shrinkIncrement = new Vector3(1f, 1f, 1f);
    public float shrinkInterval = 5f;
    public float maxExpIncrease = 5f;
    public float currentLevel = 1f;

    private PlayerDamageFlash playerDamageFlash;
    private float currentExp = 0f;
    private float maxExp = 0f;
    private float nextShrinkTime = 0f;

    public static float GetDamage() {
        return damage;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        maxExp = maxExpIncrease;
        nextShrinkTime = shrinkInterval;
        
        damage = baseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        ShrinkOverTime();
        SetDamage();
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

        IncreaseScale();

        // Increase the max experience required for the next level
        maxExp += maxExpIncrease;
        enemySpawner.ScaleSpawnAmount();

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
            Debug.Log("Damage = " + GetDamage());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            for(int i = 0; i < childObjects.Length; i++) {
                childObjects[i].StartFlash();
            }
            
            health--;
        }
    }

    private void IncreaseScale()
    {
        // Calculate the new scale using a logarithmic function
        float scaleMultiplier = Mathf.Log(currentLevel + sizeExponent, 2); // base 2 log
        Vector3 newScale = Vector3.one * scaleMultiplier;

        transform.localScale = newScale;
        Debug.Log("Damage = " + GetDamage());
    }
    

    private void SetDamage()
    {
        baseDamage = currentLevel;
        damage = baseDamage + (transform.localScale.magnitude * 2f);
    }

}
