using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStats : MonoBehaviour
{
    [Header("Base Character Stat")]
    private float baseHealth = 100;
    private float baseAttack = 20;
    private float baseMagicAttack = 15;
    private float baseArmor = 5;
    private float baseMagicDefense = 5;
    private float baseCritRate = 5;
    private float baseMovespeed = 5;
    private float baseAttackSpeed = 1.5f;

    [Header("Current Character Stat")]
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
    public bool isDead;


    private void Start()
    {
        StatCalculation();
    }

    private void Update()
    {
        DeathChecker();
    }

    void DeathChecker()
    {
        if (currentHealth <= 0)
        {
            //Do animation
            currentHealth = 0;
            isDead = true;
        }
        else if (currentHealth >= baseHealth)
        {
            currentHealth = baseHealth;
            isDead = false;
        }
        else
        {
            isDead = false;
        }
    }
    void StatCalculation()
    {

        baseHealth = baseHealth + (25 * strength) + (10 * intelligence) + (50 * vitality);
        currentAttack = baseAttack + (5 * strength) + (3 * dexterity);
        currentMagicAttack = baseMagicAttack + (5 * intelligence);
        currentArmor = baseArmor + (0.5f * strength) + (0.2f * agility) + (1f * vitality);
        currentMagicDefense = baseMagicDefense + (1f * intelligence) + (0.5f * vitality);
        currentCritRate = baseCritRate + (0.5f * agility) + (0.2f * intelligence) + (1f * dexterity);
        currentMovespeed = baseMovespeed + ((0.2f / 100) * agility);
        currentAttackspeed = baseAttackSpeed - ((1 / 100) * agility) - ((0.5f / 100) * dexterity);

        currentHealth = baseHealth;

        if (currentCritRate >= 100) currentCritRate = 100;
        if (currentAttackspeed <= 0.25f) currentAttackspeed = 0.25f;

    }
}
