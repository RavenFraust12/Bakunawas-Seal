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

    [Header("Archive Upgrade Price")]
    public TextMeshProUGUI strPrice;
    public TextMeshProUGUI agiPrice;
    public TextMeshProUGUI intPrice;
    public TextMeshProUGUI dexPrice;
    public TextMeshProUGUI vitPrice;

    [Header("Characters")]
    public GameObject[] characters;
    public CharStats charStats;
    public int charNumber;

    [Header("Scripts")]
    private GameManager gameManager;
    

    private void Awake()
    {
        // Ensure that stats are calculated once for all characters in the array
        foreach (GameObject playerUnit in characters)
        {
            charStats = playerUnit.GetComponent<CharStats>();
            if (charStats != null)
            {
                charStats.StatCalculation(); // Calculate stats for each character
            }
        }
    }
    private void Update()
    {

        //StatText();
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
        }    
    }

    public void SelectChar(int selectNumber)
    {
        charNumber = selectNumber;
        StatText();
    }
}
