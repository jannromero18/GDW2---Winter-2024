using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private int _lives;
    public float knockbackForce;

    public GameObject health;

    public float speed = 1.5f;
    bool movingLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
            //Knockback();
            _lives--;
        }
        if (other.gameObject.tag == "Void")
        {
            _lives = 0;
        }

        if (other.gameObject.tag == "WalkSpace")
        {
            movingLeft = !movingLeft;
        }
    }

    void Update()
    {
        Move();

        if (_lives ==  0)
        {
            if (Random.value > 0.5f) // 50% chance
            {
                Instantiate(health, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
            GameOver.score++;
        }
    }

    void Move()
    {
        if (movingLeft)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }

    void Knockback()
    {
        if (movingLeft)
        {
            rb.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(-Vector2.left * knockbackForce, ForceMode2D.Impulse);
        }
        
    }
}
