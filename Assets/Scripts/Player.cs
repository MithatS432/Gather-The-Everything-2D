using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D circle;
    public TextMeshProUGUI timerText;
    private float remainingTime = 60f;
    public int fruitsCollected = 10;
    public int newFruitsCollected;
    public int score;
    [SerializeField] private float speed;
    void Start()
    {
        circle = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "00:00";
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movepos = new Vector2(x, y);
        transform.Translate(movepos * speed * Time.deltaTime);
    }
    public void GetScore(int amount)
    {
        score += amount;
        fruitsCollected--;
        if (fruitsCollected <= 0)
        {
            newFruitsCollected += 5;
            speed += 1;
            fruitsCollected = newFruitsCollected;
        }
    }
}
