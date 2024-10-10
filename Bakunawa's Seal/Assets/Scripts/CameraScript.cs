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
        var bounds = navMeshSurface.navMeshData.sourceBounds;
        navMeshMinBounds = bounds.min;
        navMeshMaxBounds = bounds.max;
    }
    private void Update()
    {
        playerUnits = GameObject.FindGameObjectsWithTag("Player");
        CameraLocation();

    }

    public void CameraLocation()
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
            //float distanceToPlayer = Vector3.Distance(transform.position, playerUnits[charSelected].transform.position);

            //Vector3 targetPosition = new Vector3(playerUnits[charSelected].transform.position.x, 15f, playerUnits[charSelected].transform.position.z - 3f);

            //cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
        }
    }
    private Vector3 ClampCameraToNavMesh(Vector3 cameraPos)
    {
        float clampedX = Mathf.Clamp(cameraPos.x, navMeshMinBounds.x, navMeshMaxBounds.x);
        float clampedZ = Mathf.Clamp(cameraPos.z, navMeshMinBounds.z, navMeshMaxBounds.z);

        // Return the clamped position while keeping the Y-axis as it is
        return new Vector3(clampedX, cameraPos.y, clampedZ);
    }
    public void FirstChar()
    {
        if (playerUnits[0] != null && charSelected == 0)
        {
            charSelected = 4;
        }
        else if (playerUnits[0] != null)
        {
            charSelected = 0;
        }
    }

    public void SecondChar()
    {
        if (playerUnits[1] != null && charSelected == 1)
        {
            charSelected = 4;
        }
        else if (playerUnits[1] != null)
        {
            charSelected = 1;
        }
    }
    public void ThirdChar()
    {
        if (playerUnits[2] != null && charSelected == 2)
        {
            charSelected = 4;
        }
        else if(playerUnits[2] != null)
        {
            charSelected = 2;
        }
        else if (playerUnits[2] == null)
        {
            Debug.Log("No Third Character");
        }
         
    }
    public void FourthChar()
    {
        if (playerUnits[3] != null && charSelected == 3)
        {
            charSelected = 4;
        }
        else if(playerUnits[3] != null)
        {
            charSelected = 3;
        }
        else if (playerUnits[3] == null)
        {
            Debug.Log("No Fourth Character");
        }

    }
}
