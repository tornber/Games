using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public AudioClip hitSFX;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damage") {
            DecreaseHealth(10);

        }
        if (collision.gameObject.tag == "GlockDamage") {
            DecreaseHealth(5);
        }

    }

    private void DecreaseHealth(int minusHealth)
    {
        health -= minusHealth;
        PlayerLook.Instance.AddShake(0.1f, 0.25f);
        UIManager.Instance.InstantiateHitUI();
        AudioManager.Instance.PlaySFX(hitSFX);
        UIManager.Instance.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Time.timeScale = 0f;
        UIManager.Instance.EnableDeathUI();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
