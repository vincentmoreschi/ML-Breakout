using Google.Protobuf.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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

    public int initialLives = 3;
    public int brickPoints;  // Number of points given for each brick hitpoint

    public Player[] players { get; set; }  // { human, AI }

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

    void Start()
    {
        // Set up players array.
        // The first index is reserved for the human player.
        // The second index is reversed for the AI player.
        // Elements in the array may be null if only the human or AI is playing.
        players = new Player[2];

        GameObject human = GameObject.Find("Human");
        if (human != null)
        {
            players[0] = human.GetComponent<Player>();
        }

        GameObject AI = GameObject.Find("AI");
        if (AI != null)
        {
            players[1] = AI.GetComponent<Player>();
        }

        // Update UI
        foreach (Player player in players)
        {
            if ( player != null )
            {
                UIManager.Instance.UpdateLevelText(player);
                UIManager.Instance.UpdateScoreText(player);
                UIManager.Instance.UpdateLivesText(player);
            }
        }
    }

    public void OnBallDeath(Ball ball)
    {
        Player player = ball.player;

        player.lives--;
        UIManager.Instance.UpdateLivesText(player);

        if (player.lives <= 0)
        {
            BallManager.Instance.DestroyBalls(player);

            player.gameOverScreen.SetActive(true);
        }
        else
        {
            BallManager.Instance.ResetBall(player);
            //player.paddle.ResetPosition();

            // Pause the game
            player.gameStarted = false;
        }
    }

    private void UpdateScore(Brick brick)
    {
        Player player = brick.player;

        player.score += brick.initialHp * GameManager.Instance.brickPoints;
        UIManager.Instance.UpdateScoreText(player);
    }

    /// <summary>
    /// Reset lives to initialLives and update the UI.
    /// </summary>
    public void ResetLives(Player player)
    {
        player.lives = initialLives;
        UIManager.Instance.UpdateLivesText(player);
    }

    /// <summary>
    /// Reset score to 0 and update the UI.
    /// </summary>
    public void ResetScore(Player player)
    {
        player.score = 0;
        UIManager.Instance.UpdateScoreText(player);
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
