using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public float gunSightDistance;
    public int damage;
    public enemyAnimController animController;
    Stats stats;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    bool animationAttack = false;
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
        animController.speed = agent.velocity.magnitude;

        if (stats.state == E_States.Stunned)
        {
            Stunned();
        }
        else
        {
            if (!playerInSightRange && !playerInAttackRange)
            {
               // Debug.Log("Patrol");
                Patroling();
            }
            else if (playerInSightRange && !playerInAttackRange)
            {
                //Debug.Log("Chase");
                ChasePlayer();
            }
            else
                if (playerInSightRange && playerInAttackRange)
            {
               
                
                AttackPlayer();
                //Debug.Log("Attack");
               
                
            }
        }

    }

    public void AnimationAttack()
    {
        Debug.Log("DoDmg");
        if (playerInAttackRange)
        {
            player.GetComponent<Stats>().TakeDamage(damage, 0);
        }
        else
        {
            Debug.Log("Damnn! I missed");
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
            Debug.LogWarning("Możliwe że nie ustawiono podłogi jako layer Ground");
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
            animController.Attack();
            ///Attack code here
            

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
