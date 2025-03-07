using UnityEngine;

public class FSM : MonoBehaviour
{
    public GameObject player;
    private ChaseState chaseState;
    private WanderState wanderState;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");

        chaseState = GetComponent<ChaseState>();
        wanderState = GetComponent<WanderState>();

        SetState(wanderState);
    }

    public void SetState(MonoBehaviour newState)
    {
        if (newState.enabled) return; 

        chaseState.enabled = false;
        wanderState.enabled = false;

        newState.enabled = true;
    }
}
