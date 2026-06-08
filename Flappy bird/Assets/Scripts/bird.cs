using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bird : MonoBehaviour
{
    [SerializeField] private float velocity = 1.5f;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {

        // for mobile
        if ( Input.touchCount > 0)
        {
            Touch touch =  Input.GetTouch(0);
            if ( touch.phase == TouchPhase.Ended)
            {
                Jump();
            }
        } 

        // for mouse cursor 
        if ( Input.GetMouseButtonUp(0))
        {
            Jump(); 
        }
    }


    private void Jump()
    {
        animator.SetTrigger("isJump");
        rb.linearVelocity = Vector2.up * velocity;
    }

}
