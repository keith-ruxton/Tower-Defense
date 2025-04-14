using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;
    private NavMeshAgent agent;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();

        target = Waypoints.points[0];
        agent.SetDestination(target.position);
        agent.speed = enemy.speed;
    }

    void Update()
    {
        agent.speed = enemy.speed;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                GetNextWaypoint();
            }
        }
    }

    void GetNextWaypoint()
    {
        waypointIndex++;

        if (waypointIndex >= Waypoints.points.Length)
        {
            EndPath();
            return;
        }

        target = Waypoints.points[waypointIndex];
        agent.SetDestination(target.position);
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}

