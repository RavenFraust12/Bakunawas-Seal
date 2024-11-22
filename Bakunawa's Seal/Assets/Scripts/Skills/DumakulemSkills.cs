using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DumakulemSkills : MonoBehaviour //Shield guy to, not the bow guy
{
    public bool canSkill;
    private CharStats charStats;

    public GameObject[] playerUnits;
    public GameObject shieldVFX;

    public float maxCooldown;

    public bool skillCountdown;

    private AnimationManager animator;
    private PlayerAI playerAI;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
        
    }
    private void Start()
    {
        animator = GetComponentInChildren<AnimationManager>();
        playerAI = GetComponent<PlayerAI>();
        maxCooldown = (charStats.currentAttackspeed * 3f) + 3f;
        charStats.skillCooldown = maxCooldown;
        charStats.skillTime = maxCooldown;
        DumanganStats();
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
            playerAI.canAttack = false;
            animator.PlaySkill();
            float[] addedArmorValues = new float[playerUnits.Length];

            for (int i = 0; i < playerUnits.Length; i++)
            {
                CharStats currentCharStats = playerUnits[i].GetComponent<CharStats>();

                float addedArmor = currentCharStats.currentArmor / 2f;
                currentCharStats.currentArmor += addedArmor;

                addedArmorValues[i] = addedArmor;

                Instantiate(shieldVFX, playerUnits[i].transform.position, Quaternion.identity, playerUnits[i].transform);
            }

            skillCountdown = false;

            float attackAnimDuration = animator.animator.GetCurrentAnimatorStateInfo(0).length / animator.animator.speed; // Get attack animation length, adjusting for speed
            yield return new WaitForSeconds(attackAnimDuration);
            playerAI.canAttack = true;

            yield return new WaitForSeconds(5f);

            for (int i = 0; i < playerUnits.Length; i++)
            {
                CharStats currentCharStats = playerUnits[i].GetComponent<CharStats>();

                currentCharStats.currentArmor -= addedArmorValues[i];
            }
            skillCountdown = true;
            yield return new WaitForSeconds((charStats.currentAttackspeed * 3f) + 3f);
            canSkill = true;
        }   
    }

    public void DumanganStats()
    {
        charStats.strength = PlayerPrefs.GetInt("Dumangan_Str", 1);
        charStats.agility = PlayerPrefs.GetInt("Dumangan_Agi", 1);
        charStats.intelligence = PlayerPrefs.GetInt("Dumangan_Int", 1);
        charStats.dexterity = PlayerPrefs.GetInt("Dumangan_Dex", 1);
        charStats.vitality = PlayerPrefs.GetInt("Dumangan_Vit", 1);
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
