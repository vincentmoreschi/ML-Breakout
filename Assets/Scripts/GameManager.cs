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

    void Start()
    {
        Brick.OnBrickDestruction += LevelCompletion;
    }

    private void LevelCompletion()
    {
        if (LevelManager.Instance.CheckLevelCompletion())
        {
            if (LevelManager.Instance.CheckFinalLevel())
            {
                // TODO: show victory scene
            }
            else
            {
                // TODO: level change visual?
                LevelManager.Instance.GenerateNextLevel();
            }
        }
    }

    public bool hasGameStarted { get; set;}

}
