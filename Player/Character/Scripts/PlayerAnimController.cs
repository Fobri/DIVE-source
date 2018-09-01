using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour {

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.techOpen)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                animator.SetBool("isMoving", true);
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
