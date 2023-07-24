using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.5f;
    public TextMeshProUGUI scoreText;
    private int score;
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
}
