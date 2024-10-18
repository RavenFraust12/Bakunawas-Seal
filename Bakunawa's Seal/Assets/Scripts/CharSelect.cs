using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
    public GameObject[] characterPrefabs;  // Prefabs of characters available for selection
    public GameObject[] selectedCharacters = new GameObject[4];  // Array to hold up to 4 selected characters
    private int selectedCount = 0;  // To keep track of how many characters have been selected
    public GameObject startButton;

    public void Update()
    {
        if (selectedCount > 0) { startButton.SetActive(true); } else { startButton.SetActive(false); }
    }

    // Call this function when a character is selected
    public void SelectCharacter(int index)
    {
        if (selectedCount < selectedCharacters.Length)  // Check if there is room in the array
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
        }
    }

    // Call this function when a character is deselected
    public void DeselectCharacter(int index)
    {
        for (int i = 0; i < selectedCount; i++)
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
        }
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
                PlayerPrefs.SetInt("SelectedCharacter_" + i, System.Array.IndexOf(characterPrefabs, selectedCharacters[i]));
            }

            SceneManager.LoadScene("2 Gameplay");  // Load the gameplay scene
        }
    }
}
