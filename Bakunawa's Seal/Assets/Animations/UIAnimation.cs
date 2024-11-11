using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public Animator animator;
    public GameObject exitScene;
    public GameObject returnScene;

    public void ShowScene()
    {
        animator.SetTrigger("Show");
    }
        
    public void ExitScene()
    {
        animator.SetTrigger("Exit");
    }

    public void ExitAnimation()
    {
        exitScene.SetActive(false);
    }
    public void ReturnScene()
    {
        returnScene.SetActive(true);
    }
}
