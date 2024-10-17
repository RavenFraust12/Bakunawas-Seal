using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPandSkillsHUD : MonoBehaviour
{
    [Header("Char Stats HUD")]
    public TextMeshProUGUI[] charNames;

    public TextMeshProUGUI[] healthText;
    public TextMeshProUGUI[] attackText;
    public TextMeshProUGUI[] magicAttackText;
    public TextMeshProUGUI[] armorText;
    public TextMeshProUGUI[] magicDefText;
    public TextMeshProUGUI[] critRateText;
    public TextMeshProUGUI[] movespeedText;
    public TextMeshProUGUI[] attackspeedText;

    public TextMeshProUGUI[] strText;
    public TextMeshProUGUI[] agiText;
    public TextMeshProUGUI[] intText;
    public TextMeshProUGUI[] dexText;
    public TextMeshProUGUI[] vitText;

    public void Start()
    {
        // Find all PlayerObjects in the scene (tag them as "Player")
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Iterate through each player up to 4 players
        for (int i = 0; i < players.Length && i < 4; i++)
        {
            // Access the CharStats component in each player (assuming you have this script in your player objects)
            CharStats charStats = players[i].GetComponent<CharStats>();

            if (charStats != null)
            {
                // Update the UI with the respective player stats
                charNames[i].text = charStats.playerName;
                healthText[i].text = charStats.currentHealth.ToString();
                attackText[i].text = charStats.currentAttack.ToString();
                magicAttackText[i].text = charStats.currentMagicAttack.ToString();
                armorText[i].text = charStats.currentArmor.ToString();
                magicDefText[i].text = charStats.currentMagicDefense.ToString();
                critRateText[i].text = charStats.currentCritRate.ToString();
                movespeedText[i].text = charStats.currentMovespeed.ToString();
                attackspeedText[i].text = charStats.currentAttackspeed.ToString();

                strText[i].text = charStats.strength.ToString();
                agiText[i].text = charStats.agility.ToString();
                intText[i].text = charStats.intelligence.ToString();
                dexText[i].text = charStats.dexterity.ToString();
                vitText[i].text = charStats.vitality.ToString();
            }
        }
    }
}
