using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _lives;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack")
        {
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
}
