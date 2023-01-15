using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables for the enemy of the player.
    public float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the enemy follow the player.
        Vector3 loookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(loookDirection * speed);

        // Destroy the enemies when it fall off.
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
