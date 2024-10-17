using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isMainMenu;

    [Header("Game HUD")]
    public GameObject[] charSelection;
    public GameObject[] charStatHUD;
    public Slider[] healthSlider;
    public Slider[] skillSlider;
    public TextMeshProUGUI[] charNames;
    public int charCount;
    public TextMeshProUGUI killCountText, coinCountText, waveCountText;
    public float coinCount;

    [Header("Main Menu")]
    public GameObject[] charactersPrefab, charSpawnPoint;
    public GameObject charHolder;

    [Header("Scripts")]
    private SpawnManager spawnManager;

    [Header("Character Objects")]
    public GameObject[] characterPrefabs;  // Same prefabs as in the main menu

    private void Awake()
    {
        if(Instance == null) { Instance = this; } else { Destroy(gameObject); }

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
            OnMainMenuCounts();
        }
        if (!isMainMenu)
        {
            UpdateHealthSliders();
            OnGameCounts();
        }
    }
    public void OnGameCounts()
    {
        coinCountText.text = coinCount.ToString();
        waveCountText.text = spawnManager.waveCount.ToString();
        killCountText.text = spawnManager.killedUnits.ToString();
    }

    public void OnMainMenuCounts()
    {
        coinCount = PlayerPrefs.GetFloat("Coins", 0);
        coinCountText.text = coinCount.ToString();
    }
    public void CharacterSelection()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();
            charSelection[charCount].SetActive(true);
            charStatHUD[charCount].SetActive(true);
            charNames[charCount].text = charStats.playerName;

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

            skillSlider[charCount].maxValue = charStats.skillCooldown;
            skillSlider[charCount].value = charStats.skillTime;

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
