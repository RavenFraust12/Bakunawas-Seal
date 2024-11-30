using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;

public class CharSelect : MonoBehaviour
{
    public List<GameObject> characterPrefabs = new List<GameObject>();  // Prefabs of characters available for selection
    public GameObject[] selectedCharacters = new GameObject[4];  // Array to hold up to 4 selected characters
    public int selectedCount = 0;  // To keep track of how many characters have been selected
    public GameObject startButton;
    public Image[] charImage;
    public TextMeshProUGUI[] charName;
    public GameObject buyFirst;
    private bool haveHero = false;

    void Update()
    {
        // Check each character in characterPrefabs and activate the corresponding selectedCharacters slot
        for (int i = 0; i < selectedCharacters.Length; i++)
        {
            CharStats stats = characterPrefabs[i].GetComponent<CharStats>();
            int isBoughtStatus = PlayerPrefs.GetInt(stats.playerName + "_isBought", 0);
            if (isBoughtStatus == 1)
            {
                charImage[i].sprite = stats.charProfile;  // Set the character's image
                charName[i].text = stats.playerName;      // Set the character's name
                selectedCharacters[i]?.SetActive(true);  // Set active if there is a character
                haveHero = true;
            }
            else if(isBoughtStatus == 0/*stats.isBought == 0*/)
            {
                selectedCharacters[i]?.SetActive(false); // Deactivate if no character is selected
            }
        }

        // Enable or disable the start button based on selectedCount
        startButton.SetActive(selectedCount > 0);
        buyFirst.SetActive(!haveHero);
    }

    // Call this function when a character is selected
    public void SelectCharacter(int index)
    {
        // Check if there is room and character is not already selected

        if (index < selectedCharacters.Length)
        {
            CharStats stats = characterPrefabs[index].GetComponent<CharStats>();
            PlayerPrefs.SetInt(stats.playerName + "_Selected",1);
            stats.isSelected = 1;
            selectedCount++;
            Debug.Log("Character selected at index: " + index);
        }
    }

    // Call this function when a character is deselected
    public void DeselectCharacter(int index)
    {
        /*if (index < selectedCharacters.Length && selectedCharacters[index] != null)
        {
            CharStats stats = characterPrefabs[index].GetComponent<CharStats>();

            selectedCharacters[stats.charID] = null;  // Remove the character from selectedCharacters
            selectedCount--;
        }*/
        if (index < selectedCharacters.Length)
        {
            CharStats stats = characterPrefabs[index].GetComponent<CharStats>();
            PlayerPrefs.SetInt(stats.playerName + "_Selected", 0);
            stats.isSelected = 0;
            selectedCount--;
        }
    }

    // Call this function to start the game
    public void StartGame()
    {
        // Ensure at least one character is selected
        if (selectedCount > 0)
        {
            PlayerPrefs.SetInt("SelectedCount", selectedCount);

            int saveIndex = 0;  // Separate index for saving only valid selections

            for (int i = 0; i < selectedCharacters.Length; i++)
            {
                // Save only selected characters that are not null
                if (selectedCharacters[i] != null)
                {
                    int characterIndex = characterPrefabs.IndexOf(selectedCharacters[i]);
                    PlayerPrefs.SetInt("SelectedCharacter_" + saveIndex, characterIndex);
                    saveIndex++;
                }
            }
        }
    }

}
