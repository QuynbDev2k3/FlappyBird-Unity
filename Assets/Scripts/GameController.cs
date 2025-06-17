using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject pipePrefab;
    public GameObject gameOverCanvas;
    private float countdown;
    private float timeGenerator = 2;
    public bool gameStart;
    public GameObject bird;
    public GameObject message;
    private bool isGameOver = false;
    private int score = 0;
    private int highestScore;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highestScoreText;

    private void Awake()
    {
        if (instance == null) instance = this;
        countdown = 0.1f;
        gameStart = false;
        scoreText.text = score.ToString();
        highestScoreText.text = PlayerPrefs.GetInt("HighestScore", 0).ToString();
        Time.timeScale = 1f;
    }

    private void Start()
    {
        var birdScript = bird.GetComponent<Bird>();
        birdScript.onPassedPipe += IncreaseScore;
        birdScript.onHitObstacle += EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!gameStart)
                {
                    gameStart = true;
                    bird.GetComponent<Bird>().gameStart = true;
                    message.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            if (gameStart)
            {
                PipeGenerator();
            }
        }
        else
        {
            // SceneManager.LoadScene(0);
            score = 0;
        }
    }

    private void PipeGenerator()
    {
        // 30 FPS: Mỗi frame countdown giảm đi 1/30s, 1s = 30 frames thì countdown giảm đi đúng 1
        countdown -= Time.deltaTime; // Mỗi frame countdown -= 1 / FPS
        if (countdown <= 0)
        {
            // Generator pipe tiếp theo, reset countdown và xóa pipe trước đó
            Instantiate(pipePrefab, new Vector3(9.7f, Random.Range(-1.8f, 3), 0),
                Quaternion.identity); // tham số thứ 3 là góc xoay, sử dụng Quanternion.identity để không xoay khi gameobject đã có sẵn
            countdown = timeGenerator;
        }
    }
    
    private void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        // highestScore = score > highestScore ? score : highestScore;
        if (score > PlayerPrefs.GetInt("HighestScore", 0))
        {
            PlayerPrefs.SetInt("HighestScore", score);
            PlayerPrefs.Save();
            highestScoreText.text = PlayerPrefs.GetInt("HighestScore", 0).ToString();
        }
    }


    private void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    private void EndGame()
    {
        gameStart = false;
        isGameOver = true;
        gameOverCanvas.SetActive(true);
        // Invoke(nameof(ReloadScene), 1.5f);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}