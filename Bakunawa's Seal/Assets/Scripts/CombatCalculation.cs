using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCalculation : MonoBehaviour
{
    public float DoPhysDamage(float AttackerDmgValue, float DefenderArmor)
    {
        float TotalDamage = AttackerDmgValue - DefenderArmor;

        if (TotalDamage > 0)
        {
            Debug.Log("Damage log final damage " + TotalDamage);
            return TotalDamage;
        }
        else
        {
            Debug.Log("1 Damage");
            return 1;
        }


    }
}
