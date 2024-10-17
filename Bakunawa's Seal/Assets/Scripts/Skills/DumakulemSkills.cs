using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DumakulemSkills : MonoBehaviour
{
    public bool canSkill;
    private CharStats charStats;

    public GameObject[] playerUnits;

    public float maxCooldown;

    public bool skillCountdown;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
        
    }
    private void Start()
    {
        maxCooldown = (charStats.currentAttackspeed * 3f) + 3f;
        charStats.skillCooldown = maxCooldown;
        charStats.skillTime = maxCooldown;
    }
    public void Update()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
        if (!canSkill)
        {
            if(skillCountdown) charStats.skillTime += Time.deltaTime;
        }
        else
        {
            charStats.skillTime = maxCooldown;
        }
    }
    public void Skills()
    {
        StartCoroutine(SkillNiDumakulem());
    }

    IEnumerator SkillNiDumakulem()
    {
        if(canSkill)
        {
            charStats.skillTime = 0f;
            canSkill = false;

            float[] addedArmorValues = new float[playerUnits.Length];

            for (int i = 0; i < playerUnits.Length; i++)
            {
                CharStats currentCharStats = playerUnits[i].GetComponent<CharStats>();

                float addedArmor = currentCharStats.currentArmor / 2f;
                currentCharStats.currentArmor += addedArmor;

                addedArmorValues[i] = addedArmor;
            }

            skillCountdown = false;
            yield return new WaitForSeconds(5f);

            for (int i = 0; i < playerUnits.Length; i++)
            {
                CharStats currentCharStats = playerUnits[i].GetComponent<CharStats>();

                currentCharStats.currentArmor -= addedArmorValues[i];
            }
            skillCountdown = true;
            yield return new WaitForSeconds((charStats.currentAttackspeed * 3f) + 3f);
            canSkill = true;
            Debug.Log("Dumakulem can skill again");
        }   
    }

    public void DumakulemStats()
    {
        charStats.strength = PlayerPrefs.GetInt("Dumakulem_Str", 1);
        charStats.agility = PlayerPrefs.GetInt("Dumakulem_Agi", 1);
        charStats.intelligence = PlayerPrefs.GetInt("Dumakulem_Int", 1);
        charStats.dexterity = PlayerPrefs.GetInt("Dumakulem_Dex", 1);
        charStats.vitality = PlayerPrefs.GetInt("Dumakulem_Vit", 1);
    }

    public void AcceptUpgrade(float strength, float agility, float intelligence, float dexterity, float vitality)
    {
        PlayerPrefs.SetFloat("Dumakulem_Str", strength);
        PlayerPrefs.SetFloat("Dumakulem_Agi", agility);
        PlayerPrefs.SetFloat("Dumakulem_Int", intelligence);
        PlayerPrefs.SetFloat("Dumakulem_Dex", dexterity);
        PlayerPrefs.SetFloat("Dumakulem_Vit", vitality);

        PlayerPrefs.Save();
    }
}
