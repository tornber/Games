using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public GameObject hitUI;
    public GameObject deathUI;
    public TextMeshProUGUI ammoText;
    public Image healthBar;
    public Gradient healthGradient;

    void Awake()
    {
        Time.timeScale = 1f;
        Instance = this;
    }

    public void InstantiateHitUI() {
        Instantiate(hitUI, transform);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Debug.Log("restart game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableDeathUI()
    {
        deathUI.SetActive(true);
    }

    public void SetHealth(int health)
    {
        float floatHealth = (float) health / 100;
        healthBar.color = healthGradient.Evaluate(floatHealth);
        healthBar.fillAmount = floatHealth;
    }

    void Update()
    {
        
    }
}
