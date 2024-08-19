using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    
    PlayerController playerController;
    public float health = 1f;
    public float healthExponent = 1f;
    // Start is called before the first frame update// Time interval in seconds for scaling up the GameObject
    public float interval = 2.0f;

    // Scale increment
    public Vector3 scaleIncrement = new Vector3(0.1f, 0.1f, 0.1f);
    public GameObject drop;
    private float elapsedTime = 0f;

    // Internal variable to keep track of the next scaling time
    private float nextScaleTime;

    void Start()
    {
        elapsedTime = 0f;
        nextScaleTime = interval;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        ScaleObjectOverTime();
    }

    void ScaleObjectOverTime()
    {
        // Check if the current time has reached the next scaling time
        if (elapsedTime >= nextScaleTime)
        {
            // Scale the GameObject up by the increment
            transform.localScale += scaleIncrement;

            // Set the next scale time
            nextScaleTime += interval;

            StatIncrease();
        }
    }


    //Increase health based on scale
    private void StatIncrease()
    {
        health += transform.localScale.magnitude * healthExponent;

    }
        
    //Enemy Take Damage
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            EnemyHit(other.gameObject, PlayerController.GetDamage());
        }
    }

    public void EnemyHit(GameObject other, float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        //Drop exp

        Instantiate(drop, gameObject.transform.position, Quaternion.identity);

        GameObject.Destroy(gameObject);
    }




}
