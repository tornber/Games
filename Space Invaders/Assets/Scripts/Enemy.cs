using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float[] maxShootTimes;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPlace;

    private float timer;
    private float currentmaxShootTime;

    private Animator animator;

    private void Start()
    {
        currentmaxShootTime = maxShootTimes[0];
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    private void Update()
    {
        if (timer > currentmaxShootTime)
        {
            Shoot();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab,bulletSpawnPlace.position, Quaternion.identity);
    }

    public void DestroyEnemy()
    {
        animator.enabled = true;
        GameManager.Instance.UpdateScore();
        Destroy(gameObject, 1f);

    }

}
