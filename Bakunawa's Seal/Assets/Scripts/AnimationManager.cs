using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animator; //0 = idle, 1 = walk, 2 = attack, 3 = skill, 4 = death
    public PlayerAI playerAI;
    public EnemyAI enemyAI;
    private AudioSource audioSource;
    public AudioClip[] sfxClips; //0 = normal attack, 1 = skill
    void Start()
    {
        animator = GetComponent<Animator>();
        playerAI = GetComponentInParent<PlayerAI>();
        enemyAI = GetComponentInParent<EnemyAI>();
        audioSource = GetComponent<AudioSource>();
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

    public void PlayProjectile()
    {
        if (enemyAI != null) enemyAI.ReleaseProjectile();
        if (playerAI != null) playerAI.ReleaseProjectile();
    }

    public void AttackAudio()
    {
        audioSource.volume = AudioManager.instance.sfx.volume;
        audioSource.clip = sfxClips[0];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }

    public void SkillAudio()
    {
        audioSource.volume = AudioManager.instance.sfx.volume;
        audioSource.clip = sfxClips[1];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }
}

