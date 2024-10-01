using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public string target;

    public bool isMagicDealing;
    private EnemyStats enemyStats;
    private CharStats charStats;
    private CombatCalculation combatCalc;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy") 
        { 
            float FinalDamage = combatCalc.DoPhysDamage(charStats.currentAttack, other.GetComponent<EnemyStats>().currentArmor);

            other.GetComponent<EnemyStats>().currentHealth -= FinalDamage;
        }
        else if (other.gameObject.tag == "Player")
        {
            float FinalDamage = combatCalc.DoPhysDamage(enemyStats.currentAttack, other.GetComponent<CharStats>().currentArmor);

            other.GetComponent<CharStats>().currentHealth -= FinalDamage;
        }
    }
}
