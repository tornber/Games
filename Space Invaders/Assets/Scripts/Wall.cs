using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private int health = 20;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        healthText.text = health.ToString();
    }

    public void Demage()
    {
        health--;
        healthText.text = health.ToString();
        if( health < 0)
        {
            Destroy(gameObject);
        }
    }

}
