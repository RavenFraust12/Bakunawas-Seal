using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    public List<GameObject> characterPrefabs = new List<GameObject>();  // Prefabs of characters available for selection
    public GameObject[] selectedCharacters = new GameObject[4];  // Array to hold up to 4 selected characters
    private int selectedCount = 0;  // To keep track of how many characters have been selected
    public GameObject startButton;

    void Start()
    {
        PopulateCharacterPrefabs();  // Populate characterPrefabs only once at the start
    }

    public void PopulateCharacterPrefabs()
    {
        characterPrefabs.Clear();  // Clear any existing characters in the list

        foreach (var character in GameManager.Instance.mm_charPrefab)
        {
            CharStats stats = character.GetComponent<CharStats>();

            if (/*stats != null && */stats.isBought == 1)  // Check if the character is bought
            {
                characterPrefabs.Add(character);  // Add to the characterPrefabs list
                Debug.Log(stats.playerName + "is bought. " + stats.isBought);
            }
            else
            {
                Debug.Log(stats.playerName + "is on Sale. " + stats.isBought);
            }
        }
        Debug.Log(characterPrefabs.Count + " characters have been added to characterPrefabs.");
    }

    void Update()
    {
        PopulateCharacterPrefabs();

        // Check each character in characterPrefabs and activate the corresponding selectedCharacters slot
        for (int i = 0; i < selectedCharacters.Length; i++)
        {
            if (i < characterPrefabs.Count && characterPrefabs[i] != null)
            {
                selectedCharacters[i]?.SetActive(true);  // Set active if there is a character
            }
            else
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
        if (index < selectedCharacters.Length && selectedCharacters[index] == null)
        {
            selectedCharacters[index] = characterPrefabs[index];  // Align index in selectedCharacters with characterPrefabs
            selectedCount++;
            Debug.Log("laki etit");
        }
        /*if (selectedCount < selectedCharacters.Length)  // Check if there is room in the array
        {
            bool alreadySelected = false;

            // Check if the character is already selected
            for (int i = 0; i < selectedCount; i++)
            {
                if (selectedCharacters[i] == characterPrefabs[index])
                {
                    alreadySelected = true;
                    break;
                }
            }

            if (!alreadySelected)
            {
                selectedCharacters[selectedCount] = characterPrefabs[index];
                selectedCount++;
            }
        }*/
    }

    // Call this function when a character is deselected
    public void DeselectCharacter(int index)
    {
        if (index < selectedCharacters.Length && selectedCharacters[index] != null)
        {
            selectedCharacters[index] = null;  // Remove the character from selectedCharacters
            selectedCount--;
        }
        /*for (int i = 0; i < selectedCount; i++)
        {
            if (selectedCharacters[i] == characterPrefabs[index])
            {
                // Remove the character by shifting the array elements
                for (int j = i; j < selectedCount - 1; j++)
                {
                    selectedCharacters[j] = selectedCharacters[j + 1];
                }
                selectedCharacters[selectedCount - 1] = null;  // Clear the last slot
                selectedCount--;
                break;
            }
        }*/
    }

    // Call this function to start the game
    public void StartGame()
    {
        if (selectedCount > 0)  // Ensure at least one character is selected
        {
            PlayerPrefs.SetInt("SelectedCount", selectedCount);

            // Save selected character indexes
            for (int i = 0; i < selectedCount; i++)
            {
                PlayerPrefs.SetInt("SelectedCharacter_" + i, characterPrefabs.IndexOf(selectedCharacters[i]));
            }

            SceneManager.LoadScene("2 Gameplay");  // Load the gameplay scene
        }
    }
}