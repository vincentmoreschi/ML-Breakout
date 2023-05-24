using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleAgentJL : Agent
{
    // Start is called before the first frame update
    private int bricks; 
    private GameObject ball;
    private GameObject brickContainer;
    private GameObject walls;
    private int episodeCount;
    private Vector3 leftWall;
    private Vector3 rightWall;
    private int currentLives;

    // private Ball target;

    private Brick[] bricksList;

    void Start()
    {
        // BallManager.Instance.CreateBall(target);
        bricks = LevelManager.Instance.RemainingBricks;
        brickContainer = LevelManager.Instance._bricksContainer;
        episodeCount = 0;
        leftWall.Set(-2.8f,-4.2f,0);
        rightWall.Set(2.8f,-4.2f,0);
    }
    // [SerializeField] private Transform ballTransform;
    public override void OnEpisodeBegin(){
        // BallManager.Instance.CreateBall(BallManager.Instance.ballRedPrefab);
        BallManager.Instance.ShootBall();
        GameManager.Instance.gameStarted = true;
        ball = GameObject.Find("Ball Red(Clone)");
        brickContainer = GameObject.Find("Bricks Container");
        bricksList = brickContainer.GetComponentsInChildren<Brick>();
        bricks = LevelManager.Instance.RemainingBricks;
        walls = GameObject.Find("Walls");
        // Debug.Log(episodeCount);
        episodeCount++;
        // Debug.Log(ball);
        // Debug.Log(brickContainer);
        // Debug.Log(bricksList.GetLength(0));
        // Debug.Log(transform.position);
        // Debug.Log(ball.transform.position-transform.position);
        // Debug.Log(leftWall-transform.position);
        // Debug.Log(rightWall-transform.position);
        GameManager.Instance.lives = 5;
        currentLives = GameManager.Instance.lives;

    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        if (GameManager.Instance.lives < currentLives) {
            // Debug.Log("Deathwall Penalty");
            SetReward(-2.0f);
            currentLives = GameManager.Instance.lives;
        }
        if (GameManager.Instance.lives == 1) {
            // Debug.Log("Deathwall Penalty");
            SetReward(-4.0f);
            EndEpisode();
        }
        if (ball != null) {
            sensor.AddObservation(ball.transform.position);
        }
        sensor.AddObservation(transform.position);
        sensor.AddObservation(ball.transform.position-transform.position);
        sensor.AddObservation(leftWall-transform.position);
        sensor.AddObservation(rightWall-transform.position);

        foreach (Brick brick in bricksList) {
            if (brick != null) {
            sensor.AddObservation(brick.transform.position);
            sensor.AddObservation(brick.transform.position-ball.transform.position);
            sensor.AddObservation(brick.transform.position-transform.position);
            }
        }

        if (LevelManager.Instance.RemainingBricks < bricks) {
            for (int i=0;i<bricks-(LevelManager.Instance.RemainingBricks);i++) {
                // Debug.Log("Brick break reward");
                SetReward(2.0f);
            }
            bricks = LevelManager.Instance.RemainingBricks;
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.ContinuousActions[0]);

        float moveX = actions.ContinuousActions[0];

        float moveSpeed = 5f;
        transform.position += new Vector3(moveX,0,0) * Time.deltaTime * moveSpeed;

    }
    // private void OnCollisionEnter2D(Collision2D other) 
    //     {
    //         Debug.Log("paddle hits ball penalty");
    //         SetReward(-0.001f);
    //     }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
    }
}
