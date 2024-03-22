using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Controller : MonoBehaviour
{

    public Animator animator;

    public bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        //running
        if (Input.GetKey(KeyCode.D))
        {
            isFacingRight = true;
            animator.SetBool("Running", true);
        }
        else
            animator.SetBool("Running", false);

        if (Input.GetKey(KeyCode.A))
        {
            isFacingRight = false;
            animator.SetBool("Running_left", true);
        }
        else
            animator.SetBool("Running_left", false);

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && isFacingRight == true)
        {
            animator.SetBool("Jumping", true);
        }
        else
            animator.SetBool("Jumping", false);

        if (Input.GetKeyDown(KeyCode.Space) && isFacingRight == false)
        {
            animator.SetBool("Jumping_left", true);
        }
        else
            animator.SetBool("Jumping_left", false);

        //Dashing
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Dash", true);
        }
        else
            animator.SetBool("Dash", false);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("Dash_left", true);
        }
        else
            animator.SetBool("Dash_left", false);

        //Attacking
        if (Input.GetMouseButtonDown(0) && isFacingRight == true)
        {
            animator.SetBool("Attacking", true);
        }
        else
            animator.SetBool("Attacking", false);

        if (Input.GetMouseButtonDown(0) && isFacingRight == false)
        {
            animator.SetBool("Attacking_left", true);
        }
        else
            animator.SetBool("Attacking_left", false);
    }
}