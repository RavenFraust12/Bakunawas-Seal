using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isMainMenu;

    [Header("Game HUD")]
    public GameObject[] charSelection;
    public GameObject[] charStatHUD;
    public Image[] healthSlider;
    public Image[] skillSlider;
    public TextMeshProUGUI[] charNames;
    public int charCount;
    public TextMeshProUGUI killCountText, coinCountText, waveCountText, gameTimeText;
    public float coinCount;
    public float timer;
    public string finalTimer;
    public bool allDead = false;

    [Header("Defeat Panel")]
    public GameObject losePanel;
    public TextMeshProUGUI finalKillCountText, finalCoinCountText, finalWaveCountText, finalGameTimeText;

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
            DeathChecker();
        }
    }

    public void DeathChecker()
    {
        allDead = true;  // Assume all are dead initially

        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();

            if (!charStats.isDead)
            {
                allDead = false;
            }
        }

        if(allDead)
        {
            losePanel.SetActive(true);
            finalCoinCountText.text = coinCount.ToString();
            finalWaveCountText.text = spawnManager.waveCount.ToString();
            finalKillCountText.text = spawnManager.killedUnits.ToString();
            finalGameTimeText.text = finalTimer;
        }
    }
    public void OnGameCounts()
    {
        coinCountText.text = coinCount.ToString();
        waveCountText.text = spawnManager.waveCount.ToString();
        killCountText.text = spawnManager.killedUnits.ToString();
    }

    public void Timer()
    {
        if (!allDead)
        {
            // Increment the timer by the time passed since the last frame
            timer += Time.deltaTime;

            // Calculate minutes and seconds
            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

            // Format the time as MM:SS
            finalTimer = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

            // Update the clockText UI
            //finalGameTimeText.text = timeText;
        }   
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

            Image charImage = charSelection[charCount].GetComponent<Image>();
            charImage.sprite = charStats.charProfile;

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

            healthSlider[charCount].fillAmount = charStats.currentHealth / charStats.totalHealth;

            skillSlider[charCount].fillAmount = charStats.skillTime / charStats.skillCooldown;

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
