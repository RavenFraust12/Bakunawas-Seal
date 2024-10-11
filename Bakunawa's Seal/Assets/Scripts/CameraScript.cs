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
    public int charSelected;

    public NavMeshSurface navMeshSurface;
    public Vector3 navMeshMinBounds;
    public Vector3 navMeshMaxBounds;

    private void Start()
    {
        
    }
    private void Update()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
        CameraLocation();
    }

    public void CameraLocation() //Camera on who to follow
    {
        if (charSelected == 4)
        {
            cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, 15f, cameraObject.transform.position.z);
        }
        else
        {
            Vector3 playerPos = playerUnits[charSelected].transform.position;

            // Set target camera position
            Vector3 targetPosition = new Vector3(playerPos.x, 15f, playerPos.z - 3f);

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

    public void PickChar(int charNumber)//Picking of character for camera to follow
    {
        if (playerUnits[charNumber] != null && charSelected == charNumber)
        {
            charSelected = 4;
        }
        else if (playerUnits[charNumber] != null)
        {
            charSelected = charNumber;
        }
    }
}
