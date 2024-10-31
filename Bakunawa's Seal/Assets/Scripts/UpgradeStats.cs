using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeStats : MonoBehaviour
{
    [Header("Archive Stats")]
    public TextMeshProUGUI charNameText;
    public string charName;
    public Image charProfile;
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
    //public GameObject[] characters;
    public List<GameObject> characters = new List<GameObject>(); // Use List for dynamic resizing
    public CharStats charStats;
    public int charNumber;
    public GameObject[] charButtons;

    [Header("Scripts")]
    private GameManager gameManager;


    private void Start()
    {
        //characters = new GameObject[4];
        UpdateBoughtCharacters(); // Populate the characters list with bought characters
        UpdateSelectedCharacterStats();
    }
    private void Update()
    {
        StatText();
        for (int i = 0; i < characters.Count; i++)
        {
            CharStats stats = characters[i].GetComponent<CharStats>();

            if (stats.isBought == 1)
            {     
                // Set the character's name
                charButtons[i]?.SetActive(true);  // Set active if there is a character
            }
            else if (stats.isBought == 0)
            {
                charButtons[i]?.SetActive(false); // Deactivate if no character is selected
            }
        }
    }

    public void UpdateBoughtCharacters()
    {
        characters.Clear(); // Clear the list to start fresh

        foreach (var character in GameManager.Instance.mm_charPrefab)
        {
            CharStats stats = character.GetComponent<CharStats>();

            if (stats != null && stats.isBought == 1) // Check if the character is bought
            {
                characters.Add(character); // Add to the characters list
            }
        }

        if (characters.Count == 0)
        {
            Debug.Log("No characters bought yet.");
        }
        else
        {
            Debug.Log(characters.Count + " characters have been added to the list.");
        }
    }
    public void UpdateSelectedCharacterStats()
    {
        if (characters == null || characters.Count == 0 || charStats == null)
        {
            Debug.Log("No character yet");
        }
        else if (characters != null && characters.Count > 0 && charNumber >= 0 && charNumber < characters.Count)
        {
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

            charNameText.text = charName;
            healthText.text = charStats.totalHealth.ToString();
            attackText.text = charStats.currentAttack.ToString();
            magicAttackText.text = charStats.currentMagicAttack.ToString();
            armorText.text = charStats.currentArmor.ToString();
            magicDefText.text = charStats.currentMagicDefense.ToString();
            critRateText.text = charStats.currentCritRate.ToString();
            movespeedText.text = charStats.currentMovespeed.ToString();
            attackspeedText.text = charStats.currentAttackspeed.ToString();

            //Attribute Text
            /*charStats.strength = PlayerPrefs.GetFloat(charName + "_Str", 1);
            charStats.agility = PlayerPrefs.GetFloat(charName + "_Agi", 1);
            charStats.intelligence = PlayerPrefs.GetFloat(charName + "_Int", 1);
            charStats.dexterity = PlayerPrefs.GetFloat(charName + "_Dex", 1);
            charStats.vitality = PlayerPrefs.GetFloat(charName + "_Vit", 1);*/

            strText.text = charStats.strength.ToString();
            agiText.text = charStats.agility.ToString();//PlayerPrefs.GetFloat(charName + "_Agi", 1).ToString();
            intText.text = charStats.intelligence.ToString();//PlayerPrefs.GetFloat(charName + "_Int", 1).ToString();
            dexText.text = charStats.dexterity.ToString();//PlayerPrefs.GetFloat(charName + "_Dex", 1).ToString();
            vitText.text = charStats.vitality.ToString();//PlayerPrefs.GetFloat(charName + "_Vit", 1).ToString();

            //Attribute Price
            strPrice = charStats.strength * 3f;
            agiPrice = charStats.agility * 3f;
            intPrice = charStats.intelligence * 3f;
            dexPrice = charStats.dexterity * 3f;
            vitPrice = charStats.vitality * 3;

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

        charProfile.sprite = charStats.charProfile;
        charName = charStats.playerName;
        baseStrength = charStats.strength;
        baseAgility = charStats.agility;
        baseIntelligence = charStats.intelligence;
        baseDexterity = charStats.dexterity;
        baseVitality = charStats.vitality;
    }
    public void ConfirmStatUpgrade()
    {
        PlayerPrefs.SetFloat(charName + "_Str", charStats.strength);
        PlayerPrefs.SetFloat(charName + "_Agi", charStats.agility);
        PlayerPrefs.SetFloat(charName + "_Int", charStats.intelligence);
        PlayerPrefs.SetFloat(charName + "_Dex", charStats.dexterity);
        PlayerPrefs.SetFloat(charName + "_Vit", charStats.vitality);

        baseStrength = charStats.strength;
        baseAgility = charStats.agility;
        baseIntelligence = charStats.intelligence;
        baseDexterity = charStats.dexterity;
        baseVitality = charStats.vitality;

        PlayerPrefs.Save();

        Debug.Log("Saved " + charName + "'s stats.");

        /*if (charNumber >= 0 && charNumber <= characters.Length)
        {
            GameObject selectedUnit = characters[charNumber]; // charNumber is 1-based, so subtract 1

            switch (charNumber)
            {
                case 0:
                    ApolakiSkills apolaki = selectedUnit.GetComponent<ApolakiSkills>();
                    if (apolaki != null)
                        apolaki.AcceptUpgrade(charStats.strength, charStats.agility, charStats.intelligence, charStats.dexterity, charStats.vitality);
                    else
                        Debug.LogError("ApolakiSkills not found on the selected unit");
                    break;
                case 1:
                    MayariSkills mayari = selectedUnit.GetComponent<MayariSkills>();
                    if (mayari != null)
                        mayari.AcceptUpgrade(charStats.strength, charStats.agility, charStats.intelligence, charStats.dexterity, charStats.vitality);
                    else
                        Debug.LogError("MayariSkills not found on the selected unit");
                    break;
                case 2:
                    DumanganSkills dumangan = selectedUnit.GetComponent<DumanganSkills>();
                    if (dumangan != null)
                        dumangan.AcceptUpgrade(charStats.strength, charStats.agility, charStats.intelligence, charStats.dexterity, charStats.vitality);
                    else
                        Debug.LogError("DumanganSkills not found on the selected unit");
                    break;
                case 3:
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
        }*/
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
        float currentCoins = PlayerPrefs.GetFloat("Coins", 0);

        if (attributeName == "Strength")
        {
            if (charStats.strength > baseStrength) 
            {
                currentCoins += strPrice - 5;
                charStats.strength--;
            }
        }
        else if (attributeName == "Agility")
        {
            if (charStats.agility > baseAgility)
            {
                currentCoins += agiPrice - 5;
                charStats.agility--; 
            }
        }
        else if (attributeName == "Intelligence")
        {
            if (charStats.intelligence > baseIntelligence)
            {
                currentCoins += intPrice - 5;
                charStats.intelligence--;
            }
        }
        else if (attributeName == "Dexterity") 
        {
            if (charStats.dexterity > baseDexterity)
            {
                currentCoins += dexPrice - 5;
                charStats.dexterity--;
            }
        }
        else if (attributeName == "Vitality") 
        {
            if (charStats.vitality > baseVitality)
            {
                currentCoins += vitPrice - 5;
                charStats.vitality--;
            }
        }

        PlayerPrefs.SetFloat("Coins", currentCoins); // Save the updated coin count
    }
    public void ResetStats()
    {
        charStats.strength = 1;
        charStats.agility = 1;
        charStats.intelligence = 1;
        charStats.dexterity = 1;
        charStats.vitality = 1;

        PlayerPrefs.DeleteKey(charName + "_Str");
        PlayerPrefs.DeleteKey(charName + "_Agi");
        PlayerPrefs.DeleteKey(charName + "_Int");
        PlayerPrefs.DeleteKey(charName + "_Dex");
        PlayerPrefs.DeleteKey(charName + "_Vit");
        PlayerPrefs.Save();
    }
}
