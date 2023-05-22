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

    // Brick[] bricksList;

    void Start()
    {
        // BallManager.Instance.CreateBall(ball);
        bricks = LevelManager.Instance.RemainingBricks;
        // brickContainer = LevelManager.Instance._bricksContainer;
    }
    // [SerializeField] private Transform ballTransform;
    public override void OnEpisodeBegin(){
        GameManager.Instance.gameStarted = true;
        BallManager.Instance._ballRb.AddForce(new Vector2(0, BallManager.Instance.ballStartForce));
        // ball = GameObject.Find("Ball Red(Clone)");
        // brickContainer = GameObject.Find("Bricks Container");
        // bricksList = brickContainer.GetComponentsInChildren<Brick>();
        // bricks = LevelManager.Instance.RemainingBricks;
        // Debug.Log(ball);
        // Debug.Log(brickContainer);
        // Debug.Log(bricksList.GetLength(0));
        GameManager.Instance.lives = 3;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        // sensor.AddObservation(ball.transform.position);
        // sensor.AddObservation(transform.position);
        // sensor.AddObservation(ball.transform.position-transform.position);

        // foreach (Brick brick in bricksList) {
        //     if (brick != null)
        //     sensor.AddObservation(brick.transform.position);
        // }

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.ContinuousActions[0]);

        // float moveX = actions.ContinuousActions[0];

        // int moveSpeed = 7;
        // transform.position += new Vector3(moveX,0,0) * Time.deltaTime * moveSpeed;

    }
private void OnCollisionEnter2D(Collision2D other) 
     {
        // if (other.otherCollider.TryGetComponent<Ball> (out Ball ball)) {
        //     Debug.Log("paddle hits ball reward");
        //     SetReward(+1f);
        // }

        // if (other.otherCollider.TryGetComponent<Brick> (out Brick brick)) {
        //     Debug.Log("Brick break reward");
        //     SetReward(+2f);
        // }

        // if (other.otherCollider.TryGetComponent<DeathWall> (out DeathWall deathWall)) {
        //     Debug.Log("Deathwall Penalty");
        //     SetReward(-2f);
        //     EndEpisode();
        // }

        // if (GameManager.Instance.lives < 2) {
        //     EndEpisode();
        // }
        
     }

    // public override void Heuristic(in ActionBuffers actionsOut)
    // {
    //     ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
    //     continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
    // }
}
