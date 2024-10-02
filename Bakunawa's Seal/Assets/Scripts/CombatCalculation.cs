using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCalculation : MonoBehaviour
{
    public float DoPhysDamage(float AttackerDmgValue, float DefenderArmor)
    {
        float TotalDamage = AttackerDmgValue - DefenderArmor;

        /*if (TotalDamage > 0)
        {
            Debug.Log("Damage log final damage " + TotalDamage);
        }
        else */if(TotalDamage < 1)
        {
            TotalDamage = 1;
            Debug.Log("1 Damage");
            
        }

        return TotalDamage;
    }
}
