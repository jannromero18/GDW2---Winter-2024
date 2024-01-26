using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;
    private Vector3 _moveDirection;
    private Vector2 _dashDirection;

    private void Start()
    {
        InputManager.Init(this);
        InputManager.EnableInGame();
    }

    void Update()
    {
        transform.position += _speed * Time.deltaTime * _moveDirection;
    }

    public void SetMovementDirection(Vector2 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void Jump()
    {
         rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void Dash(Vector2 currentDirection)
    {
        _dashDirection = currentDirection;
        rb.AddForce(currentDirection * _dashForce, ForceMode2D.Impulse);
    }
}

