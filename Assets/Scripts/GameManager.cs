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

    public bool hasGameStarted { get; set;}

}