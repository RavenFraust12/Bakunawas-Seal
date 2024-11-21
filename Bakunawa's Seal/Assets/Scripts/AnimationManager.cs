using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator; //0 = idle, 1 = walk, 2 = attack, 3 = skill, 4 = death
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing from this GameObject.");
        }
    }
    public void PlayIdle()
    {
        animator.SetInteger("AnimState", 0);
    }
    public void PlayWalk()
    {
        animator.SetInteger("AnimState", 1);
    }
    public void PlayAttack()
    {
        animator.SetInteger("AnimState", 2);
    }
    public void PlaySkill()
    {
        animator.SetInteger("AnimState", 3);
    }
    public void PlayDeath()
    {
        animator.SetInteger("AnimState", 4);
    }
}

