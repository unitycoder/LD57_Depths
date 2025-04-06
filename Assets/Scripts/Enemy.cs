using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set the initial velocity of the enemy
        rb.linearVelocity = new Vector2(speed, 0);
    }

}
