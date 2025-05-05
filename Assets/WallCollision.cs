using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager gameManager = GameObject.Find
                ("GameManager").GetComponent<GameManager>();
            gameManager.GameOver();
        }
    }

}
