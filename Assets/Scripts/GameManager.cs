using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public int score { get; set; }
    public int lives { get; set; }
    public bool gameStarted { get; set; }

    public GameObject gameOverScreen;
    public GameObject victoryScreen;

    public int initialLives = 3;
    public int brickPoints;  // Number of points given for each brick hitpoint

    void Start()
    {
        lives = initialLives;
        gameStarted = false;

        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateLivesText();
        // Debug.Log(gameOverScreen.GetInstanceID());
    }

    private void OnEnable()
    {
        Brick.OnBrickDestruction += UpdateScore;
        Ball.OnBallDeath += OnBallDeath;
    }

    private void OnDisable()
    {
        // Necessary for re-loading game scenes
        Brick.OnBrickDestruction -= UpdateScore;
        Ball.OnBallDeath -= OnBallDeath;
    }

    public void OnBallDeath(Ball ball)
    {
        lives--;
        UIManager.Instance.UpdateLivesText();

        if (lives <= 0)
        {
            BallManager.Instance.DestroyBalls();

            gameOverScreen.SetActive(true);
        }
        else
        {
            BallManager.Instance.ResetBall();

            // Pause the game
            gameStarted = false;
        }
    }

    private void UpdateScore(Brick brick)
    {
        score += brick.initialHp * brickPoints;
        UIManager.Instance.UpdateScoreText();
    }

    /// <summary>
    /// Reset lives to initialLives and update the UI.
    /// </summary>
    public void ResetLives()
    {
        lives = initialLives;
        UIManager.Instance.UpdateLivesText();
    }

    /// <summary>
    /// Reset score to 0 and update the UI.
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        UIManager.Instance.UpdateScoreText();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
