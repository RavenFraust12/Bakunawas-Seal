using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator;

    // Define animation parameter names here (based on your Animator Controller)
    private static readonly string IdleAnim = "Idle";
    private static readonly string WalkAnim = "Walk";
    private static readonly string AttackAnim = "Attack";
    private static readonly string DeathAnim = "Death";
    private static readonly string SkillAnim = "Skill";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Ensure the object has an Animator component
        if (animator == null)
        {
            Debug.LogError("Animator component missing from this GameObject.");
        }
    }

    // Call this to trigger idle animation
    public void PlayIdle()
    {
        ResetAllTriggers();
        animator.SetTrigger(IdleAnim);
    }

    // Call this to trigger walk animation
    public void PlayWalk()
    {
       // Debug.Log("Walk animation triggered in AnimationManager");

        //ResetAllTriggers();
        animator.SetTrigger(WalkAnim);
       // Debug.Log("WalkAnim trigger set in Animator.");

    }

    // Call this to trigger attack animation
    public void PlayAttack()
    {
        ResetAllTriggers();
        animator.SetTrigger(AttackAnim);
    }

    // Call this to trigger death animation
    public void PlayDeath()
    {
        ResetAllTriggers();
        animator.SetTrigger(DeathAnim);
    }

    // Call this to trigger skill animation
    public void PlaySkill()
    {
        ResetAllTriggers();
        animator.SetTrigger(SkillAnim);
    }

    // Reset all triggers to avoid conflicting animations
    private void ResetAllTriggers()
    {
        animator.ResetTrigger(IdleAnim);
        animator.ResetTrigger(WalkAnim);
        animator.ResetTrigger(AttackAnim);
        animator.ResetTrigger(DeathAnim);
        animator.ResetTrigger(SkillAnim);
    }
}

