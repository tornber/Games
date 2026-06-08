using UnityEngine;

public class DamageObject : MonoBehaviour
{

    [SerializeField] private float speed = 2;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("gmaeOver");
            GameManager.Instance.GameOver();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; // Move the damage object to the left at a constant speed
    }
}
