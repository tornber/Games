using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5;
    public float jumpForce = 5;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioClip footStepSound;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector2 moveInput;
    private PlayerInput playerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = new PlayerInput();
        playerInput.Enable();
        StartCoroutine(PlayFootStep());
    }

    void Update()
    {
        CheckGround();


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void OnJump()
    {
        if(isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void MovePlayer()
    {
        Vector3 directions = transform.right * moveInput.x + transform.forward * moveInput.y;
        directions.Normalize();
        rb.linearVelocity = new Vector3(directions.x * moveSpeed,rb.linearVelocity.y, directions.z * moveSpeed);
    }

    IEnumerator PlayFootStep()
    {
        while (true)
        {
            if (rb.linearVelocity.magnitude > 0.1f && isGrounded)
            {
                Debug.Log(AudioManager.Instance);
                Debug.Log(footStepSound);
                AudioManager.Instance.PlaySFX(footStepSound);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


}
