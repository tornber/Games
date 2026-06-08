using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();   
    }

    void Update()
    {
        
    }

    private void LaunchBall()
    {
        bool isRight = Random.value >= 0.5;
        float xVeclocity = -1f;
        float yVelocity = Random.Range(-1,1);
        if (isRight)
        {
            xVeclocity = 1f;
        }
        rb.linearVelocity = new Vector2(xVeclocity * speed, yVelocity * speed);
    }

    public   void ReplaceBall() { 
        
        gameObject.transform.position = Vector2.zero;
        LaunchBall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AIScoreBorder")
        {
            GameManager.Instance.UpdateScore(true);

        }
        else if (collision.gameObject.name == "PlayerScoreBorder")
        {
            GameManager.Instance.UpdateScore(false);
        }
    }
}
