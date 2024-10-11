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
        StartCoroutine(SkillNiMayari());
    }
    IEnumerator SkillNiMayari()
    {
        if (canSkill)
        {
            canSkill = false;

            float closestDistance = Mathf.Infinity;
            GameObject closestPlayer = null;

            // Find the closest player with less than full health (excluding the caster unless their health is below 50%)
            foreach (GameObject player in playerUnits)
            {
                CharStats otherChar = player.GetComponent<CharStats>();

                // Skip checking for caster unless their health is <= 50% of their base health
                if (player == gameObject && otherChar.currentHealth > (charStats.baseHealth / 2f))
                {
                    continue; // Skip to the next player if caster's health is > 50%
                }

                // If player's health is below full, consider them as a healing target
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

    public void MayariStats()
    {
        charStats.strength = PlayerPrefs.GetInt("Mayari_Str", 1);
        charStats.agility = PlayerPrefs.GetInt("Mayari_Agi", 1);
        charStats.intelligence = PlayerPrefs.GetInt("Mayari_Int", 1);
        charStats.dexterity = PlayerPrefs.GetInt("Mayari_Dex", 1);
        charStats.vitality = PlayerPrefs.GetInt("Mayari_Vit", 1);
    }

    public void AcceptUpgrade()
    {
        PlayerPrefs.SetFloat("Mayari_Str", charStats.strength);
        PlayerPrefs.SetFloat("Mayari_Agi", charStats.agility);
        PlayerPrefs.SetFloat("Mayari_Int", charStats.intelligence);
        PlayerPrefs.SetFloat("Mayari_Dex", charStats.dexterity);
        PlayerPrefs.SetFloat("Mayari_Vit", charStats.vitality);

        PlayerPrefs.Save();
    }
}
