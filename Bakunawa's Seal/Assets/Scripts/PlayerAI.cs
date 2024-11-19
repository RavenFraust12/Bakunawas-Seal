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
    public GameObject weapon; // Collider for damage
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
        /*if (movementManager != null)
        {
            playerMovement = movementManager.GetComponent<PlayerMovement>();
            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement script not found on Movement Manager.");
            }
        }*/
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
            animationManager.PlayIdle();
            navAgent.velocity = Vector3.zero;
            Debug.Log("No enemy detected; idle animation triggered");

        }
    }

    void PlayerActionAI()
    {
        if (Vector3.Distance(transform.position, target.position) <= detectionRange)
        {
            // Rotate towards the target only if within detection range
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Stop AI movement when the player is controlling the character
            if (isPlayerControlled)
            {
                navAgent.ResetPath();
                //navAgent.velocity = Vector3.zero;

                // If in attack range, trigger attack animation
                if (Vector3.Distance(transform.position, target.position) <= attackRange)
                {
                    StartCoroutine(AttackDelay());
                    SkillDelay();
                    Debug.Log("Attacking " + target.name);
                }
                return;
            }
            else
            {
                navAgent.SetDestination(target.position);
                animationManager.PlayWalk();
                Debug.Log("Walking animation triggered on AI");

                // If in attack range, stop and attack
                if (Vector3.Distance(transform.position, target.position) <= attackRange)
                {
                    navAgent.ResetPath();
                    navAgent.velocity = Vector3.zero;
                    StartCoroutine(AttackDelay());
                    SkillDelay();
                    Debug.Log("Attacking " + target.name);
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
                weapon.SetActive(true);
                canAttack = false;

                //yield return new WaitForSeconds(0.1f);
                float attackAnimDuration = animationManager.animator.GetCurrentAnimatorStateInfo(0).length / animationManager.animator.speed; // Get attack animation length, adjusting for speed
                yield return new WaitForSeconds(attackAnimDuration);

                animationManager.PlayIdle();
                weapon.SetActive(false);
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
                GameObject projectile = Instantiate(rangeProjectile, pointOfFire.transform.position, pointOfFire.transform.rotation, bulletHolder.transform);
                //projectile.transform.rotation *= Quaternion.Euler(0, 180, 0);
                Damage projectileScript = projectile.GetComponent<Damage>();
                projectileScript.charStats = GetComponent<CharStats>();
                animationManager.PlayIdle();
                yield return new WaitForSeconds(charStats.currentAttackspeed);
                canAttack = true;
            }
        }
    }

    void SkillDelay()
    {
        // Play skill animation based on character selection
        animationManager.PlaySkill();
        switch (charSelection)
        {
            case 1:
                ApolakiSkills apolaki = GetComponent<ApolakiSkills>();
                apolaki.Skills();
                break;
            case 2:
                MayariSkills mayari = GetComponent<MayariSkills>();
                mayari.Skills();
                break;
            case 3:
                DumanganSkills dumangan = GetComponent<DumanganSkills>();
                dumangan.Skills();
                break;
            case 4:
                DumakulemSkills dumakulem = GetComponent<DumakulemSkills>();
                dumakulem.Skills();
                break;
            default:
                Debug.Log("No Skills Detected");
                break;
        }
    }
}
//original script
//public class PlayerAI : MonoBehaviour
//{
//    private NavMeshAgent navAgent;
//    private CharStats charStats;

//    public Transform target; // The current target (closest unit)
//    public float attackRange = 2f; // Set your attack range
//    public float detectionRange = 5f;
//    public bool canAttack; //Can the unit attack
//    public GameObject weapon; //Collider for damage
//    public int charSelection;//1 = Apolaki, 2 = Mayari, 3 = Dumangan, 4 = Dumakulem

//    [Header("Range Attacks")]
//    public bool isRanged; //Is the unit ranged attack
//    public GameObject pointOfFire;
//    public GameObject rangeProjectile;
//    public GameObject bulletHolder;

//    private CameraScript cameraScript;
//    public bool isPlayerControlled;

