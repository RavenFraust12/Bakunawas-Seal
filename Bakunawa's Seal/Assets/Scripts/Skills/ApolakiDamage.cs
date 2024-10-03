using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolakiDamage : MonoBehaviour
{
    public string target;

    private CharStats charStats;
    private CombatCalculation combatCalc;

    private void Start()
    {
        combatCalc = FindObjectOfType<CombatCalculation>();
        charStats = GetComponentInParent<CharStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            float FinalDamage = combatCalc.DoPhysDamage(charStats.currentAttack, charStats.currentCritRate, other.GetComponent<EnemyStats>().currentArmor);

            other.GetComponent<EnemyStats>().currentHealth -= (FinalDamage * 2);
        }
    }
}
