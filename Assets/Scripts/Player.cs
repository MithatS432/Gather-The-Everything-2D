using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody2D circle;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI fruitsText;
    private float remainingTime = 5f;
    public int fruitsCollected = 10;
    public int newFruitsCollected;
    public Button restartButton;
    public int score;

    public GameObject biggercircle;
    public AudioClip biggerSound;
    public Camera mainCamera;
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
        if (remainingTime <= 0)
        {
            restartButton.gameObject.SetActive(true);
            restartButton.onClick.AddListener(RestartGame);
            Time.timeScale = 0f;
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
        scoreText.text = "Score: " + score.ToString();
        fruitsCollected--;
        fruitsText.text = "" + fruitsCollected.ToString();
        if (fruitsCollected <= 0)
        {
            remainingTime = 60f;
            newFruitsCollected += 5;
            speed += 1;
            fruitsCollected = newFruitsCollected;
            fruitsText.text = "" + newFruitsCollected.ToString();
            GetBigger();
        }
    }
    public void GetBigger()
    {
        float growthFactor = 0.2f;
        transform.localScale += new Vector3(growthFactor, growthFactor, growthFactor);

        mainCamera.orthographicSize += growthFactor * 0.8f;

        if (biggerSound != null)
            AudioSource.PlayClipAtPoint(biggerSound, transform.position);
        GameObject biggerCircleInstance = Instantiate(biggercircle, transform.position, Quaternion.identity);
        Destroy(biggerCircleInstance, 0.5f);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
