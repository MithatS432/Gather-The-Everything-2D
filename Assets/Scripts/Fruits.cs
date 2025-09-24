using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int point;
    public AudioClip eatSound;
    public GameObject fruitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Circle"))
        {
            Player player = collision.GetComponentInParent<Player>();

            if (player != null)
            {
                player.GetScore(point);

                if (eatSound != null)
                {
                    AudioSource.PlayClipAtPoint(eatSound, transform.position);
                }

                if (fruitEffect != null)
                {
                    GameObject effect = Instantiate(fruitEffect, transform.position, Quaternion.identity);
                    Destroy(effect, 1f);
                }

                Destroy(gameObject);
            }
        }
    }
}
