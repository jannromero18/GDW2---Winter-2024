using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D playerCollider;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;
    private Vector3 _moveDirection;
    private Vector2 _dashDirection;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    [SerializeField] private int _lives;
    private int _score;

    [SerializeField] private float _knockbackForce;

    private void Start()
    {
        InputManager.Init(this);
        InputManager.EnableInGame();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position += _speed * Time.deltaTime * _moveDirection; //player movement

        if (IsGrounded())
        {
            Debug.Log("Player is grounded!");
        }
        else
        {
            Debug.Log("Player is not grounded.");
        }

        Debug.Log("Lives: " + _lives);
        Debug.Log("Score: " + _score);
    }

    public void SetMovementDirection(Vector2 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void Jump()
    {
        if(IsGrounded()) //player can only jump if they are on the ground
        {
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
        
    }

    public void Dash(Vector2 currentDirection)
    {
        _dashDirection = currentDirection;
        rb.AddForce(currentDirection * _dashForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        { 
            return false;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy made contact");
            _lives--;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Debug.Log("Coin collected");
            _score++;
            Destroy(other.gameObject);
        }
    }
}

