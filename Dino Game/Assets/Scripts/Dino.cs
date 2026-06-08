using UnityEngine;

public class Dino : MonoBehaviour
{

    [SerializeField] private float velocity = 3f; // Speed of the dinosaur movement 

    [SerializeField] private AudioSource jumpSound;

    private Rigidbody2D rb;

    private Animator anim;

    private BoxCollider2D boxCollider;

    private float defY = 0;

    private bool isGrounded = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the dinosaur
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        defY = boxCollider.size.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Ground") isGrounded = true;
    }

    private void onColiisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGrounded = false;
    }

    private void Jump()
    {
        if (!isGrounded) return; // Check if the dinosaur is on the ground before allowing it to jump
        jumpSound.Play();   
        rb.linearVelocity = Vector2.up * velocity;
        isGrounded = false; // Set isGrounded to false when the dinosaur jumps
    }

    private void DownEvent(bool changed)
    {
        anim.SetBool("isDown", changed);
        boxCollider.size = new Vector2(boxCollider.size.x, changed ? defY - 0.20f : defY);
        if (changed)
        {
            rb.linearVelocity = Vector2.down * (velocity / 2);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump(); // Call the Jump method when the space bar or up arrow key is pressed)  
            }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownEvent(true); // Call the DownEvent method with true when the down arrow key is pressed
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DownEvent(false); // Call the DownEvent method with false when the down arrow key is released)
        }
    }
}
