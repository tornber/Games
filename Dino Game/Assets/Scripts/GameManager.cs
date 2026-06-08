using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource dieSound;

    public float score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1.0f;
    }
    
    public void GameOver()
    {
        dieSound.Play();
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        
    }
    private void Update()
    {
        score += Time.deltaTime;
        int sortedScore = (int)score;
        scoreText.text = sortedScore.ToString();
    }
}
