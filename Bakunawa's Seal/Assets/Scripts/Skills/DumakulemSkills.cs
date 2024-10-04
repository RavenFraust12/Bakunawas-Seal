using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumakulemSkills : MonoBehaviour
{
    public bool canSkill;
    private CharStats charStats;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
    }
    public void Skills()
    {
        StartCoroutine(SkillNiDumakulem());
    }

    IEnumerator SkillNiDumakulem()
    {
        if(canSkill)
        {
            canSkill = false;

            GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in playerUnits)
            {
                CharStats currentCharStats = player.GetComponent<CharStats>();

                float currentArmor = currentCharStats.currentArmor;
                currentCharStats.currentArmor += (currentCharStats.currentArmor / 2f);
                yield return new WaitForSeconds(5f);
                currentCharStats.currentArmor = currentArmor;
                
            }

            yield return new WaitForSeconds((charStats.currentAttackspeed * 3f) + 3f);
            canSkill = true;
        }   
    }
}
