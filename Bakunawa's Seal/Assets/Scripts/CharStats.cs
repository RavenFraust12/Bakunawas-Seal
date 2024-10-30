using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharStats : MonoBehaviour
{
    [Header("Base Character Stat")]
    public float baseHealth = 100;
    public float baseAttack = 20;
    public float baseMagicAttack = 15;
    public float baseArmor = 5;
    public float baseMagicDefense = 5;
    public float baseCritRate = 5;
    public float baseMovespeed = 5;
    public float baseAttackSpeed = 1.5f;

    [Header("Current Character Stat")]
    public float totalHealth;
    public float currentHealth;
    public float currentAttack;
    public float currentMagicAttack;
    public float currentArmor;
    public float currentMagicDefense;
    public float currentCritRate;
    public float currentMovespeed;
    public float currentAttackspeed;

    [Header("Character Attributes")]
    public float strength = 1;
    public float agility = 1;
    public float intelligence = 1;
    public float dexterity = 1;
    public float vitality = 1;

    [Header("Gameplay")]
    public Sprite charProfile;
    public string playerName;
    public bool isDead;
    public float skillCooldown;
    public float skillTime;
    public int isBought; // 0 is for sale, 1 is for bought


    private void Start()
    {
        PlayerPrefsToAttribute();
        StatCalculation();
    }

    private void Update()
    {
        isBought = PlayerPrefs.GetInt(playerName + "_isBought", 0);

        DeathChecker();
        if (currentCritRate >= 100) currentCritRate = 100;
        if (currentAttackspeed <= 0.25f) currentAttackspeed = 0.25f;
    }
    void DeathChecker()
    {
        if (currentHealth <= 0)
        {
            //Do animation
            currentHealth = 0;
            isDead = true;
        }
        else if (currentHealth >= totalHealth)
        {
            currentHealth = totalHealth;
            isDead = false;
        }
        else
        {
            isDead = false;
        }
    }
    public void StatCalculation()
    {

        currentHealth = baseHealth + (25f * strength) + (10f * intelligence) + (50f * vitality);
        currentAttack = baseAttack + (5f * strength) + (3f * dexterity);
        currentMagicAttack = baseMagicAttack + (5f * intelligence);
        currentArmor = baseArmor + (0.5f * strength) + (0.2f * agility) + (1f * vitality);
        currentMagicDefense = baseMagicDefense + (1f * intelligence) + (0.5f * vitality);
        currentCritRate = baseCritRate + (0.5f * agility) + (0.2f * intelligence) + (1f * dexterity);
        currentMovespeed = baseMovespeed + ((0.2f / 100) * agility);
        currentAttackspeed = baseAttackSpeed - ((1f / 100f) * agility) - ((0.5f / 100f) * dexterity);

        totalHealth = currentHealth;
    }

    public void PlayerPrefsToAttribute()
    {
        strength = PlayerPrefs.GetFloat(playerName + "_Str", 1);
        agility = PlayerPrefs.GetFloat(playerName + "_Agi", 1);
        intelligence = PlayerPrefs.GetFloat(playerName + "_Int", 1);
        dexterity = PlayerPrefs.GetFloat(playerName + "_Dex", 1);
        vitality = PlayerPrefs.GetFloat(playerName + "_Vit", 1);

        Debug.Log(playerName + "'s Str:" + strength + ", Agi:" + agility + ", Int:" + intelligence + ", Dex:" +  dexterity + ", Vit:" + vitality);
    }
}
