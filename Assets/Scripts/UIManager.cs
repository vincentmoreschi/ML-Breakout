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
    public void UpdateLevelText(Player player)
    {
        player.levelText.text = $"Level{Environment.NewLine}{player.currentLevel}";
    }

    public void UpdateScoreText(Player player)
    {
        player.scoreText.text = $"Score{Environment.NewLine}{player.score}";
    }

    public void UpdateLivesText(Player player)
    {
        player.livesText.text = $"Lives{Environment.NewLine}{player.lives}";
    }

    public void UpdateFinalScoreText(Player player)
    {
        player.finalScoreText.text = $"Final Score{Environment.NewLine}{player.score}";
    }
}
