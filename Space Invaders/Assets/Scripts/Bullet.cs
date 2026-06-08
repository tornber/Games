using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private bool hasAlien;   

    private void Update()
    {
        MoveBullet();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject,2f);
    }

    private void MoveBullet()
    {
        if (hasAlien)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        } else
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Wall wallScript = collision.gameObject.GetComponent<Wall>();
            wallScript.Demage();
            Destroy(gameObject);
        }
        if( collision.gameObject.CompareTag("Enemy") )
        {
            if(hasAlien) return;
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            enemyScript.DestroyEnemy();
            Destroy(gameObject);
        }
        if(hasAlien && collision.gameObject.CompareTag("Player"))
        {
            Ship shipScript = collision.gameObject.GetComponent<Ship>();
            shipScript.Death();
            Destroy(gameObject);
        }
    }
}
