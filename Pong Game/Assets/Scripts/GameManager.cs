using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI AIScoreText;

    [SerializeField] private Ball ballScript;

    [SerializeField] private TextMeshProUGUI playerWinLooseText;
    [SerializeField] private GameObject gameOverCanvas;


    private int playerScore = 0;
    private int AIScore = 0;
    private int maxLimitScore = 11;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1f;
    }

    public void UpdateScore(bool isPlayer1)
    {
        if (isPlayer1)
        {
            playerScore++;
            playerScoreText.text = playerScore.ToString();
        }
        else
        { 
            AIScore++;
            AIScoreText.text = AIScore.ToString();
        }
        ballScript.ReplaceBall();
        if(playerScore >= maxLimitScore || AIScore >= maxLimitScore)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverCanvas.SetActive(true);
        if (playerScore >= maxLimitScore)
        {
            playerWinLooseText.text = "You Win!";

        }
        else
        {
            playerWinLooseText.text = "You Loose!";
        }
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
