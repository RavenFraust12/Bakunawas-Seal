using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolakiSkills : MonoBehaviour
{
    public GameObject weaponSkill;
    public bool canSkill;
    private CharStats charStats;
    public float maxCooldown;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
    }

    private void Start()
    {
        maxCooldown = (charStats.currentAttackspeed * 3f) + 3f;
        ApolakiStats();
        charStats.skillCooldown = maxCooldown;
        charStats.skillTime = maxCooldown;
    }
    public void Skills()
    {
        StartCoroutine(SkillNiApolaki());
    }

    public void Update()
    {
        if (!canSkill)
        {
            charStats.skillTime += Time.deltaTime;
        }   
        else
        {
            charStats.skillTime = maxCooldown;
        }
    }

    IEnumerator SkillNiApolaki()
    {
        if(canSkill)
        {
            charStats.skillTime = 0f;
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

    public void AcceptUpgrade(float strength, float agility, float intelligence, float dexterity, float vitality)
    {
        PlayerPrefs.SetFloat("Apolaki_Str", strength);
        PlayerPrefs.SetFloat("Apolaki_Agi", agility);
        PlayerPrefs.SetFloat("Apolaki_Int", intelligence);
        PlayerPrefs.SetFloat("Apolaki_Dex", dexterity);
        PlayerPrefs.SetFloat("Apolaki_Vit", vitality);

        PlayerPrefs.Save();
    }
}
