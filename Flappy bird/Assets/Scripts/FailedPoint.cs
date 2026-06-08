using UnityEngine;

public class FailedPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
}
