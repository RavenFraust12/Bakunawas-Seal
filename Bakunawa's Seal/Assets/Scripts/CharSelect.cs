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

    void Start()
    {
        //PopulateCharacterPrefabs();  // Populate characterPrefabs only once at the start
    }

    public void PopulateCharacterPrefabs()
    {
        Debug.Log("Something happened");

        int index = 0; // Initialize index to keep track of positions

        // Ensure charImage and charName arrays are large enough to hold all prefabs
        if (charImage.Length < GameManager.Instance.mm_charPrefab.Length)
        {
            charImage = new Image[GameManager.Instance.mm_charPrefab.Length];
        }
        if (charName.Length < GameManager.Instance.mm_charPrefab.Length)
        {
            charName = new TextMeshProUGUI[GameManager.Instance.mm_charPrefab.Length];
        }

        foreach (var character in GameManager.Instance.mm_charPrefab)
        {
            CharStats stats = character.GetComponent<CharStats>();

            if (stats != null && stats.isBought == 1)  // Only process characters that are bought
            {
                // Add character if it’s not already in characterPrefabs
                if (!characterPrefabs.Contains(character))
                {
                    characterPrefabs.Add(character);
                }

                // Assign profile image and name at the current index if within bounds
                if (index < charImage.Length && index < charName.Length)
                {
                    charImage[index].sprite = stats.charProfile;  // Set the character's image
                    charName[index].text = stats.playerName;      // Set the character's name
                }

                index++;  // Increment index after each bought character is processed
                Debug.Log(stats.playerName + " is bought. isBought: " + stats.isBought);
            }
            else if (stats != null)
            {
                Debug.Log(stats.playerName + " is on Sale. isBought: " + stats.isBought);
            }
        }

        Debug.Log(characterPrefabs.Count + " characters have been added to characterPrefabs.");
    }

    void Update()
    {
        //PopulateCharacterPrefabs();

        // Check each character in characterPrefabs and activate the corresponding selectedCharacters slot
        /*for (int i = 0; i < selectedCharacters.Length; i++)
        {
            if (i < characterPrefabs.Count && characterPrefabs[i] != null)
            {
                selectedCharacters[i]?.SetActive(true);  // Set active if there is a character
            }
            else
            {
                selectedCharacters[i]?.SetActive(false); // Deactivate if no character is selected
            }
        }*/

        for (int i = 0; i < selectedCharacters.Length; i++)
        {
            CharStats stats = characterPrefabs[i].GetComponent<CharStats>();

            if (stats.isBought == 1)
            {
                charImage[i].sprite = stats.charProfile;  // Set the character's image
                charName[i].text = stats.playerName;      // Set the character's name
                selectedCharacters[i]?.SetActive(true);  // Set active if there is a character
            }
            else if(stats.isBought == 0)
            {
                selectedCharacters[i]?.SetActive(false); // Deactivate if no character is selected
            }
        }

        // Enable or disable the start button based on selectedCount
        startButton.SetActive(selectedCount > 0);
    }

    // Call this function when a character is selected
    public void SelectCharacter(int index)
    {
        // Check if there is room and character is not already selected
        /*if (index < selectedCharacters.Length)
        {
            CharStats stats = characterPrefabs[index].GetComponent<CharStats>();

            selectedCharacters[stats.charID] = characterPrefabs[index];  // Align index in selectedCharacters with characterPrefabs
            selectedCount++;
            Debug.Log("Character selected at index: " + index);
        }*/

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

            SceneManager.LoadScene("2 Gameplay");  // Load the gameplay scene
        }
    }

}
