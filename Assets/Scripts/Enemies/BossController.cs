using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    //BossRanges
    [SerializeField] float shootingRange;
    [SerializeField] float playerChaseRange;
    [SerializeField] float playerDetectionRange;
    [SerializeField] float stopChaseRange;

    //BossMovement
    [SerializeField] float bossSpeed;
    private Transform playerToChase;
    private Rigidbody bossRigidbody;
    private Vector3 directionToMove;
    private bool isChasing = false;

    //BoosShooting
    [SerializeField] float timeBetweenShots;
    private bool readyToShoot;

    //DamageBoss
    [SerializeField] int bossHealth;

    //objects
    [SerializeField] GameObject bossProjectile;
    [SerializeField] Transform firePosition;


    void Start()
    {
        bossRigidbody = GetComponent<Rigidbody>();

        playerToChase = FindObjectOfType<PlayerController>().transform;

        readyToShoot = true;
    }


    void Update()
    {
        BossMovement();

        LookAtPlayer();

        BossShooting();
    }

    private void BossShooting()
    {
        if (readyToShoot && Vector3.Distance(playerToChase.transform.position, transform.position) < shootingRange)
        {
            if (!playerToChase.gameObject.activeInHierarchy) return;

            readyToShoot = false;
            StartCoroutine(FireEnemyProjectile());
        }
    }

    private void LookAtPlayer()
    {
        transform.LookAt(playerToChase);
    }

    private void BossMovement()
    {
        if (Vector3.Distance(transform.position, playerToChase.position) < playerDetectionRange)
        {
            isChasing = true;
        }

        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange && Vector3.Distance(transform.position, playerToChase.position) > stopChaseRange && isChasing)
        {
            directionToMove = playerToChase.position - transform.position;
        }
        else
        {
            directionToMove = Vector3.zero;
            isChasing = false;
        }

        directionToMove.Normalize();
        bossRigidbody.velocity = directionToMove * bossSpeed;
    }

    IEnumerator FireEnemyProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        Instantiate(bossProjectile, firePosition.position, firePosition.rotation);
        readyToShoot = true;
    }

    public void DamageEnemy(int damageTaken)
    {
        bossHealth -= damageTaken;

        if (bossHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerChaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, stopChaseRange);
    }

}