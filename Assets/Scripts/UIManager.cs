using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }

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

    public TMP_Text levelText;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    public void UpdateLevelText()
    {
        levelText.text = $"Level{Environment.NewLine}{LevelManager.Instance.currentLevel}";
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"Score{Environment.NewLine}{GameManager.Instance.score}";
    }

    public void UpdateLivesText()
    {
        livesText.text = $"Lives{Environment.NewLine}{GameManager.Instance.lives}";
    }
}
