using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MayariSkills : MonoBehaviour
{
    private CharStats charStats;
    public bool canSkill;
    public bool canHeal;
    public GameObject[] playerUnits;
    public GameObject healEffect;
    public GameObject heal;
    public Transform target;

    public float maxCooldown;

    private void Awake()
    {
        charStats = GetComponentInParent<CharStats>();
    }
    private void Start()
    {
        maxCooldown = (charStats.currentAttackspeed * 5f) + 5f;
        charStats.skillCooldown = maxCooldown;
        charStats.skillTime = maxCooldown;
        MayariStats();
    }
    public void Update()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
        if (!canSkill)
        {
            charStats.skillTime += Time.deltaTime;
        }
        else
        {
            charStats.skillTime = maxCooldown;
        }
    }
    public void Skills()
    {
        StartCoroutine(SkillNiMayari());
    }
    IEnumerator SkillNiMayari()
    {
        if (canSkill)
        {
            charStats.skillTime = 0f;
            canSkill = false;

            float closestDistance = Mathf.Infinity;
            GameObject closestPlayer = null;

            // Find the closest player with less than full health and more than 0 health
            foreach (GameObject player in playerUnits)
            {
                CharStats otherChar = player.GetComponent<CharStats>();

                // Skip the player if they are dead (currentHealth is 0)
                if (otherChar.currentHealth == 0)
                {
                    Debug.Log(otherChar.playerName + " is not healed");
                    continue;
                }

                // Skip checking for the caster unless their health is <= 50% of base health
                if (player == gameObject && otherChar.currentHealth > (charStats.baseHealth / 2f))
                {
                    continue;
                }

                // If the player's health is below full, consider them as a healing target
                if (otherChar.currentHealth < otherChar.baseHealth)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                    // Find the closest player to the caster
                    if (distanceToPlayer < closestDistance)
                    {
                        closestDistance = distanceToPlayer;
                        closestPlayer = player;
                    }
                }
            }

            // If a closest player was found, perform the healing
            if (closestPlayer != null)
            {
                CharStats closestCharStats = closestPlayer.GetComponent<CharStats>();

                // Heal the closest player or the caster themselves if they are below 50% health
                closestCharStats.currentHealth += (charStats.currentMagicAttack * 2f);

                // Trigger healing effect
                heal = Instantiate(healEffect, closestPlayer.transform.position, Quaternion.identity);
                Debug.Log("Healed " + closestPlayer.name);

                // Disable healing for the duration of the cooldown
                canHeal = false;
            }

            // Disable healing effect after a delay
            yield return new WaitForSeconds(1f);
            Destroy(heal.gameObject);

            // Wait for skill cooldown (based on current attack speed)
            yield return new WaitForSeconds((charStats.currentAttackspeed * 5) + 5);
            canHeal = true;
            canSkill = true;
        }
    }

    //walang condition na pag namatay any players di na siya dapat mag heal
    //IEnumerator SkillNiMayari()
    //{
    //    if (canSkill)
    //    {
    //        charStats.skillTime = 0f;
    //        canSkill = false;

    //        float closestDistance = Mathf.Infinity;
    //        GameObject closestPlayer = null;

    //        // Find the closest player with less than full health (excluding the caster unless their health is below 50%)
    //        foreach (GameObject player in playerUnits)
    //        {
    //            CharStats otherChar = player.GetComponent<CharStats>();

    //            // Skip checking for caster unless their health is <= 50% of their base health
    //            if (player == gameObject && otherChar.currentHealth > (charStats.baseHealth / 2f))
    //            {
    //                continue; // Skip to the next player if caster's health is > 50%
    //            }

    //            // If player's health is below full, consider them as a healing target
    //            if (otherChar.currentHealth < otherChar.baseHealth)
    //            {
    //                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

    //                // Find the closest player to the caster
    //                if (distanceToPlayer < closestDistance)
    //                {
    //                    closestDistance = distanceToPlayer;
    //                    closestPlayer = player;
    //                }
    //            }
    //        }

    //        // If a closest player was found, perform the healing
    //        if (closestPlayer != null)
    //        {
    //            CharStats closestCharStats = closestPlayer.GetComponent<CharStats>();

    //            // Heal the closest player or the caster themselves if they are below 50% health
    //            closestCharStats.currentHealth += (charStats.currentMagicAttack * 2f);

    //            // Trigger healing effect
    //            heal = Instantiate(healEffect, closestPlayer.transform.position, Quaternion.identity);
    //            Debug.Log("Healed " + closestPlayer.name);

    //            // Disable healing for the duration of the cooldown
    //            canHeal = false;
    //        }

    //        // Disable healing effect after a delay
    //        yield return new WaitForSeconds(1f);
    //        Destroy(heal.gameObject);

    //        // Wait for skill cooldown (based on current attack speed)
    //        yield return new WaitForSeconds((charStats.currentAttackspeed * 5) + 5);
    //        canHeal = true;
    //        canSkill = true;
    //    }
    //}

    public void MayariStats()
    {
        charStats.strength = PlayerPrefs.GetInt("Mayari_Str", 1);
        charStats.agility = PlayerPrefs.GetInt("Mayari_Agi", 1);
        charStats.intelligence = PlayerPrefs.GetInt("Mayari_Int", 1);
        charStats.dexterity = PlayerPrefs.GetInt("Mayari_Dex", 1);
        charStats.vitality = PlayerPrefs.GetInt("Mayari_Vit", 1);
    }

    public void AcceptUpgrade(float strength, float agility, float intelligence, float dexterity, float vitality)
    {
        PlayerPrefs.SetFloat("Mayari_Str", strength);
        PlayerPrefs.SetFloat("Mayari_Agi", agility);
        PlayerPrefs.SetFloat("Mayari_Int", intelligence);
        PlayerPrefs.SetFloat("Mayari_Dex", dexterity);
        PlayerPrefs.SetFloat("Mayari_Vit", vitality);

        PlayerPrefs.Save();
    }
}
