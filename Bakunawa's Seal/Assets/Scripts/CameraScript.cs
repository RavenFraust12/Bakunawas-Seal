using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject[] playerUnits;
    public GameObject cameraObject;
    public float cameraSpeed;
    public float panSpeed;
    public int charSelected;

    public NavMeshSurface navMeshSurface;
    public Vector3 navMeshMinBounds;
    public Vector3 navMeshMaxBounds;

    public GameObject joystick;
    private void Start()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
        /*if (playerUnits.Length > 0)
        {
            charSelected = 0; // Automatically select the first character
            joystick.SetActive(true); // Enable joystick for player control
        }*/
    }
    private void Update()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
        CameraLocation();
        CameraPanningMovement();
    }

    public void CameraLocation() //Camera on who to follow
    {
        if (charSelected == 4) //Not Selected
        {
            cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, 15f, cameraObject.transform.position.z);
        }
        else //Selected
        {
            Vector3 playerPos = playerUnits[charSelected].transform.position;

            // Set target camera position
            Vector3 targetPosition = new Vector3(playerPos.x, 15f, playerPos.z - 10f);

            // Lerp the camera position
            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, targetPosition, Time.deltaTime * cameraSpeed);

            // Clamp the camera position within the NavMesh bounds
            cameraObject.transform.position = ClampCameraToNavMesh(cameraObject.transform.position);  
        }
    }
    private Vector3 ClampCameraToNavMesh(Vector3 cameraPos) //Camera Bounds
    {
        float clampedX = Mathf.Clamp(cameraPos.x, navMeshMinBounds.x, navMeshMaxBounds.x);
        float clampedZ = Mathf.Clamp(cameraPos.z, navMeshMinBounds.z, navMeshMaxBounds.z);

        // Return the clamped position while keeping the Y-axis as it is
        return new Vector3(clampedX, cameraPos.y, clampedZ);
    }
    public void PickChar(int charNumber) // Picking a character for the camera to follow
    {
        if (playerUnits[charNumber] != null && charSelected == charNumber)
        {
            // Deselect the currently selected character
            playerUnits[charNumber].GetComponent<PlayerAI>().isPlayerControlled = false;
            joystick.SetActive(false);
            GameManager.Instance.currentSlider.SetActive(false);
            charSelected = 4; // Reset the selected character index
        }
        else if (playerUnits[charNumber] != null)
        {
            if (playerUnits[charNumber].GetComponent<CharStats>().isDead == false)
            {
                // Set all other characters' isPlayerControlled to false
                foreach (var unit in playerUnits)
                {
                    if (unit != null)
                    {
                        unit.GetComponent<PlayerAI>().isPlayerControlled = false;
                    }
                }

                // Set the selected character to controlled
                playerUnits[charNumber].GetComponent<PlayerAI>().isPlayerControlled = true;

                // Update the main sliders with the current character's health/skill if controlled
                CharStats charStats = playerUnits[charNumber].GetComponent<CharStats>();
                GameManager.Instance.mainHealthSlider.fillAmount = charStats.currentHealth / charStats.totalHealth;
                GameManager.Instance.mainSkillSlider.fillAmount = charStats.skillTime / charStats.skillCooldown;
                GameManager.Instance.currentIcon.sprite = charStats.charProfile;

                GameManager.Instance.currentSlider.SetActive(true);
            }
            else
            {
                GameManager.Instance.currentSlider.SetActive(false);
            }
            charSelected = charNumber; // Update the selected character index
            joystick.SetActive(true);  // Enable the joystick
        }
    }

    public void CameraPanningMovement()
    {
        if (charSelected == 4)
        {
            if (Input.touchCount == 1) // Ensure there is exactly one touch
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved) // Detect swipe movement
                {
                    // Scale down delta to control the movement speed
                    Vector2 delta = touch.deltaPosition * panSpeed * Time.deltaTime;

                    // Calculate the movement on XZ plane (Y stays constant)
                    Vector3 move = new Vector3(-delta.x, 0, -delta.y);

                    // Apply the movement to the camera's position
                    Vector3 newPos = cameraObject.transform.position + move;

                    //ClampCameraToNavMesh(newPos);

                    // Clamp the new position to the defined limits
                    newPos.x = Mathf.Clamp(newPos.x, navMeshMinBounds.x, navMeshMaxBounds.x);
                    newPos.z = Mathf.Clamp(newPos.z, navMeshMinBounds.z, navMeshMaxBounds.z);

                    cameraObject.transform.position = newPos;  // Apply the clamped position
                }
            }
        }     
    }
}
