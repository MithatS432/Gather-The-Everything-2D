using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D circle;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI fruitsText;
    public TextMeshProUGUI speedBoostText;
    private bool isSpeedBoosted = false;
    private float remainingTime = 60f;
    public int fruitsCollected = 10;
    public int newFruitsCollected;
    public Button restartButton;
    public int score;
    public int leftFruits = 500;
    public AudioClip winsound;
    public GameObject winEffect;
    public TextMeshProUGUI leftFruitsText;

    public GameObject biggercircle;
    public AudioClip biggerSound;
    public Camera mainCamera;
    [SerializeField] private float speed;

    private bool isGameOver = false;
    private bool hasWon = false;
    private int speedBoostDuration = 3;
    private bool isFirstSpeedBoost = true;

    private bool isDoubleScore = false;

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
            if (!isGameOver)
            {
                GameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BoostSpeed();
        }

        if (leftFruits <= 0 && !hasWon)
        {
            TriggerWin();
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
        if (isDoubleScore)
            amount *= 2;

        score += amount;
        scoreText.text = "Score: " + score.ToString();

        if (score >= 1000 && isFirstSpeedBoost)
        {
            isFirstSpeedBoost = false;
            speedBoostDuration += 3;
            speedBoostText.text = "X" + speedBoostDuration.ToString();
        }

        fruitsCollected--;
        leftFruits--;
        fruitsText.text = "" + fruitsCollected.ToString();
        leftFruitsText.text = "Left: " + leftFruits.ToString();

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
        float growthFactor = 0.1f;
        transform.localScale += new Vector3(growthFactor, growthFactor, growthFactor);

        mainCamera.orthographicSize += growthFactor * 0.8f;

        if (biggerSound != null)
            AudioSource.PlayClipAtPoint(biggerSound, transform.position);

        GameObject biggerCircleInstance = Instantiate(biggercircle, transform.position, Quaternion.identity);
        Destroy(biggerCircleInstance, 0.5f);
    }

    public void GameOver()
    {
        isGameOver = true;
        restartButton.gameObject.SetActive(true);
        restartButton.onClick.AddListener(RestartGame);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BoostSpeed()
    {
        if (speedBoostDuration > 0 && !isSpeedBoosted)
        {
            isSpeedBoosted = true;
            speedBoostDuration--;
            speedBoostText.text = "X" + speedBoostDuration.ToString();
            StartCoroutine(SpeedBoostCoroutine());
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        float originalSpeed = speed;
        speed *= 2;
        yield return new WaitForSeconds(3f);
        speed = originalSpeed;
        isSpeedBoosted = false;
    }

    void TriggerWin()
    {
        hasWon = true;

        if (winsound != null)
            AudioSource.PlayClipAtPoint(winsound, transform.position);

        if (winEffect != null)
        {
            GameObject effect = Instantiate(winEffect, transform.position, Quaternion.identity);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                var main = ps.main;
                main.useUnscaledTime = true;
            }
        }

        Time.timeScale = 0f;
        StartCoroutine(ReturnToMenuAfterDelay(5f));
    }

    private IEnumerator ReturnToMenuAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Time"))
        {
            remainingTime += 10f;
            AudioSource.PlayClipAtPoint(biggerSound, transform.position);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("X2 Score"))
        {
            StartCoroutine(ScoreBoostCoroutine());
            Destroy(other.gameObject);
        }
    }

    private IEnumerator ScoreBoostCoroutine()
    {
        isDoubleScore = true;
        scoreText.text = "DOUBLE SCORE!";
        yield return new WaitForSecondsRealtime(3f);
        isDoubleScore = false;
        scoreText.text = "Score: " + score.ToString();
    }
}
