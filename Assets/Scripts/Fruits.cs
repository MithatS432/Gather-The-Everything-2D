using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int point;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Circle"))
        {
            collision.GetComponent<Player>().GetScore(point);
            Destroy(gameObject);
        }
    }
}
