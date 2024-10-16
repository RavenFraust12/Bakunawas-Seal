using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeStats : MonoBehaviour
{
    [Header("Archive Stats")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI magicAttackText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI magicDefText;
    public TextMeshProUGUI critRateText;
    public TextMeshProUGUI movespeedText;
    public TextMeshProUGUI attackspeedText;

    [Header("Archive Attribute")]
    public TextMeshProUGUI strText;
    public TextMeshProUGUI agiText;
    public TextMeshProUGUI intText;
    public TextMeshProUGUI dexText;
    public TextMeshProUGUI vitText;

    [Header("Archive Attribute Upgrade")]
    public TextMeshProUGUI strUpgrade;
    public TextMeshProUGUI agiUpgrade;
    public TextMeshProUGUI intUpgrade;
    public TextMeshProUGUI dexUpgrade;
    public TextMeshProUGUI vitUpgrade;
    public float baseStrength;
    public float baseAgility;
    public float baseIntelligence;
    public float baseDexterity;
    public float baseVitality;

    [Header("Archive Upgrade Price")]
    public TextMeshProUGUI strPriceText;
    public TextMeshProUGUI agiPriceText;
    public TextMeshProUGUI intPriceText;
    public TextMeshProUGUI dexPriceText;
    public TextMeshProUGUI vitPriceText;
    public float strPrice;
    public float agiPrice;
    public float intPrice;
    public float dexPrice;
    public float vitPrice;

[Header("Characters")]
    public GameObject[] characters;
    public CharStats charStats;
    public int charNumber;

    [Header("Scripts")]
    private GameManager gameManager;


    private void Awake()
    {
        
    }

    private void Start()
    {
        UpdateSelectedCharacterStats();

        baseStrength = charStats.strength;
        baseAgility = charStats.agility;
        baseIntelligence = charStats.intelligence;
        baseDexterity = charStats.dexterity;
        baseVitality = charStats.vitality;
    }
    private void Update()
    {
        StatText();
    }

    public void UpdateSelectedCharacterStats()
    {
        if (characters != null && characters.Length > 0 && charNumber >= 0 && charNumber < characters.Length)
        {
            // Get the CharStats component from the selected character
            charStats = characters[charNumber].GetComponent<CharStats>();
        }
    }
    public void StatText()
    {
        UpdateSelectedCharacterStats();

        if (charStats != null)
        {
            charStats.StatCalculation();
            //Stat Text
            healthText.text = charStats.totalHealth.ToString();
            attackText.text = charStats.currentAttack.ToString();
            magicAttackText.text = charStats.currentMagicAttack.ToString();
            armorText.text = charStats.currentArmor.ToString();
            magicDefText.text = charStats.currentMagicDefense.ToString();
            critRateText.text = charStats.currentCritRate.ToString();
            movespeedText.text = charStats.currentMovespeed.ToString();
            attackspeedText.text = charStats.currentAttackspeed.ToString();

            //Attribute Text
            strText.text = charStats.strength.ToString();
            agiText.text = charStats.agility.ToString();
            intText.text = charStats.intelligence.ToString();
            dexText.text = charStats.dexterity.ToString();
            vitText.text = charStats.vitality.ToString();

            //Attribute Price
            strPrice = charStats.strength * 5f;
            agiPrice = charStats.agility * 5f;
            intPrice = charStats.intelligence * 5f;
            dexPrice = charStats.dexterity * 5f;
            vitPrice = charStats.vitality * 5f;

            strPriceText.text = strPrice.ToString();
            agiPriceText.text = agiPrice.ToString();
            intPriceText.text = intPrice.ToString();
            dexPriceText.text = dexPrice.ToString();
            vitPriceText.text = vitPrice.ToString();
        }
    }
    public void SelectChar(int selectNumber)
    {
        charNumber = selectNumber;
        StatText();

        baseStrength = charStats.strength;
        baseAgility = charStats.agility;
        baseIntelligence = charStats.intelligence;
        baseDexterity = charStats.dexterity;
        baseVitality = charStats.vitality;
    }
    public void ConfirmStatUpgrade()
    {
        if (charNumber >= 0 && charNumber <= characters.Length)
        {
            GameObject selectedUnit = characters[charNumber]; // charNumber is 1-based, so subtract 1

            switch (charNumber)
            {
                case 1:
                    ApolakiSkills apolaki = selectedUnit.GetComponent<ApolakiSkills>();
                    if (apolaki != null)
                        apolaki.AcceptUpgrade(charStats.strength, charStats.agility, charStats.intelligence, charStats.dexterity, charStats.vitality);
                    else
                        Debug.LogError("ApolakiSkills not found on the selected unit");
                    break;
                case 2:
                    MayariSkills mayari = selectedUnit.GetComponent<MayariSkills>();
                    if (mayari != null)
                        mayari.AcceptUpgrade(charStats.strength, charStats.agility, charStats.intelligence, charStats.dexterity, charStats.vitality);
                    else
                        Debug.LogError("MayariSkills not found on the selected unit");
                    break;
                case 3:
                    DumanganSkills dumangan = selectedUnit.GetComponent<DumanganSkills>();
                    if (dumangan != null)
                        dumangan.AcceptUpgrade(charStats.strength, charStats.agility, charStats.intelligence, charStats.dexterity, charStats.vitality);
                    else
                        Debug.LogError("DumanganSkills not found on the selected unit");
                    break;
                case 4:
                    DumakulemSkills dumakulem = selectedUnit.GetComponent<DumakulemSkills>();
                    if (dumakulem != null)
                        dumakulem.AcceptUpgrade(charStats.strength, charStats.agility, charStats.intelligence, charStats.dexterity, charStats.vitality);
                    else
                        Debug.LogError("DumakulemSkills not found on the selected unit");
                    break;
                default:
                    Debug.LogError("Invalid charNumber, no skill detected");
                    break;
            }
        }
        else
        {
            Debug.LogError("charNumber out of range of playerUnits array");
        }
    }
    public void AddAttribute(string attributeName)
    {
        float currentCoins = PlayerPrefs.GetFloat("Coins", 0);

        if (attributeName == "Strength" && strPrice <= currentCoins)
        {
            currentCoins -= strPrice;
            charStats.strength++;
        }
        else if (attributeName == "Agility" && agiPrice <= currentCoins)
        {
            currentCoins -= agiPrice;
            charStats.agility++;
        }
        else if (attributeName == "Intelligence" && intPrice <= currentCoins)
        {
            currentCoins -= intPrice;
            charStats.intelligence++;
        }
        else if (attributeName == "Dexterity" && dexPrice <= currentCoins)
        {
            currentCoins -= dexPrice;
            charStats.dexterity++;
        }
        else if (attributeName == "Vitality" && vitPrice <= currentCoins)
        {
            currentCoins -= vitPrice;
            charStats.vitality++;
        }

        PlayerPrefs.SetFloat("Coins", currentCoins); // Save the updated coin count

    }
    public void SubtractAttribute(string attributeName)
    {
        if (attributeName == "Strength")
        {
            if (charStats.strength > baseStrength) charStats.strength--;
        }
        else if (attributeName == "Agility")
        {
            if (charStats.agility > baseAgility) charStats.agility--;
        }
        else if (attributeName == "Intelligence")
        {
            if (charStats.intelligence > baseIntelligence) charStats.intelligence--;
        }
        else if (attributeName == "Dexterity") 
        {
            if (charStats.dexterity > baseDexterity) charStats.dexterity--; 
        }
        else if (attributeName == "Vitality") 
        {
            if (charStats.vitality > baseVitality) charStats.vitality--;
        }
    }
    public void ResetStats()
    {

        charStats.strength = 1;
        charStats.agility = 1;
        charStats.intelligence = 1;
        charStats.dexterity = 1;
        charStats.vitality = 1;
    }
}
