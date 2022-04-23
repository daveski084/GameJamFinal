using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class GuardController : MonoBehaviour
{
    public static bool chase;

    public Animator animator;
    
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 3;
    public float speedRun = 4;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform [] wayPoints;
    int currentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 playerPosition;

    float waitTime;
    float m_timeToRoate;
    bool m_playerInRange;
    bool playerNear;
    bool isPatrol;
    bool caughtPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
        chase = false; 
        playerPosition = Vector3.zero;
        isPatrol = true;
        caughtPlayer = false;
        m_playerInRange = false;
        waitTime = startWaitTime;
        m_timeToRoate = timeToRotate;

        currentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        EnvironmentView();

        if(!isPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
            chase = false;
        }
    }


    void CaughtPlayer()
    {
        caughtPlayer = true;
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }


    void Chasing()
    {
        Debug.Log("Should be chasing");
        chase = true;
       
        playerNear = false;
        playerLastPosition = Vector3.zero;

        if(!caughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(playerPosition); 
        }
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (waitTime <= 0 && !caughtPlayer && Vector3.Distance(transform.position, 
                GameObject.FindGameObjectWithTag("Player").transform.position) >= 6)
            {
                isPatrol = true;
                playerNear = false;
                Move(speedWalk);
                m_timeToRoate = timeToRotate;
                waitTime = startWaitTime;
                navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position); 
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    waitTime -= Time.deltaTime; 
                }
            }
        }
    }

    void Patroling()
    {

        if(playerNear)
        {
            if(m_timeToRoate <= 0)
            {
                Move(speedWalk);
                LookingForPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_timeToRoate -= Time.deltaTime;
            }
        }
        else
        {
            playerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if(waitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    waitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % wayPoints.Length;
        navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
    }

    void LookingForPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player); 
        if(Vector3.Distance(transform.position, player) <= 0.3)
        {
            if(waitTime <= 0)
            {
                playerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(wayPoints[currentWaypointIndex].position);
                waitTime = startWaitTime;
                m_timeToRoate = timeToRotate;

            }
            else
            {
                Stop();
                waitTime = Time.deltaTime;
            }
        }
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
       
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if(!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_playerInRange = true;
                    isPatrol = false; 
                }
                else
                {
                    m_playerInRange = false;
                }
            }
            if(Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_playerInRange = false;
            }

            if (m_playerInRange)
            {
                playerPosition = player.transform.position;
            }
        }
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("isPlayer", true); 
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(FlipBool()); 
        }
    }

    public IEnumerator FlipBool()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("isPlayer", false);
    }

}
