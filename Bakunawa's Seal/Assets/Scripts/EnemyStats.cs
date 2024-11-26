using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Base Enemy Stat")]
    public float baseHealth = 10;
    public float baseAttack = 1;
    public float baseMagicAttack = 1;
    public float baseArmor = 1;
    public float baseMagicDefense = 1;

    [Header("Current Enemy Stat")]
    public float currentHealth;
    public float currentAttack;
    public float currentMagicAttack;
    public float currentArmor;
    public float currentMagicDefense;
    public float movespeed;
    public float attackspeed;

    [Header("Enemy Attributes")]
    public float strength = 1;
    public float agility = 1;
    public float intelligence = 1;
    public float dexterity = 1;
    public float vitality = 1;
    public float currentStat;

    [Header("Drops")]
    public GameObject lootDrop;
    private GameObject coinHolder;
    public GameObject bloodVFX;

    [Header("Scripts")]
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        coinHolder = GameObject.Find("Coin Holder");
        AttributeCalculation();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            spawnManager.EnemyDestroyed();

            ChanceToDrop();
        }
    }

    void AttributeCalculation()
    {
        strength += currentStat;
        intelligence += currentStat;
        agility += currentStat;
        dexterity += currentStat;
        vitality += currentStat;

        StatCalculation();
    }
    void StatCalculation()
    {
        currentHealth = baseHealth + (5 * strength) + (2 * intelligence) + (10 * vitality);
        currentAttack = baseAttack + (2 * strength) + (1 * dexterity);
        currentMagicAttack = baseMagicAttack + (2 * intelligence);
        currentArmor = baseArmor + (0.05f * strength) + (0.02f * agility) + (0.1f * vitality);
        currentMagicDefense = baseMagicDefense + (0.1f * intelligence) + (0.05f * vitality);
    }

    public void ChanceToDrop()
    {
        Instantiate(bloodVFX, transform.position, Quaternion.identity);
        if (Random.Range(0, 100) <= 50f)
        {
            Instantiate(lootDrop, transform.position, Quaternion.identity, coinHolder.transform);
        }
        Destroy(this.gameObject);
    }
    
}
