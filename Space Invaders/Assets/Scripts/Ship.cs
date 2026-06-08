using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    private void Update()
    {
        MoveShip();

        if(Input.GetKeyUp(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void MoveShip()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab,bulletSpawn.position, Quaternion.identity);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
        

}
