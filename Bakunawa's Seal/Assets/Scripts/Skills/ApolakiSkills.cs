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
        //ApolakiStats();
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
            yield return new WaitForSeconds(0.2f);
            weaponSkill.SetActive(false);
            yield return new WaitForSeconds((charStats.currentAttackspeed * 3f) + 3f);
            canSkill = true;
        }
    }

    public void ApolakiStats()
    {
        charStats.strength = PlayerPrefs.GetInt("Apolaki_Str", 1);
        charStats.agility = PlayerPrefs.GetInt("Apolaki_Agi", 1);
        charStats.intelligence = PlayerPrefs.GetInt("Apolaki_Int", 1);
        charStats.dexterity = PlayerPrefs.GetInt("Apolaki_Dex", 1);
        charStats.vitality = PlayerPrefs.GetInt("Apolaki_Vit", 1);
    }

    public void AcceptUpgrade()
    {
        PlayerPrefs.SetFloat("Apolaki_Str", charStats.strength);
        PlayerPrefs.SetFloat("Apolaki_Agi", charStats.agility);
        PlayerPrefs.SetFloat("Apolaki_Int", charStats.intelligence);
        PlayerPrefs.SetFloat("Apolaki_Dex", charStats.dexterity);
        PlayerPrefs.SetFloat("Apolaki_Vit", charStats.vitality);

        PlayerPrefs.Save();
    }
}
