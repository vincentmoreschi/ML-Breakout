using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleAgent : Agent
{
    // Start is called before the first frame update
    private int bricks; 
    private GameObject ball;
    private GameObject brickContainer;
    private GameObject walls;
    private int episodeCount;

    // private Ball target;

    private Brick[] bricksList;

    void Start()
    {
        // BallManager.Instance.CreateBall(target);
        bricks = LevelManager.Instance.RemainingBricks;
        brickContainer = LevelManager.Instance._bricksContainer;
        episodeCount = 0;
    }
    // [SerializeField] private Transform ballTransform;
    public override void OnEpisodeBegin(){
        GameManager.Instance.gameStarted = true;
        BallManager.Instance._ballRb.AddForce(new Vector2(0, BallManager.Instance.ballStartForce));
        ball = GameObject.Find("Ball Red(Clone)");
        brickContainer = GameObject.Find("Bricks Container");
        bricksList = brickContainer.GetComponentsInChildren<Brick>();
        bricks = LevelManager.Instance.RemainingBricks;
        walls = GameObject.Find("Walls");
        Debug.Log(episodeCount);
        episodeCount++;
        Debug.Log(ball);
        Debug.Log(brickContainer);
        Debug.Log(bricksList.GetLength(0));
        Debug.Log(ball.transform.position-transform.position);
        Debug.Log(walls.transform.position-transform.position);
        GameManager.Instance.lives = 3;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        if (ball == null) {
            Debug.Log("Deathwall Penalty");
            SetReward(-2.0f);
            EndEpisode();
        }

        sensor.AddObservation(ball.transform.position);
        sensor.AddObservation(transform.position);
        sensor.AddObservation(ball.transform.position-transform.position);
        sensor.AddObservation(walls.transform.position-transform.position);

        foreach (Brick brick in bricksList) {
            if (brick != null)
            sensor.AddObservation(brick.transform.position);
        }

        if (LevelManager.Instance.RemainingBricks < bricks) {
            for (int i=0;i<bricks-(LevelManager.Instance.RemainingBricks);i++) {
                Debug.Log("Brick break reward");
                SetReward(2.0f);
            }
            bricks = LevelManager.Instance.RemainingBricks;
        }
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.ContinuousActions[0]);

        float moveX = actions.ContinuousActions[0];

        int moveSpeed = 10;
        transform.position += new Vector3(moveX,0,0) * Time.deltaTime * moveSpeed;

    }
private void OnCollisionEnter2D(Collision2D other) 
     {
        Debug.Log("paddle hits ball reward");
        SetReward(1.0f);
     }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
    }
}
