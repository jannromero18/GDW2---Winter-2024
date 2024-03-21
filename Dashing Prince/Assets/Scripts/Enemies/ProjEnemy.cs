using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjEnemy : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private int _lives;
    public float knockbackForce;

    public GameObject proj;
    public Transform player;

    public float shotCooldown = 3f; // Set the cooldown time (in seconds)
    private float lastShotTime; // Store the time when the last proj was shot

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

    }

    void Update()
    {
        if (_lives == 0)
        {
            Destroy(gameObject);
        }

        if (Time.time > lastShotTime + shotCooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    public void Shoot()
    {
        // Instantiate the projectile at the shooter's position
        GameObject newProjectile = Instantiate(proj, transform.position, Quaternion.identity);

        // Calculate the direction from the shooter to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Set the projectile's velocity (you can adjust the speed as needed)
        float projectileSpeed = 10f; // Example speed
        Rigidbody2D projectileRb = newProjectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = directionToPlayer * projectileSpeed;

    }


    /*  void Knockback()
      {
          if (movingLeft)
          {
              rb.AddForce(Vector2.left * knockbackForce, ForceMode2D.Impulse);
          }
          else
          {
              rb.AddForce(-Vector2.left * knockbackForce, ForceMode2D.Impulse);
          }

      }*/
}
