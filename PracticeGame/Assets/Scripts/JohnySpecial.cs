using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JohnySpecial : MonoBehaviour
{
    public GameObject starPrefab; // Reference to the star prefab
    public float moveSpeed = 5f; // Movement speed
    public float boundaryX = 8f; // Boundary limits for movement
    private ScoreMenu scoreManager; // Reference to ScoreManager


    void Start()
    {
        scoreManager = FindObjectOfType<ScoreMenu>();
        InvokeRepeating("SpawnStar", 0f, 3f); // Spawn a star every 3 seconds
    }


    void Update()
    {
        Move();
    }


    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;


        // Apply movement
        transform.position += movement;


        // Clamp position to stay within boundaries
        float clampedX = Mathf.Clamp(transform.position.x, -boundaryX, boundaryX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }


    void SpawnStar()
    {
        // Generate random positions within the defined boundaries
        float randomX = Random.Range(-boundaryX, boundaryX);
        float randomZ = Random.Range(-boundaryX, boundaryX); // Assuming a square area for spawning


        Vector3 spawnPosition = new Vector3(randomX, 1, randomZ); // Adjust the Y position as needed
        Instantiate(starPrefab, spawnPosition, Quaternion.identity);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            scoreManager.AddScore(100); // Increase score by 100
            Destroy(other.gameObject); // Destroy the collected star
        }
    }
}
