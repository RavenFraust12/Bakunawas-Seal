using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

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

    [Header("Main Slider")]
    public GameObject currentSlider;
    public Image mainHealthSlider;
    public Image mainSkillSlider;
    public Image currentIcon;

    [Header("Defeat Panel")]
    public GameObject losePanel;
    public TextMeshProUGUI finalKillCountText, finalCoinCountText, finalWaveCountText, finalGameTimeText;

    [Header("Main Menu")]
    public GameObject[] mm_charPrefab;
    public CharSelect charSelect;

    [Header("Gameplay")]
    public GameObject[] charSpawnPoint;
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
        Application.targetFrameRate = 60;

        if (isMainMenu)
        {
            PlayerPrefs.DeleteKey("Apolaki_Selected");
            PlayerPrefs.DeleteKey("Mayari_Selected");
            PlayerPrefs.DeleteKey("Dumangan_Selected");
            PlayerPrefs.DeleteKey("Dumakulem_Selected");
        }
        else if (!isMainMenu)
        {
            InstantiateSelectedCharacters();//test
            CharacterSelection();
        }
        UpdateHealthSliders();

        //if (charCount > 0)
        //{
        //    CameraScript cameraScript = FindObjectOfType<CameraScript>();
        //    cameraScript.PickChar(0);  // Automatically follow the first character (index 0)
        //}
    }
    public void Update()
    {
        if (isMainMenu)
        {
            OnMainMenuCounts();
        }
        else if (!isMainMenu)
        {
            UpdateHealthSliders();
            OnGameCounts();
            DeathChecker();
            timer += Time.deltaTime;
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
           // Time.timeScale = 0;
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
            // Calculate minutes and seconds
            TimeSpan timeSpan = TimeSpan.FromSeconds(timer);

            // Format the time as MM:SS
            finalTimer = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
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

        bool isAnyPlayerControlled = false; // Flag to check if any player is controlled

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();

            healthSlider[charCount].fillAmount = charStats.currentHealth / charStats.totalHealth;

            skillSlider[charCount].fillAmount = charStats.skillTime / charStats.skillCooldown;

            charCount++;

            PlayerAI playerAI = player.GetComponent<PlayerAI>();
            if (playerAI.isPlayerControlled)
            {
                isAnyPlayerControlled = true; // Set flag if any player is controlled

                // Update the main sliders with the current character's health/skill if controlled
                mainHealthSlider.fillAmount = charStats.currentHealth / charStats.totalHealth;
                mainSkillSlider.fillAmount = charStats.skillTime / charStats.skillCooldown;
                currentIcon.sprite = charStats.charProfile;
            }
        }

        // Set the slider's active state only after the loop based on the flag
        currentSlider.SetActive(isAnyPlayerControlled);
    }

    private void InstantiateSelectedCharacters()
    {

        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            CharStats stats = characterPrefabs[i].GetComponent<CharStats>();
            int characterIndex = PlayerPrefs.GetInt(stats.playerName + "_Selected", 0);

            if (characterIndex == 1)
            {
                Instantiate(characterPrefabs[i], charSpawnPoint[i].transform.position, Quaternion.identity, charHolder.transform);
            }

            // Retrieve the saved index of each selected character
            /*int characterIndex = PlayerPrefs.GetInt("SelectedCharacter_" + i, -1);
             int selectedCount = PlayerPrefs.GetInt("SelectedCount", 0);  // Number of selected characters


            Instantiate(characterPrefabs[characterIndex], charSpawnPoint[i].transform.position, Quaternion.identity, charHolder.transform);
            Debug.Log("Spawning " + characterPrefabs[characterIndex].name);*/

            // Check if the characterIndex is valid and within bounds
            /*if (characterIndex >= 0 && characterIndex < characterPrefabs.Length)
            {
                if (i < charSpawnPoint.Length)  // Ensure there's a corresponding spawn point
                {
                    // Instantiate the character at the assigned spawn point
                    Instantiate(characterPrefabs[characterIndex], charSpawnPoint[i].transform.position, Quaternion.identity, charHolder.transform);
                    Debug.Log("Spawning " + characterPrefabs[characterIndex].name);
                }
            }*/
        }
    }

}
