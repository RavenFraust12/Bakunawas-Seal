using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Drops : MonoBehaviour
{
    public Transform target;
    public float detectionRange;
    public GameManager manager;
    public float speed;

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        FindClosestPlayer();

        Vector3 playerPos = target.position;

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
            manager.coinCount++;
            //PlayerPrefs.SetInt("Coins", +1);
            Destroy(this.gameObject);
        }
    }
}
