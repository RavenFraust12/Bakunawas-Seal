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

    [Header("Main Menu")]
    public GameObject[] characters;


    public void Start()
    {
        CharacterSelection();
    }
    public void Update()
    {
        
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
