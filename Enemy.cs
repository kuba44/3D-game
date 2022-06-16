using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//SKRYPT OD PRZYJMOWANIA DMG, SMIERCI ENEMY 
//KRZYSIEK

public class Enemy : MonoBehaviour
{
    float health;
    public float Maxhealth;

    // Start is called before the first frame update
    private void Start()
    {
        health = Maxhealth;
    }
    
    public void TakeDamage(float damageAmount)
    {
        Debug.Log(health);
        health-=damageAmount;

        if (health <= 0)
        { 
            Destroy(gameObject);
        }
        Debug.Log(health);
    }
}
