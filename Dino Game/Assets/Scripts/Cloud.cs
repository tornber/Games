using UnityEngine;

public class CLoud : MonoBehaviour
{
    [SerializeField] private float[] RandomSpeed;
    private float currentSpeed = 0;

    private void Start()
    {
        currentSpeed = Random.Range(RandomSpeed[0], RandomSpeed[1]);
    }

    private void Update()
    {
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
