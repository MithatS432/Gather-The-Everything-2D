using UnityEngine;

public class Fruits : MonoBehaviour
{
    public int point;
    public AudioClip eatSound;

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

                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Player component bulunamadı! Çarpışan obje: " + collision.name);
            }
        }
    }
}
