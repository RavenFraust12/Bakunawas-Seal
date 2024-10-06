using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game HUD")]
    public GameObject[] charSelection;
    public TextMeshProUGUI[] charNames;
    public int charCount;
    public TextMeshProUGUI killCount, pearlCount, coinCount, waveCount;

    [Header("Main Menu")]
    public GameObject[] characters;

    [Header("Scripts")]
    private SpawnManager spawnManager;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
    }
    public void Start()
    {
        CharacterSelection();
    }
    public void Update()
    {
        OnGameCounts();
    }

    public void OnGameCounts()
    {
        waveCount.text = "Wave: " + spawnManager.waveCount.ToString();
        killCount.text = spawnManager.killedUnits.ToString();
    }
    public void CharacterSelection()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();
            charSelection[charCount].SetActive(true);
            charNames[charCount].text = charStats.playerName;

            charCount++;
        }
    }
}
