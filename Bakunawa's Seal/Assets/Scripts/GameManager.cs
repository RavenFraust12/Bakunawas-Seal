using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isMainMenu;

    [Header("Game HUD")]
    public GameObject[] charSelection;
    public Slider[] healthSlider;
    public Slider[] skillSlider;
    public TextMeshProUGUI[] charNames;
    public int charCount;
    public TextMeshProUGUI killCountText, pearlCountText, coinCountText, waveCountText;
    public int coinCount;

    [Header("Main Menu")]
    public GameObject[] charactersPrefab, charSpawnPoint;
    public GameObject charHolder;

    [Header("Scripts")]
    private SpawnManager spawnManager;

    [Header("Test")]
    public GameObject[] characterPrefabs;  // Same prefabs as in the main menu

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        charHolder = GameObject.Find("Character Holder");
    }
    public void Start()
    {
        InstantiateSelectedCharacters();//test
        CharacterSelection();
    }
    public void Update()
    {
        if (isMainMenu)
        {
            coinCount = PlayerPrefs.GetInt("Coins", 1);
        }
        if (!isMainMenu)
        {
            UpdateHealthSliders();
            OnGameCounts();
        }
    }
    public void OnGameCounts()
    {
        coinCountText.text = "Coins: " + coinCount.ToString();
        waveCountText.text = "Wave: " + spawnManager.waveCount.ToString();
        killCountText.text = spawnManager.killedUnits.ToString();
    }
    public void CharacterSelection()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();
            charSelection[charCount].SetActive(true);
            charNames[charCount].text = charStats.playerName;

            //healthSlider[charCount].maxValue = charStats.totalHealth;
            //healthSlider[charCount].value = charStats.currentHealth;

            charCount++;
        }
    }

    private void UpdateHealthSliders()
    {
        charCount = 0;  // Reset character count

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();

            healthSlider[charCount].maxValue = charStats.totalHealth;
            healthSlider[charCount].value = charStats.currentHealth;

            charCount++;
        }
    }

    private void InstantiateSelectedCharacters()
    {
        int selectedCount = PlayerPrefs.GetInt("SelectedCount", 0);

        for (int i = 0; i < selectedCount; i++)
        {
            int characterIndex = PlayerPrefs.GetInt("SelectedCharacter_" + i, 0);

            if (i < charSpawnPoint.Length)  // Ensure there are enough spawn points
            {
                Instantiate(characterPrefabs[characterIndex], charSpawnPoint[i].transform.position, Quaternion.identity, charHolder.transform);
            }

        }
    }
}
