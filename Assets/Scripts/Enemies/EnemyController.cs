using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //EnemyRanges
    [SerializeField] float shootingRange;
    [SerializeField] float playerChaseRange;
    [SerializeField] float playerDetectionRange;

    //EnemyMovement
    [SerializeField] float enemySpeed;
    private Transform playerToChase;
    private Rigidbody enemyRigidbody;
    private Vector3 directionToMove;
    private bool isChasing = false;

    //EnemyAnimations
    private Animator enemyAnimator;

    //EnemyShooting
    [SerializeField] float timeBetweenShots;
    private bool readyToShoot;

    //DamageEnemy
    [SerializeField] int enemyHealth;

    //objects
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] Transform firePosition;


    void Start()
    {
        //enemyAnimator = GetComponent<Animator>();

        enemyRigidbody = GetComponent<Rigidbody>();

        playerToChase = FindObjectOfType<PlayerController>().transform;

        readyToShoot = true;
    }


    void Update()
    {
        EnemyMovement();

        //EnemyAnimations();

        EnemyTurnAround();

        EnemyShooting();
    }

    private void EnemyShooting()
    {
        if (readyToShoot && Vector3.Distance(playerToChase.transform.position, transform.position) < shootingRange)
        {
            if (!playerToChase.gameObject.activeInHierarchy) return;

            readyToShoot = false;
            StartCoroutine(FireEnemyProjectile());
        }
    }

    private void EnemyTurnAround()
    {
        if (playerToChase.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    private void EnemyAnimations()
    {
        if (directionToMove != Vector3.zero)
        {
            enemyAnimator.SetBool("isWalking", true);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
        }
    }

    private void EnemyMovement()
    {
        if (Vector3.Distance(transform.position, playerToChase.position) < playerDetectionRange)
        {
            isChasing = true;
        }

        if (Vector3.Distance(transform.position, playerToChase.position) < playerChaseRange && isChasing)
        {
            directionToMove = playerToChase.position - transform.position;
        }
        else
        {
            directionToMove = Vector3.zero;
            isChasing = false;
        }

        directionToMove.Normalize();
        enemyRigidbody.velocity = directionToMove * enemySpeed;
    }

    IEnumerator FireEnemyProjectile()
    {
        yield return new WaitForSeconds(timeBetweenShots);

        Instantiate(enemyProjectile, firePosition.position, firePosition.rotation);
        readyToShoot = true;
    }

    public void DamageEnemy(int damageTaken)
    {
        enemyHealth -= damageTaken;

        if (enemyHealth <= 0)
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
    }

}