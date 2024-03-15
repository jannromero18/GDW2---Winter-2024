using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Controller : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Running", true);
        }
        else
            animator.SetBool("Running", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jumping", true);
        }
        else
            animator.SetBool("Jumping", false);
    }
}