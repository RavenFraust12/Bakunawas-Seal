using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Drops : MonoBehaviour
{
    public Transform target;
    public float detectionRange;
    public float speed;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        FindClosestPlayer();

        Vector3 playerPos = target.position;

        audioSource.volume = AudioManager.instance.sfx.volume;

        if (Vector3.Distance(transform.position, target.position) <= detectionRange)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * speed);
        }
    }
    void FindClosestPlayer()
    {
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("Player");

        float closestDistance = Mathf.Infinity;
        GameObject closestPlayer = null;

        foreach (GameObject player in playerUnits)
        {
            CharStats charStats = player.GetComponent<CharStats>();
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance && charStats.currentHealth > 0)
            {
                closestDistance = distanceToPlayer;
                closestPlayer = player;
            }
        }
        if (closestPlayer != null)
        {
            target = closestPlayer.transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.coinCount++;
            float currentCoins = PlayerPrefs.GetFloat("Coins", 0);
            PlayerPrefs.SetFloat("Coins", currentCoins + 1);
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(this.gameObject, 1);
        }
    }
}
