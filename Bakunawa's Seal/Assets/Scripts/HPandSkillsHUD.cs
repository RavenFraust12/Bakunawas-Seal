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

    [Header("Kills, Coins, Waves, and Time")]
    public TextMeshProUGUI killCount;
    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI waveCount;
    public TextMeshProUGUI timer;

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
                attackText[i].text = charStats.currentAttack.ToString();
                magicAttackText[i].text = charStats.currentMagicAttack.ToString();
                magicDefText[i].text = charStats.currentMagicDefense.ToString();
                critRateText[i].text = charStats.currentCritRate.ToString();
                movespeedText[i].text = charStats.currentMovespeed.ToString();
                attackspeedText[i].text = charStats.currentAttackspeed.ToString();

                float updateStr = PlayerPrefs.GetFloat(charStats.playerName + "_Str", 1);
                float updateAgi = PlayerPrefs.GetFloat(charStats.playerName + "_Agi", 1);
                float updateInt = PlayerPrefs.GetFloat(charStats.playerName + "_Int", 1);
                float updateDex = PlayerPrefs.GetFloat(charStats.playerName + "_Dex", 1);
                float updateVit = PlayerPrefs.GetFloat(charStats.playerName + "_Vit", 1);

                strText[i].text = updateStr.ToString();
                agiText[i].text = updateAgi.ToString();
                intText[i].text = updateInt.ToString();
                dexText[i].text = updateDex.ToString();
                vitText[i].text = updateVit.ToString();
            }
        }
    }

    public void Update()
    {
        killCount.text = GameManager.Instance.killCountText.text;
        coinCount.text = GameManager.Instance.coinCountText.text;
        waveCount.text = "Waves: " + GameManager.Instance.waveCountText.text;
        GameManager.Instance.Timer();
        timer.text = "GameTime: " + GameManager.Instance.finalTimer;


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Iterate through each player up to 4 players
        for (int i = 0; i < players.Length && i < 4; i++)
        {
            // Access the CharStats component in each player
            CharStats charStats = players[i].GetComponent<CharStats>();

            if (charStats != null)
            {
                // Update only the stats that change during gameplay (e.g., health, attack)
                healthText[i].text = charStats.currentHealth.ToString("F2");
                armorText[i].text = charStats.currentArmor.ToString("F2");
            }
        }
    }
}
