using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    public float gunSightDistance;
    public int damage;

    Stats stats;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    RaycastHit hit;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;

    //Atacking
    public float timeBetweenAtacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    private void Awake()
    {

        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        stats = GetComponentInChildren<Stats>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        //DistanceToPlayer();

        if (stats.state == E_States.Stunned)
        {
            Stunned();
        }
        else
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            else
                if (playerInSightRange && playerInAttackRange)
            {
                AttackPlayer();
            }
        }

    }

    void Patroling()
    {

        if (walkPointSet == false)
        {
            SearchWalkPoint();
        }

        if (walkPointSet == true)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }
    void SearchWalkPoint()
    {

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;

        }
        else
        {
            Debug.LogWarning("Mo¿liwe ¿e nie ustawiono pod³ogi jako layer Ground");
        }

        //if(Physics.Raycast(transform.position, walkPoint, out hit))
        //{
        //    if(hit.collider != null)
        //    {
        //        Debug.Log("Terrain");
        //    }
        //}
    }
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void Stunned()
    {
        agent.SetDestination(transform.position);
    }
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            ///Attack code here
            if (player.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.TakeDamage(damage, 0);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAtacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, player.position - transform.position );

    }

    void DistanceToPlayer()
    {
        //Debug.Log(player.position);

        if (Physics.Raycast(transform.position, player.position, gunSightDistance, whatIsGround))
        {

            //jeœli œciana
            //Debug.Log("Wall in between");
        }
        else
        {
            if(Vector3.Distance(transform.position,  player.position) <= gunSightDistance)
            {
                //Check rotation
            }
        }
    }
}
