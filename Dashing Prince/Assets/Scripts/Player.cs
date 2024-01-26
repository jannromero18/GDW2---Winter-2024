using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Vector3 _moveDirection;

    private void Start()
    {
        InputManager.Init(this);
        InputManager.EnableInGame();
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * _moveDirection;
    }

    public void SetMovementDirection(Vector2 currentDirection)
    {
        _moveDirection = currentDirection;
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}

