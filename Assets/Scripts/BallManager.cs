using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private static BallManager _instance;
    public static BallManager Instance => BallManager._instance;

    private void Awake() {
        if (BallManager._instance != null) {
            Destroy(gameObject);
        } else {
        BallManager._instance = this;
        }
    }
    public List<Ball> Balls {get; set;}

    void Start() {
        InitBall();
    }

    private void InitBall() {
        Vector3 startingPosition = new Vector3();
    }
}
