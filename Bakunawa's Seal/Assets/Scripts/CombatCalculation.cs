using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCalculation : MonoBehaviour
{
    public float DoPhysDamage(float AttackerDmgValue, float AttackerCrit, float DefenderArmor)
    {
        float didCrit = Random.Range(0, 100);
        if (didCrit <= AttackerCrit)
        {
            AttackerDmgValue *= 2;
            Debug.Log("Attack Critically Striked");
        }

        float TotalDamage = AttackerDmgValue - DefenderArmor;


        if(TotalDamage < 1)
        {
            TotalDamage = 1;
            Debug.Log("1 Damage");
            
        }

        return TotalDamage;
    }
}
