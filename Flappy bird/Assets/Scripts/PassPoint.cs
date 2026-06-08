using UnityEngine;

public class EnterPoint : MonoBehaviour
{

    [SerializeField] private GameObject gameOverCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.UpdateScore();
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
