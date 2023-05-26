using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool gameStarted { get; set; }

    public int currentLevel { get; set; }
    public int score { get; set; }
    public int lives { get; set; }

    public Paddle paddle { get; set; }
    public List<Ball> balls { get; set; }
    public int RemainingBricks { get; set; }  // Number of bricks left in the current level
    public GameObject bricksContainer { get; set; }  // Container to hold instantiated bricks

    public TMP_Text levelText;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text finalScoreText;

    public GameObject victoryScreen;
    public GameObject gameOverScreen;

    void Start()
    {
        gameStarted = false;

        currentLevel = LevelManager.Instance.initialLevel;
        score = 0;
        lives = GameManager.Instance.initialLives;

        paddle = transform.Find("Paddle").GetComponent<Paddle>();

        bricksContainer = new GameObject("Bricks Container");
        bricksContainer.transform.SetParent(transform);
    }
}
