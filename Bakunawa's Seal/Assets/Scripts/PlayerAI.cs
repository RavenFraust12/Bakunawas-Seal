using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private CharStats charStats;

    public Transform target; // The current target (closest unit)
    public float attackRange = 2f; // Set your attack range
    public float detectionRange = 5f;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        charStats = GetComponent<CharStats>();
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        if (target != null)
        {
            // Rotate towards the target
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            navAgent.SetDestination(target.transform.position);

            // Check if within attack range
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                // Attack logic here, e.g., trigger attack animation
                Debug.Log("Attacking " + target.name);
            }
        }
        else
        {
            navAgent.ResetPath();
        }
    }

    void FindClosestEnemy()
    {
        if (charStats.isDead == false)
        {
            GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");

            float closestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;

            foreach (GameObject enemy in enemyUnits)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < closestDistance && distanceToEnemy <= detectionRange)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }

            }

            if (closestEnemy != null)
            {
                target = closestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
        else
        {
            target = null;
        }
        
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
    }
}
