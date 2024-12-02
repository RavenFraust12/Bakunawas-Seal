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
    public GameObject buyFirst;

    [Header("Enemy")]
    public TextMeshProUGUI description;
    public Sprite[] enemyIcon;
    public Image enemyProfile;
    public TextMeshProUGUI enemyIdentity;

    [Header("Shop")]
    public bool didConfirmed;
    public float originalCoins;
    public float currentCoins;


    private void Start()
    {
        DefaultStats();
        currentCoins = PlayerPrefs.GetFloat("Coins", 0);
        PlayerPrefs.SetFloat("OriginalCoins", currentCoins);
    }
    private void Update()
    {
        StatText();
        for (int i = 0; i < characters.Count; i++)
        {
            CharStats stats = characters[i].GetComponent<CharStats>();
            int isBoughtStatus = PlayerPrefs.GetInt(stats.playerName + "_isBought", 0);
            if (isBoughtStatus == 1)
            {     
                // Set the character's name
                charButtons[i]?.SetActive(true);  // Set active if there is a character
                buyFirst.SetActive(false);
            }
            else if (isBoughtStatus == 0)
            {
                charButtons[i]?.SetActive(false); // Deactivate if no character is selected
            }
        }
    }

    public void UpdateBoughtCharacters()
    {
        //characters.Clear(); // Clear the list to start fresh

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
        //UpdateSelectedCharacterStats();

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
    public void DefaultStats()
    {
        charNameText.text = "";
        healthText.text = 100.ToString();
        attackText.text = 20.ToString();
        magicAttackText.text = 15.ToString();
        armorText.text = 5.ToString();
        magicDefText.text = 5.ToString();
        critRateText.text = 5.ToString();
        movespeedText.text = 5.ToString();
        attackspeedText.text = 1.5f.ToString();

        strText.text = 1.ToString();
        agiText.text = 1.ToString();
        intText.text = 1.ToString();
        dexText.text = 1.ToString();
        vitText.text = 1.ToString();

        //Attribute Price
        strPrice = 3f;
        agiPrice = 3f;
        intPrice = 3f;
        dexPrice = 3f;
        vitPrice = 3f;

        strPriceText.text = strPrice.ToString();
        agiPriceText.text = agiPrice.ToString();
        intPriceText.text = intPrice.ToString();
        dexPriceText.text = dexPrice.ToString();
        vitPriceText.text = vitPrice.ToString();
    }
    public void SelectChar(int selectNumber)
    {
        charStats = characters[selectNumber].GetComponent<CharStats>();
        charNumber = selectNumber;
        StatText();

        charName = charStats.playerName;
        ResetCoinsAndStats();
    }

    public void ResetCoinsAndStats()
    {
        originalCoins = PlayerPrefs.GetFloat("OriginalCoins", 0);
        if (!didConfirmed)
        {
            PlayerPrefs.SetFloat("Coins", originalCoins);
        }
        didConfirmed = false;

        charStats.strength = PlayerPrefs.GetFloat(charName + "_Str", 1);
        charStats.agility = PlayerPrefs.GetFloat(charName + "_Agi", 1);
        charStats.intelligence = PlayerPrefs.GetFloat(charName + "_Int", 1);
        charStats.dexterity = PlayerPrefs.GetFloat(charName + "_Dex", 1);
        charStats.vitality = PlayerPrefs.GetFloat(charName + "_Vit", 1);

        charProfile.sprite = charStats.charProfile;
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

        PlayerPrefs.SetFloat("OriginalCoins", PlayerPrefs.GetFloat("Coins", 0));
        didConfirmed = true;
        PlayerPrefs.Save();
    }
    public void AddAttribute(string attributeName)
    {
        currentCoins = PlayerPrefs.GetFloat("Coins", 0);

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
        currentCoins = PlayerPrefs.GetFloat("Coins", 0);

        if (attributeName == "Strength")
        {
            if (charStats.strength > baseStrength) 
            {
                currentCoins += strPrice - 3;
                charStats.strength--;
            }
        }
        else if (attributeName == "Agility")
        {
            if (charStats.agility > baseAgility)
            {
                currentCoins += agiPrice - 3;
                charStats.agility--; 
            }
        }
        else if (attributeName == "Intelligence")
        {
            if (charStats.intelligence > baseIntelligence)
            {
                currentCoins += intPrice - 3;
                charStats.intelligence--;
            }
        }
        else if (attributeName == "Dexterity") 
        {
            if (charStats.dexterity > baseDexterity)
            {
                currentCoins += dexPrice - 3;
                charStats.dexterity--;
            }
        }
        else if (attributeName == "Vitality") 
        {
            if (charStats.vitality > baseVitality)
            {
                currentCoins += vitPrice - 3;
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

    public void EnemyDescription(string enemyName)
    {
        if (enemyName == "Manananggal")
        {
            description.text = "The Manananggal is a vampire-like creature in Filipino folklore that detaches her upper body at night, growing bat-like wings to prey on pregnant women and newborns. Vulnerable when separated from her lower body, she can be repelled by garlic and salt, and her legend varies across the Visayas, making her one of the most feared figures in Philippine mythology.\r\n";
            enemyProfile.sprite = enemyIcon[0];
        }
        else if (enemyName == "Duwende")
        {
            description.text = "The Duwende is a small, dwarf-like creature in Filipino folklore that resides in mounds, forests, or old houses, known for its playful or mischievous nature. People say \"Tabi-tabi po\" to avoid bad luck, as Duwende can hoard items and bring misfortune, with red and black ones considered malicious and white and green ones seen as playful.";
            enemyProfile.sprite = enemyIcon[1];
        }
        else if (enemyName == "Mangkukulam")
        {
            description.text = "The Mangkukulam is a witch in Filipino folklore known for practicing black magic that inflicts ailments beyond modern medicine’s reach. Using personal items and voodoo dolls, she can summon pain and suffering, while traditional healers, or albularyos, may offer protection against her curses.";
            enemyProfile.sprite = enemyIcon[2];
        }
        else if (enemyName == "Tikbalang")
        {
            description.text = "Encounter the Tikbalang, a tall, bony humanoid from Philippine folklore, with the head and hooves of a horse and impossibly long limbs. This mysterious creature lurks in the mountains and rainforests, embodying eerie tales of transformation and the supernatural.";
            enemyProfile.sprite = enemyIcon[3];
        }
        else if (enemyName == "Diwata")
        {
            description.text = "Diwata are mystical nature spirits who guard the forests and waters, offering blessings to protectors of the land and curses to those who harm it. Dwelling in ancient trees, these elusive beings can be benevolent or fearsome, commanding both light and dark forces.";
            enemyProfile.sprite = enemyIcon[4];
        }

        enemyIdentity.text = enemyName;
    }
}
