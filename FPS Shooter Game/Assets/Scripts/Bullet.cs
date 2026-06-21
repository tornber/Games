using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 15;
    public float lifeTime = 3;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = -transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
