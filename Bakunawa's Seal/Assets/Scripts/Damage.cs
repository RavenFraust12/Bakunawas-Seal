using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public string target;

    [Header("Scripts")]
    private EnemyStats enemyStats;
    private CharStats charStats;
    private CombatCalculation combatCalc;

    [Header("Range Attacks")]
    public bool isProjectile;
    public float projectileSpeed;
    public bool isMagicDealing;
    private Rigidbody rbProjectile;

    private void Start()
    {
        combatCalc = FindObjectOfType<CombatCalculation>();

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

        if (isProjectile)
        {
            rbProjectile = GetComponent<Rigidbody>();
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
            Destroy(this.gameObject, 5f);
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
