using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public CharSelect charSelect;
    public UpgradeStats archive;

    public TextMeshProUGUI currentCoins;
    public float cost = 0;
    public int currentIndex;

    public void Start()
    {
        /*archive.characters = new GameObject[10];  // Adjust size as needed
        charSelect.characterPrefabs = new GameObject[10];*/
        //charSelect = FindObjectOfType<CharSelect>();
        //archive = FindObjectOfType<UpgradeStats>();
    }

    public void Update()
    {
        currentIndex = PlayerPrefs.GetInt("Bought Units", 0);
        cost = 500 * currentIndex;
        //currentCoins.text = PlayerPrefs.GetFloat("Coins", 0).ToString();
    }
    public void BuyHero(GameObject playerUnit)
    {
        float coinCount = PlayerPrefs.GetFloat("Coins", 1);
        if (cost < coinCount)
        {
            if (currentIndex < archive.characters.Length)
            {
                archive.characters[currentIndex] = playerUnit;
                charSelect.characterPrefabs[currentIndex] = playerUnit;

                currentIndex++;
                coinCount -= cost;
                cost += 500;

                PlayerPrefs.SetInt("Bought Units", currentIndex);
                PlayerPrefs.SetFloat("Coins", coinCount);

                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log("wow laki etit");
            }

            
        }
    }

}
