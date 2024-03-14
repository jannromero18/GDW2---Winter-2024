using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private int _lives;
    public float knockbackForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Knockback();
            _lives--;
        }
    }

    void Update()
    {
        if (_lives ==  0)
        {
            Destroy(gameObject);
        }
    }

    void Knockback()
    {
        rb.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
    }
}
