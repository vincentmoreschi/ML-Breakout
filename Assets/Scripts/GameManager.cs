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

    public GameObject victoryScreen;

    public int initialLives = 3;
    public int brickPoints;  // Number of points given for each brick hitpoint
    public int currentLevel;

    void Start()
    {
        Brick.OnBrickDestruction += UpdateScore;

        this.lives = this.initialLives;
        this.currentLevel = LevelManager.Instance.currentLevel;
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
        // Debug.Log(BallManager.Instance.Balls.Count.ToString());
        // if (BallManager.Instance.Balls.Count <= 0) {
        //     Debug.Log("Balls count is 0");
        //     //gameStarted = false;
            if(currentLevel == LevelManager.Instance.currentLevel) {
                this.lives--;
                UIManager.Instance.UpdateLivesText();
            } else {
                currentLevel = LevelManager.Instance.currentLevel;
            }

            // if (gameOverScreen == null){
            //     Debug.Log("Creating gameoverscreen instance");
            //     gameOverScreen = GameOverScenario.Instance;
            //     Debug.Log(gameOverScreen.GetInstanceID());
            // }

            if (this.lives <= 0) {
                gameOverScreen.SetActive(true);
                gameStarted = false;
            } else {
                // reload level
                BallManager.Instance.ResetBall();

                //pause the game
                gameStarted = false;

                //reload level if retry option chosen
                // LevelManager.Instance.GenerateLevel(LevelManager.Instance.currentLevel);
            }
        // }
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
