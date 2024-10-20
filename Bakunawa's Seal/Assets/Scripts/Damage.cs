using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Damage : MonoBehaviour
{
    public string target;

    [Header("Scripts")]
    public EnemyStats enemyStats;
    public CharStats charStats;
    private CombatCalculation combatCalc;

    [Header("Range Attacks")]
    public bool isProjectile;
    public float projectileSpeed;
    public bool isMagicDealing;
    private Rigidbody rbProjectile;
    private Vector3 shootDirection;

    private void Start()
    {
        combatCalc = FindObjectOfType<CombatCalculation>();

        if (isProjectile)
        {
            rbProjectile = GetComponent<Rigidbody>();
        }
        else
        {
            if (target == "Enemy")
            {
                charStats = GetComponentInParent<CharStats>();
            }
            else if (target == "Player")
            {
                enemyStats = GetComponentInParent<EnemyStats>();
            }
            else
            {
                Debug.Log("Nu ginagawa mo");
            }
        }
    }

    private void Update()
    {
        Projectile();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && target == "Enemy")
        {
            if (!isMagicDealing)
            {
                float FinalDamage = combatCalc.DoPhysDamage(charStats.currentAttack, charStats.currentCritRate, other.GetComponent<EnemyStats>().currentArmor);

                other.GetComponent<EnemyStats>().currentHealth -= FinalDamage;
            }
            else if (isMagicDealing)
            {
                float FinalDamage = combatCalc.DoPhysDamage(charStats.currentMagicAttack, charStats.currentCritRate, other.GetComponent<EnemyStats>().currentMagicDefense);

                other.GetComponent<EnemyStats>().currentHealth -= FinalDamage;
            }
        }
        else if (other.gameObject.tag == "Player" && target == "Player")
        {
            if (!isMagicDealing)
            {
                float FinalDamage = combatCalc.DoPhysDamage(enemyStats.currentAttack, 0f, other.GetComponent<CharStats>().currentArmor);

                other.GetComponent<CharStats>().currentHealth -= FinalDamage;
            }
            else if (isMagicDealing)
            {
                float FinalDamage = combatCalc.DoPhysDamage(enemyStats.currentMagicAttack, 0f, other.GetComponent<CharStats>().currentMagicDefense);

                other.GetComponent<CharStats>().currentHealth -= FinalDamage;
            }
            
        }
        else
        {
            Debug.Log("No target");
        }

        if (isProjectile && 
            (other.gameObject.tag == "Enemy" && target == "Enemy" || 
            other.gameObject.tag == "Player" && target == "Player"))
        {
            Destroy(this.gameObject);
        }
        else if (isProjectile)
        {
            Destroy(this.gameObject, 3f);
        }
    }

    void Projectile()
    {
        if (isProjectile)
        {
            rbProjectile.velocity = transform.forward * projectileSpeed;
        }
    }
}
