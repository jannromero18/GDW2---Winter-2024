using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D playerCollider;
    private Transform playerTransform;

    public SceneAsset gameOver;

    Vector3 spawn = new Vector3(-7.12f, 0.14f, 0f);

    // UI text component to display lives
    public TextMeshProUGUI livesText;

    public GameObject dashTextObject;

    public GameObject attack;

    public float dashCooldown = 3f; // Set the cooldown time (in seconds)
    private float lastDashTime; // Store the time when the ability was last used
    private bool canDash = true;


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
        playerTransform = GameObject.FindWithTag("Player").transform;

        dashTextObject.SetActive(false);

        if (gameOver != null)
        {
            string sceneName = gameOver.name;
            Debug.Log($"Scene name: {sceneName}");
        }
        else
        {
            Debug.LogError("Game Over SceneAsset is not assigned!");
        }
    }

    void Update()
    {
        SetLivesText();

        //SetCoinText();

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

        if (Time.time > lastDashTime + dashCooldown)
        {
            canDash = true;
            dashTextObject.SetActive(true);
        }
        else
        {
            canDash = false;
            dashTextObject.SetActive(false);
        }
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

        if (canDash)
        {
            rb.AddForce(currentDirection * _dashForce, ForceMode2D.Impulse);
            lastDashTime = Time.time;
        }
        
    }

    public void Attack()
    {
        Debug.Log("Attacked");

        if (_moveDirection != null)
        {
            rb.AddForce(_moveDirection * _knockbackForce / 4, ForceMode2D.Impulse);
        }
        else 
        {
            rb.AddForce(Vector2.right * _knockbackForce / 4, ForceMode2D.Impulse);
        }
        

        Destroy(Instantiate(attack, rb.position, Quaternion.identity), 0.1f);
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

        if (other.gameObject.tag == "Void")
        {
            Debug.Log("out of bounds");
            playerTransform.position = spawn;

            if (_lives > 0)
            {
                _lives--;
            }
        }

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy made contact");
            rb.AddForce(-_moveDirection * _knockbackForce, ForceMode2D.Impulse);

            if (_lives > 0)
            {
                _lives--;
            }
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
            LoadGameOverScene(gameOver);
        }
    }

    void SetCoinText()
    {
        // Update the count text with the current count.
        //coinText.text = "Coins: " + _score.ToString();
    }

    public void LoadGameOverScene(SceneAsset scene)
    {
        SceneManager.LoadScene("gameOver");
    }
}

