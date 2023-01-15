using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables for the movement of the player.
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public GameObject gameOverUI;
    public float speed = 5.0f;
    private float powerupStrength = 15.0f;
    public bool hasPowerup = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Makes the player move forward and backward.
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        // Condition that checks if the game is over.
        if (transform.position.y < -9.5f)
        {
            Destroy(gameObject);
            gameOverUI.gameObject.SetActive(true);
            Debug.Log("Game Over!");
        }
    }

    // Method that checks if the player has powerup.
    private void OnTriggerEnter(Collider other)
    {
        // Condition that checks if the player has powerup.
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // Timer for the powerup ability to deplete.
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerupIndicator.gameObject.SetActive(false);
        hasPowerup = false;
    }

    // Method when colliding to the enemy and powerup.
    private void OnCollisionEnter(Collision collision)
    {
        // Condition that checks if the player collide with the powerup and enemy.
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // Knockback the enemy when the player has a powerup.
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
