using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolakiSkills : MonoBehaviour
{
    public GameObject weaponSkill;
    public bool canSkill;
    private CharStats charStats;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
    }

    public void Skills()
    {
        StartCoroutine(SkillNiApolaki());
    }

    IEnumerator SkillNiApolaki()
    {
        if(canSkill)
        {
            canSkill = false;
            weaponSkill.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            weaponSkill.SetActive(false);
            yield return new WaitForSeconds(5);
            canSkill = true;
        }
    }
}
