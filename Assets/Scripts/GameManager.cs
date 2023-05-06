using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int initialLives;
    public int brickPoints;  // Number of points given for each brick hitpoint

    void Start()
    {
        Brick.OnBrickDestruction += UpdateScore;

        lives = initialLives;
        gameStarted = false;

        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateLivesText();
    }

    private void UpdateScore(Brick brick)
    {
        score += brick.initialHp * brickPoints;
        UIManager.Instance.UpdateScoreText();
    }

    public void OnBallDeath(Ball obj)
    {
        //determine if ball is gone
        //reduce lives
        //if lives are < 1
        //show game over
        //else 
    }
}
