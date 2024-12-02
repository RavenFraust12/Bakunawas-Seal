using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI currentCoins;
    public TextMeshProUGUI currentCost;

    public float cost = 0;
    public int currentIndex;

    public GameObject cantAfford;

    [Header("Referenced Scripts")]
    public CharSelect charSelect;
    public UpgradeStats archive;

    [Header("Character Prefabs")]
    public GameObject apolakiPrefab;
    public GameObject mayariPrefab;
    public GameObject dumanganPrefab;
    public GameObject dumakulemPrefab;
    public GameObject[] characterPrefabs;

    [Header("Apolaki")]
    public GameObject canApolaki;
    public GameObject boughtApolaki;

    [Header("Mayari")]
    public GameObject canMayari;
    public GameObject boughtMayari;

    [Header("Dumangan")]
    public GameObject canDumangan;
    public GameObject boughtDumangan;

    [Header("Dumakulem")]
    public GameObject canDumakulem;
    public GameObject boughtDumakulem;
    public void Start()
    {
        currentIndex = PlayerPrefs.GetInt("Bought Units", 0);
        cost = 50 * currentIndex;
        charSelect = FindObjectOfType<CharSelect>();
        archive = FindObjectOfType<UpgradeStats>();
    }

    public void Update()
    {
        if (currentIndex == 4) currentCost.text = "Cost: Maxed";
        else currentCost.text = "Cost: " + cost.ToString();
        currentCoins.text = PlayerPrefs.GetFloat("Coins", 0).ToString();

        CharState("Apolaki");
        CharState("Mayari");
        CharState("Dumangan");
        CharState("Dumakulem");
    }

    public void BuyHero(GameObject playerUnit, string charName)
    {
        float coinCount = PlayerPrefs.GetFloat("Coins", 0);
        if (cost <= coinCount)
        {
            currentIndex++;
            coinCount -= cost;
            cost += 50;

            // Update isBought status in CharStats
            CharStats stats = playerUnit.GetComponent<CharStats>();
            if (stats != null)
            {
                stats.isBought = 1;
                PlayerPrefs.SetInt(charName + "_isBought", stats.isBought);  // Save purchase status
            }

            // Update other PlayerPrefs
            PlayerPrefs.SetInt("Bought Units", currentIndex);
            PlayerPrefs.SetFloat("Coins", coinCount);
            PlayerPrefs.SetFloat("OriginalCoins", coinCount);
            PlayerPrefs.Save();

            CharState(charName);

            Debug.Log(charName + " is bought");
        }
        else
        {
            Debug.Log(charName + " is not bought");
        }
    }
    public void CharacterPrefab(int prefabIndex) 
    {
        string characName = ""; // 0 = Apolaki, 1 = Mayari, 2 = Dumangan, 3 = Dumakulem

        if (prefabIndex == 0) { characName = "Apolaki"; }
        else if (prefabIndex == 1) { characName = "Mayari"; }
        else if (prefabIndex == 2) { characName = "Dumangan"; }
        else if (prefabIndex == 3) { characName = "Dumakulem"; }

        BuyHero(GameManager.Instance.mm_charPrefab[prefabIndex], characName);

        Debug.Log("Picked no " + prefabIndex + ": " + characName);
    }

    public void CharState(string charName)
    {
        int whoChar = PlayerPrefs.GetInt(charName + "_isBought", 0); // 0 is on sale, 1 is bought
        if (whoChar == 0)
        {
            if (charName == "Apolaki") { canApolaki.SetActive(true); boughtApolaki.SetActive(false); }
            else if (charName == "Mayari") { canMayari.SetActive(true); boughtMayari.SetActive(false); }
            else if (charName == "Dumangan") { canDumangan.SetActive(true); boughtDumangan.SetActive(false); }
            else if (charName == "Dumakulem") { canDumakulem.SetActive(true); boughtDumakulem.SetActive(false); }
        }
        else if (whoChar == 1)
        {
            if (charName == "Apolaki") { canApolaki.SetActive(false); boughtApolaki.SetActive(true); }
            else if (charName == "Mayari") { canMayari.SetActive(false); boughtMayari.SetActive(true); }
            else if (charName == "Dumangan") { canDumangan.SetActive(false); boughtDumangan.SetActive(true); }
            else if (charName == "Dumakulem") { canDumakulem.SetActive(false); boughtDumakulem.SetActive(true); }
        }
    }
    public void CanAfford(GameObject confirm)
    {
        float coinCount = PlayerPrefs.GetFloat("Coins", 0);

        if (cost <= coinCount)
        {
            confirm.SetActive(true);
        }
        else
        {
            cantAfford.SetActive(true);
        }
    }

    public void ResetShop()
    {
        foreach (GameObject character in GameManager.Instance.mm_charPrefab)
        {
            CharStats stats = character.GetComponent<CharStats>();

            stats.isBought = 0;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            //charSelect.characterPrefabs.Clear();
        }
    }
}
