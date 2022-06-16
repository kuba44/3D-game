using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColider : MonoBehaviour
{
    Rigidbody rb;
   private float projectileSpeed=100f;

    private Vector3 playerDirection;
    void Start()
    {
        playerDirection = FindObjectOfType<PlayerController>().transform.position - transform.position;
        playerDirection.Normalize();
    }
    void Update()
    {
        
        transform.position += playerDirection * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyConpoment))
        {
            enemyConpoment.TakeDamage(1);
        }
        Destroy(gameObject);

    }
}
