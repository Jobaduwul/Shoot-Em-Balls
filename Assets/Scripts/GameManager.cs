using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 0.65f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI highScoreText;
    private int score;
    public int lives;
    private int highScore;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public AudioClip ballSound;
    public AudioClip bombSound;
    public AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen.gameObject.SetActive(true);

        //associate player prefs HighScore to highScore and set default value to 0
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //coroutine to spawn targets
    IEnumerator SpawnTarget()
    {
        //condition to spawn targets
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    //method to update score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if(score < 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;

        //use playerprefs to store data from score variable, highscore is key name
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            UpdateHighScore();
        }
    }

    //method to decrease lives
    public void UpdateLives()
    {
        lives--;
        if(lives < 0)
        {
            lives = 0;
        }
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }

    //method will be added to list of OnCick of restart button
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true; //need to ensure game is active before spawning can occur
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        livesText.text = "Lives: " + lives; //show initial number of lives
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void PlayBallSound()
    {
        playerAudio = gameObject.GetComponent<AudioSource>();
        playerAudio.PlayOneShot(ballSound);
    }
    public void PlayBombSound()
    {
        playerAudio = gameObject.GetComponent<AudioSource>();
        playerAudio.PlayOneShot(bombSound);
    }

    public void UpdateHighScore()
    {
        PlayerPrefs.SetInt("HighScore", score);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }
}
