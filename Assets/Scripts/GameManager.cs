using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => GameManager._instance;
    
    private void Awake() {
        if (GameManager._instance != null) {
            Destroy(gameObject);
        } else {
            GameManager._instance = this;
        }
    }

    public int score { get; set; }
    public int lives { get; set; }
    public bool gameStarted { get; set; }
    public GameObject gameOverScreen;

    public int initialLives = 3;
    public int brickPoints;  // Number of points given for each brick hitpoint

    void Start()
    {
        Brick.OnBrickDestruction += UpdateScore;

        this.lives = this.initialLives;
        gameStarted = false;
        Ball.OnBallDeath += OnBallDeath;

        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateLivesText();
        // Debug.Log(gameOverScreen.GetInstanceID());
    }

    private void UpdateScore(Brick brick)
    {
        score += brick.initialHp * brickPoints;
        UIManager.Instance.UpdateScoreText();
    }

    public void OnBallDeath(Ball ball)
    {
        lives--;
        if (lives <= 0)
        {
            gameOverScreen.SetActive(true);
            gameStarted = false;
        }
        else
        {
            UIManager.Instance.UpdateLivesText();

            // Reload level
            BallManager.Instance.ResetBall();

            // Pause the game
            gameStarted = false;
        }
    }

    public void RestartGame() {
        this.lives = initialLives;
        this.score = 0;
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu() {
        this.lives = initialLives;
        this.score = 0;
        LevelManager.Instance.currentLevel = 1;
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }
}
