using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DumakulemSkills : MonoBehaviour
{
    public bool canSkill;
    private CharStats charStats;

    public GameObject[] playerUnits;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
    }

    public void Update()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
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

            float[] addedArmorValues = new float[playerUnits.Length];

            for (int i = 0; i < playerUnits.Length; i++)
            {
                CharStats currentCharStats = playerUnits[i].GetComponent<CharStats>();

                float addedArmor = currentCharStats.currentArmor / 2f;
                currentCharStats.currentArmor += addedArmor;

                addedArmorValues[i] = addedArmor;
            }

            yield return new WaitForSeconds(5f);

            for (int i = 0; i < playerUnits.Length; i++)
            {
                CharStats currentCharStats = playerUnits[i].GetComponent<CharStats>();

                currentCharStats.currentArmor -= addedArmorValues[i];
            }

            yield return new WaitForSeconds((charStats.currentAttackspeed * 3f) + 3f);
            canSkill = true;
            Debug.Log("Dumakulem can skill again");
        }   
    }
}
