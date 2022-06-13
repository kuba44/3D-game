using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerV2 : MonoBehaviour
{
    NavMeshAgent meshAgent;

    [SerializeField] LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] Vector3 destinationPoint;
    bool isDestinationSet;
    [SerializeField] float destinationRange;

    [SerializeField] Transform player;

    [SerializeField] float chaseRange;
    [SerializeField] bool isPlayerInChaseRange;

    [SerializeField] float attackRange;
    private bool isPlayerInAttackRange;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;

    [SerializeField] float timeBetweenShots;
    private bool readyToShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);
        isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if( !isPlayerInChaseRange && !isPlayerInAttackRange )
        {
            Guarding();
        }
        if( isPlayerInChaseRange && !isPlayerInAttackRange )
        {
            ChasingPlayer();
        }
        if( isPlayerInAttackRange )
        {
            AttackingPlayer();
        }
    }

    private void AttackingPlayer()
    {
        meshAgent.SetDestination(transform.position);
        transform.LookAt( player );

        if( readyToShoot)
            StartCoroutine( FireEnemyProjectile() );
    }

    IEnumerator FireEnemyProjectile()
    {
        readyToShoot = false;

        Instantiate(projectile, firePoint.position, transform.localRotation);
        
        yield return new WaitForSeconds(timeBetweenShots);

        readyToShoot = true;
    }

    private void ChasingPlayer()
    {
        meshAgent.SetDestination( player.position );
    }

    private void Guarding()
    {
        if( !isDestinationSet )
        {
            SearchForDestination();
        }
        else
        {
            meshAgent.SetDestination( destinationPoint );
        }

        Vector3 distanceToDestination = transform.position - destinationPoint;

        if( distanceToDestination.magnitude < 1f )
        {
            isDestinationSet = false;
        }
    }

    private void SearchForDestination()
    {
        float randomPositionZ = Random.Range( -destinationRange, destinationRange );
        float randomPositionX = Random.Range( -destinationRange, destinationRange );

        destinationPoint = new Vector3( transform.position.x + randomPositionX, transform.position.y, transform.position.z + randomPositionZ );

        if( Physics.Raycast(destinationPoint, -transform.up, 2f, whatIsGround) )
        {
            isDestinationSet = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere( transform.position, chaseRange );
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere( transform.position, attackRange );
    }

}