//    // Start is called before the first frame update
//    void Start()
//    {
//        navAgent = GetComponent<NavMeshAgent>();
//        charStats = GetComponent<CharStats>();
//        bulletHolder = GameObject.Find("BulletHolder");

//        cameraScript = FindObjectOfType<CameraScript>();

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        FindClosestEnemy();
//        if(charStats.isDead) isPlayerControlled = false;
//        if(target != null)
//        {
//            PlayerActionAI();
//        }  
//    }

//    void PlayerActionAI()
//    {
//        // Rotate towards the target
//        Vector3 direction = (target.position - transform.position).normalized;
//        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
//        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

//        // Stop AI movement when the player is controlling the character
//        if (isPlayerControlled)
//        {
//            navAgent.ResetPath(); // Stop AI movement
//                                  // Check if within attack range
//            if (Vector3.Distance(transform.position, target.position) <= attackRange)
//            {
//                // Attack logic here, e.g., trigger attack animation
//                StartCoroutine(AttackDelay());
//                SkillDelay();
//                Debug.Log("Attacking " + target.name);
//            }
//            return; // Skip further AI movement logic
//        }
//        else if (!isPlayerControlled)
//        {
//            navAgent.SetDestination(target.transform.position);

//            // Check if within attack range
//            if (Vector3.Distance(transform.position, target.position) <= attackRange)
//            {
//                // Attack logic here, e.g., trigger attack animation
//                navAgent.ResetPath();
//                StartCoroutine(AttackDelay());
//                SkillDelay();
//                Debug.Log("Attacking " + target.name);
//            }
//        }
//        else
//        {
//            navAgent.ResetPath();
//            Debug.Log("No Enemy Detected");
//        }
//    }
//    void FindClosestEnemy()
//    {
//        if (charStats.isDead == false)
//        {
//            GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");

//            float closestDistance = Mathf.Infinity;
//            GameObject closestEnemy = null;

//            foreach (GameObject enemy in enemyUnits)
//            {
//                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
//                if (distanceToEnemy < closestDistance && distanceToEnemy <= detectionRange)
//                {
//                    closestDistance = distanceToEnemy;
//                    closestEnemy = enemy;
//                }

//            }

//            if (closestEnemy != null)
//            {
//                target = closestEnemy.transform;
//            }
//            else
//            {
//                target = null;
//            }
//        }
//        else
//        {
//            target = null;
//        }

//    }

//    IEnumerator AttackDelay() //Change this later and turn it into animation
//    {
//        if (!isRanged)
//        {
//            if (canAttack == true)
//            {
//                weapon.SetActive(true);
//                canAttack = false;
//                yield return new WaitForSeconds(0.1f);
//                weapon.SetActive(false);
//                yield return new WaitForSeconds(charStats.currentAttackspeed);
//                canAttack = true;
//            }
//        }
//        else if (isRanged)
//        {
//            if (canAttack == true)
//            {
//                canAttack = false;

//                GameObject projectile = Instantiate(rangeProjectile,pointOfFire.transform.position, pointOfFire.transform.rotation, bulletHolder.transform);
//                Damage projectileScript = projectile.GetComponent<Damage>();
//                projectileScript.charStats = GetComponent<CharStats>();

//                yield return new WaitForSeconds(charStats.currentAttackspeed);
//                canAttack = true;
//            }
//        }
//    }

//    void SkillDelay()
//    {
//        switch(charSelection)
//        {
//            case 1:
//                ApolakiSkills apolaki = GetComponent<ApolakiSkills>();
//                apolaki.Skills();
//                break;
//            case 2:
//                MayariSkills mayari = GetComponent<MayariSkills>();
//                mayari.Skills();
//                break;
//            case 3:
//                DumanganSkills dumangan = GetComponent<DumanganSkills>();
//                dumangan.Skills();
//                break;
//            case 4:
//                DumakulemSkills dumakulem = GetComponent<DumakulemSkills>();
//                dumakulem.Skills();
//                break;
//            default:
//                Debug.Log("No Skills Detected");
//                break;
//        }
//    }
//}
