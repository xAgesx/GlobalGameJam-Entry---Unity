using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonsterAI : MonoBehaviour {
    public NavMeshAgent agent;
    public Transform player;
    public Transform[] waypoints;
    public float detectionRange = 10f;
    public float updateInterval = 0.2f;

    private int currentWaypointIndex = 0;
    private bool isChasing = false;
    public AbilityController abilityController;

    void Start() {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if(waypoints.Length > 0) {
            agent.SetDestination(waypoints[0].position);
        }
        
        StartCoroutine(UpdatePathLoop());
    }

    void Update() {
        if (agent.velocity.sqrMagnitude > 0.1f) {
            Quaternion lookRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }

        if (!isChasing && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f) {
            GoToNextWaypoint();
        }
    }

    IEnumerator UpdatePathLoop() {
        while (true) {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            bool playerIsVisible = !abilityController.isSpiritActive; 

            if (playerIsVisible && distanceToPlayer < detectionRange) {
                if (!isChasing) Debug.Log("<color=red>Monster: I SEE YOU!</color>");
                isChasing = true;
                agent.SetDestination(player.position);
            } else {
                if (isChasing) Debug.Log("<color=yellow>Monster: Lost the trail. Back to patrol.</color>");
                isChasing = false;
                if (!agent.hasPath) {
                    agent.SetDestination(waypoints[currentWaypointIndex].position);
                }
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }

    void GoToNextWaypoint() {
        if (waypoints.Length == 0) return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    // Visual Debugging in the Scene View
    private void OnDrawGizmos() {
        if (agent != null && agent.hasPath) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, agent.destination);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}