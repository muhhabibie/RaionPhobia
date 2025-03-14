using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : MonoBehaviour
{
    private FSM fsm;
    private NavMeshAgent agent;
    private float wanderRadius = 50f; 
    private bool isWaiting = false;
    private Vector3 lastDestination;

    private void Awake()
    {
        fsm = GetComponent<FSM>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        agent.isStopped = false;
        SetRandomDestination();
    }
    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (fsm.player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, fsm.player.transform.position);

        if (distanceToPlayer > 15f) return false; // Jarak pandang

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > 60f / 2) return false; // Sudut penglihatan

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, 15f))
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    private void Update()
    {
        if (isWaiting) return;

        if (CanSeePlayer())
        {
            Debug.Log(gameObject.name + " Kejar pemain");
            fsm.SetState(GetComponent<ChaseState>());
            return;
        }

        if (!agent.hasPath || agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitBeforeNextMove());
        }
        else
        {
            RotateTowards(agent.steeringTarget);
        }
    }


  

    private IEnumerator WaitBeforeNextMove()
    {
        isWaiting = true;
        yield return new WaitForSeconds(Random.Range(1f, 3f)); 
        isWaiting = false;
        SetRandomDestination();
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection.y = 0;
        Vector3 wanderTarget = transform.position + randomDirection;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(wanderTarget, out hit, wanderRadius, NavMesh.AllAreas))
        {
            lastDestination = hit.position;
            agent.SetDestination(hit.position);
        }
        else
        {
            Debug.LogWarning("Debug nggk nemu path");
            SetRandomDestination(); 
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * 300f);
        }
    }

}

