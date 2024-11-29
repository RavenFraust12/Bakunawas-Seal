using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private CharStats charStats;
    public AnimationManager animationManager; // Reference to the AnimationManager

    public Transform target; // The current target (closest unit)
    public float attackRange = 2f; // Set your attack range
    public float detectionRange = 5f;
    public bool canAttack; // Can the unit attack
    public int charSelection; // 1 = Apolaki, 2 = Mayari, 3 = Dumangan, 4 = Dumakulem

    [Header("Range Attacks")]
    public bool isRanged; // Is the unit ranged attack
    public GameObject pointOfFire;
    public GameObject rangeProjectile;
    public GameObject bulletHolder;

    private CameraScript cameraScript;
    public bool isPlayerControlled;
    public PlayerMovement playerMovement;
    private bool hasPlayedDeath = false; // New flag to track death animation

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        charStats = GetComponent<CharStats>();
        bulletHolder = GameObject.Find("BulletHolder");

        cameraScript = FindObjectOfType<CameraScript>();

        animationManager = GetComponentInChildren<AnimationManager>(); // Get the AnimationManager component

        GameObject movementManager = GameObject.Find("Movement Manager"); // Ensure this matches the exact name
        playerMovement = movementManager.GetComponent<PlayerMovement>();
    }
    void Update()
    {     
        if (charStats.isDead)
        {
            if (!hasPlayedDeath) // Check if death animation has not been played yet
            {
                animationManager.PlayDeath(); // Trigger death animation once
                hasPlayedDeath = true; // Set flag to prevent re-triggering
            }

            isPlayerControlled = false;
            return;
        }
        else
        {
            hasPlayedDeath = false; // Reset flag if character is not dead
        }

        FindClosestEnemy();

        if (target != null)
        {
            PlayerActionAI();
        }
        else
        {
            navAgent.ResetPath();
            if (!playerMovement.isMoving) animationManager.PlayIdle();
            navAgent.velocity = Vector3.zero;
        }
    }

    void PlayerActionAI()
    {
        if (Vector3.Distance(transform.position, target.position) <= detectionRange)
        {
            // Rotate towards the target only if within detection range
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            if(!playerMovement.isMoving) transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Stop AI movement when the player is controlling the character
            if (isPlayerControlled)
            {
                navAgent.ResetPath();

                // If in attack range, trigger attack animation
                if (Vector3.Distance(transform.position, target.position) <= attackRange && !playerMovement.isMoving)
                {
                    SkillDelay();                  
                }
                return;
            }
            else
            {
                navAgent.SetDestination(target.position);
                animationManager.PlayWalk();

                // If in attack range, stop and attack
                if (Vector3.Distance(transform.position, target.position) <= attackRange)
                {
                    navAgent.ResetPath();
                    navAgent.velocity = Vector3.zero;
                    if(canAttack) SkillDelay();
                    else animationManager.PlayIdle();
                }
            }
        }
    }


    void OnDrawGizmosSelected()
    {
        // Set Gizmo color for attack range
        Gizmos.color = Color.red;
        // Draw a sphere for attack range
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Set Gizmo color for detection range
        Gizmos.color = Color.yellow;
        // Draw a sphere for detection range
        Gizmos.DrawWireSphere(transform.position, detectionRange);
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

    IEnumerator AttackDelay() // Play attack animation during attack delay
    {
        if (!isRanged)
        {
            if (canAttack == true)
            {
                animationManager.PlayAttack(); // Play melee attack animation
                canAttack = false;
                yield return new WaitForSeconds(charStats.currentAttackspeed);
                canAttack = true;
            }
        }
        else if (isRanged)
        {
            if (canAttack == true)
            {
                canAttack = false;
                animationManager.PlayAttack(); // Play ranged attack animation
                yield return new WaitForSeconds(charStats.currentAttackspeed);
                canAttack = true;
            }
        }
    }

    public void ReleaseProjectile()
    {
        GameObject projectile = Instantiate(rangeProjectile, pointOfFire.transform.position, pointOfFire.transform.rotation, bulletHolder.transform);
        Damage projectileScript = projectile.GetComponent<Damage>();
        projectileScript.charStats = GetComponent<CharStats>();
    }

    void SkillDelay()
    {
        switch (charSelection)
        {
            case 1:
                ApolakiSkills apolaki = GetComponent<ApolakiSkills>();
                apolaki.Skills();
                if(apolaki.canSkill) animationManager.PlaySkill();
                else StartCoroutine(AttackDelay());
                break;
            case 2:
                MayariSkills mayari = GetComponent<MayariSkills>();
                mayari.Skills();
                if (mayari.canSkill) animationManager.PlaySkill();
                else StartCoroutine(AttackDelay());
                break;
            case 3:
                DumanganSkills dumangan = GetComponent<DumanganSkills>();
                dumangan.Skills();
                if (dumangan.canSkill) animationManager.PlaySkill();
                else StartCoroutine(AttackDelay());
                break;
            case 4:
                DumakulemSkills dumakulem = GetComponent<DumakulemSkills>();
                dumakulem.Skills();
                if (dumakulem.canSkill) animationManager.PlaySkill();
                else StartCoroutine(AttackDelay());
                break;
            default:
                break;
        }
        
    }
}
