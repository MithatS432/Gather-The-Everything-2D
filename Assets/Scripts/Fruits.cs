using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int point;
    public AudioSource eatSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Circle"))
        {
            collision.GetComponent<Player>().GetScore(point);
            if (eatSound != null)
            {
                eatSound.Play();
            }
            Destroy(gameObject);
        }
    }
}
