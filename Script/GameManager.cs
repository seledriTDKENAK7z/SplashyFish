using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text resultText;
    public GameObject playButton;
    public GameObject flappyBirdTitle;
    public GameObject getReady;
    public GameObject gameOver;

    public int score;
    private int highScore;

    public AudioSource pointSound;
    public AudioSource dieSound;
    public AudioSource playButtonSound;

    private bool isGameOver = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "HIGH SCORE: " + highScore.ToString();
        highScoreText.gameObject.SetActive(false);

        resultText.gameObject.SetActive(false);
        gameOver.SetActive(false);

        Pause();

        flappyBirdTitle.SetActive(true);
        getReady.SetActive(true);
    }

    public void Play()
    {
        playButtonSound.Play();

        score = 0;
        isGameOver = false;

        scoreText.text = score.ToString();

        resultText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        gameOver.SetActive(false);

        playButton.SetActive(false);
        flappyBirdTitle.SetActive(false);
        getReady.SetActive(false);

        Time.timeScale = 1f;

        player.enabled = true;

        player.transform.position = new Vector3(-1f, 0f, 0f);

        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;  
        dieSound.Play();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (score >= 20)
        {
            resultText.text = "YAY CONGRATULATIONS!";
            resultText.gameObject.SetActive(true);

            highScoreText.gameObject.SetActive(false); // disembunyikan
            gameOver.SetActive(false);
        }
        else
        {
            highScoreText.text = "HIGH SCORE: " + highScore.ToString();
            highScoreText.gameObject.SetActive(true);

            gameOver.SetActive(true);
            resultText.gameObject.SetActive(false);
        }

        playButton.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        if (score >= 20 || isGameOver)
            return;

        score++;
        scoreText.text = score.ToString();

        if (score % 5 == 0)
        {
            pointSound.Play();
        }

        if (score == 20)
        {
            GameOver();
        }
    }
}