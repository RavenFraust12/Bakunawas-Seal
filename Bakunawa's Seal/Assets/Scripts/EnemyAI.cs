using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private EnemyStats enemyStats;

    public Transform target; // The current target (closest unit)
    public bool canAttack; //Can the unit attack
    public GameObject weapon; //Collider for damage

    [Header("Range Attacks")]
    public float attackRange = 2f; // Set your attack range
    public bool isRanged; //Is the unit ranged attack
    public GameObject pointOfFire; //Where the bullet will spawn
    public GameObject rangeProjectile; //The bullet to spawn

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
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
            navAgent.speed = enemyStats.movespeed;
            navAgent.SetDestination(target.transform.position);

            // Check if within attack range
            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                // Attack logic here, e.g., trigger attack animation
                navAgent.ResetPath();
                StartCoroutine(AttackDelay());
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

    IEnumerator AttackDelay() //Change this later and turn it into animation
    {
        if (!isRanged)
        {
            if (canAttack == true)
            {
                weapon.SetActive(true);
                canAttack = false;
                yield return new WaitForSeconds(0.1f);
                weapon.SetActive(false);
                yield return new WaitForSeconds(enemyStats.attackspeed);
                canAttack = true;
            }
        }
        else if (isRanged)
        {
            if (canAttack == true)
            {
                GameObject projectile = Instantiate(rangeProjectile, pointOfFire.transform.position, pointOfFire.transform.rotation, this.transform);

                canAttack = false;
                yield return new WaitForSeconds(enemyStats.attackspeed);
                canAttack = true;
            }
        }
    }
}
