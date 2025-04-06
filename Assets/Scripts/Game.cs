using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject bubblePrefab;
    public GameObject enemyPrefab;

    Camera cam;

    public Rigidbody2D playerRb;
    public Transform spawnForceArea;

    bool hasStarted = false;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(SpawnBubbles());
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        //if (hasStarted == false && Input.GetKeyDown(KeyCode.Space))
        //{
        //    hasStarted = true;
        //    playerRb.AddForceAtPosition(-Vector2.one, ((Vector2)spawnForceArea.position + Random.insideUnitCircle* 0.26461f), ForceMode2D.Impulse);
        //    Debug.DrawRay(spawnForceArea.position, Random.insideUnitCircle * 0.26461f, Color.red, 5f);
        //}

    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            // Generate a random position within the screen bounds
            float x = Random.Range(0, Screen.width);
            float y = 0;
            Vector3 spawnPosition = new Vector3(x, y, 0);
            // to worldpos
            spawnPosition = cam.ScreenToWorldPoint(spawnPosition);
            spawnPosition.z = 0;

            // Instantiate the bubble prefab at the random position
            var go = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
            var rb = go.GetComponent<Rigidbody2D>();
            // Set the bubble's velocity to move it upwards
            rb.gravityScale = -Random.Range(0.02f, 0.03f);

            //Destroy(go, 5f); // Destroy the bubble after 5 seconds
            // Wait for a short duration before spawning the next bubble
            yield return new WaitForSeconds(Random.Range(1f, 10f));
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(-11, Random.Range(-1.4f, 3f), 0);

            // Instantiate the bubble prefab at the random position
            var go = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            var enemy = go.GetComponent<Enemy>();
            enemy.speed = Random.Range(0.09f, 0.15f);

            //Destroy(go, 5f); // Destroy the bubble after 5 seconds
            // Wait for a short duration before spawning the next bubble
            yield return new WaitForSeconds(Random.Range(20f, 60*2f));
        }
    }

}
