using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
