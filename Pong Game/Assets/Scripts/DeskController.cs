using UnityEngine;

public class DeskMovement : MonoBehaviour
{
    [SerializeField] private bool isAI;
    [SerializeField] private Transform ball;
    private float speed = 5f;
    
    private void MoveAIdesk()
    {
         if(ball.position.y > transform.position.y)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (ball.position.y < transform.position.y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y,-4f,4f));
    }

    private void MoveDesk() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Time.timeScale == 0f) return;
        transform.position = new Vector3(transform.position.x,mousePosition.y, transform.position.z);

    }

    private void Update()
    {
        if(isAI)
        {
            MoveAIdesk();
        } else { 
        
            MoveDesk();
        }
    }

}
