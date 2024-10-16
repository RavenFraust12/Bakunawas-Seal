using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPandSkillsHUD : MonoBehaviour
{
    public Slider[] healthSlider;
    public Slider[] skillSlider;

    public GameManager manager;

    public void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void CharacterHealth()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();
            manager.charSelection[manager.charCount].SetActive(true);
            manager.charNames[manager.charCount].text = charStats.playerName;

            manager.charCount++;
        }
    }
}
