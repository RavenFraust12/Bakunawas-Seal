using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private EnemyStats enemyMovespeed;

    public Transform target; // The current target (closest unit)
    public float attackRange = 2f; // Set your attack range

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyMovespeed = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestPlayer();
        if (target != null)
        {
            // Rotate towards the target
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            navAgent.speed = enemyMovespeed.movespeed;
            navAgent.SetDestination(target.transform.position);

            // Check if within attack range
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                // Attack logic here, e.g., trigger attack animation
                Debug.Log("Attacking " + target.name);
            }
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        float closestDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance && charStats.currentHealth > 0)
            {
                closestDistance = distanceToPlayer;
                closestPlayer = player;
            }
        }
        if (closestPlayer != null)
        {
            target = closestPlayer.transform;
        }
    }
}
