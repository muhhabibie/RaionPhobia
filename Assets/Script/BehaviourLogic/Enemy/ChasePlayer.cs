using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonoBehaviour
{
    private FSM fsm;
    private NavMeshAgent agent;
    private GameObject player;
    private float visionRange = 15f; // Jarak pandang
    private float visionAngle = 60f; // Sudut penglihatan
    private float lostPlayerTime = 3f; 
    private float lostPlayerTimer;

    private void Awake()
    {
        fsm = GetComponent<FSM>();
        agent = GetComponent<NavMeshAgent>();
        player = fsm.player;
    }

    private void OnEnable()
    {
        agent.isStopped = false;
        lostPlayerTimer = lostPlayerTime;

        if (!CanSeePlayer())
        {
            Debug.Log(gameObject.name + " anomali tidak terlihat.");
            fsm.SetState(GetComponent<WanderState>());
        }
        else
        {
            Debug.Log(gameObject.name + " mulai mengejar pemain!");
        }
    }



    private void Update()
    {
        if (player == null) return;

        if (CanSeePlayer())
        {
            agent.SetDestination(player.transform.position);
            RotateTowards(agent.steeringTarget);
            lostPlayerTimer = lostPlayerTime; 
        }
        else
        {
            lostPlayerTimer -= Time.deltaTime;
            if (lostPlayerTimer <= 0)
            {
                fsm.SetState(GetComponent<WanderState>()); 
            }
        }
    }


    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > visionRange) return false;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle / 2) return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionRange))
        {
            if (hit.collider.CompareTag("Player")) return true;
        }

        return false;
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
