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
    public GameOverScenario gameOverScreen;

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
    }

    private void UpdateScore(Brick brick)
    {
        score += brick.initialHp * brickPoints;
        UIManager.Instance.UpdateScoreText();
    }

    public void OnBallDeath(Ball ball)
    {
        Debug.Log(BallManager.Instance.Balls.Count.ToString());
        if (BallManager.Instance.Balls.Count <= 0) {
            Debug.Log("Balls count is 0");
            //gameStarted = false;
            this.lives--;
            UIManager.Instance.UpdateLivesText();
            if (this.lives < 1) {
                gameOverScreen.showGameOver();
            } else {
                // reload level
                BallManager.Instance.ResetBall();

                //pause the game
                gameStarted = false;

                //reload level if retry option chosen
                LevelManager.Instance.GenerateLevel(LevelManager.Instance.currentLevel);
            }
        }
    }

    public void RestartGame() {
        this.lives = 3;
        this.score = 0;
        // LevelManager.Instance.GenerateLevel(LevelManager.Instance.currentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu() {
        this.lives = 3;
        this.score = 0;
        LevelManager.Instance.currentLevel = 1;
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }
}
