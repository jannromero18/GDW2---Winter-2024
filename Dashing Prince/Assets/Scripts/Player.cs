using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D playerCollider;

    // UI text component to display lives
    public TextMeshProUGUI livesText;
    // UI text component to display coin count
    public TextMeshProUGUI coinText;
    // UI object to display winning text.
    public GameObject deadTextObject;

    public GameObject attack;

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

        // Initially set the win text to be inactive.
        deadTextObject.SetActive(false);
    }

    void Update()
    {
        SetLivesText();

        SetCoinText();

        transform.position += _speed * Time.deltaTime * _moveDirection; //player movement

        if (IsGrounded())
        {
            Debug.Log("Player is grounded!");
        }
        else
        {
            Debug.Log("Player is not grounded.");
        }

        if (_lives > 0)
        {
            Debug.Log("Lives: " + _lives);
        }
        else
        {
            Debug.Log("You died");
        }

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

    public void Attack()
    {
        Debug.Log("Attacked");


        Destroy(Instantiate(attack, rb.position, Quaternion.identity), 0.1f);
        //attack = Instantiate(attack, rb.position, Quaternion.identity);
        //Destroy(attack, 0.1f);
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
            rb.AddForce(-_moveDirection * _knockbackForce, ForceMode2D.Impulse);

            if(_lives > 0)
            {
                _lives--;
            }
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

    void SetLivesText()
    {
        // Update the count text with the current count.
        livesText.text = "Lives: " + _lives.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (_lives <= 0)
        {
            // Display the you died text.
            deadTextObject.SetActive(true);
        }
    }

    void SetCoinText()
    {
        // Update the count text with the current count.
        coinText.text = "Coins: " + _score.ToString();
    }
}

