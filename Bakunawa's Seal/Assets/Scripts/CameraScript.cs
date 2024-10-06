using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject[] playerUnits;
    public GameObject cameraObject;
    public float cameraSpeed;
    public int charSelected;

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
            float distanceToPlayer = Vector3.Distance(transform.position, playerUnits[charSelected].transform.position);

            Vector3 targetPosition = new Vector3(playerUnits[charSelected].transform.position.x, 15f, playerUnits[charSelected].transform.position.z - 3f);

            cameraObject.transform.position = Vector3.Lerp(cameraObject.transform.position, targetPosition, Time.deltaTime * cameraSpeed);
        }
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
         
    }
}
